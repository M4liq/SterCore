using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using leave_management.Data;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using leave_management.Services.Extensions;
using leave_management.Contracts;
using Microsoft.AspNetCore.Http;

namespace leave_management.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<Employee> _userManager;
        private readonly SignInManager<Employee> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IEmployeeRepository _employeeRepository;

        public LoginModel(SignInManager<Employee> signInManager, 
            ILogger<LoginModel> logger,
            UserManager<Employee> userManager,
            IEmailSender emailSender,
            IEmployeeRepository employee)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _employeeRepository = employee;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
               
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if(user == null)
                {
                    ModelState.AddModelError(string.Empty, "Niepoprawna próba logowania");
                    return Page();
                }

                var passwordchanged = user.ChangedPassword;

                if (passwordchanged != true)
                {
                    return RedirectToPage("./FirstRegistration");
                }

                var organization = _employeeRepository.FindById(user.Id, true).Result.Organization;

                if (organization.Disabled)
                {

                    ModelState.AddModelError(string.Empty, "Twoja organizacja została tymczasowo wyłączona z systemu. Skontaktuj się z Twoim dostawcą.");
                    return Page();
                }

                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                {

                    _logger.LogInformation("User logged in.");

                    if (_userManager.GetRolesAsync(user).Result.Contains("Administrator") ||
                        _userManager.GetRolesAsync(user).Result.Contains("Agent"))
                    {
                        HttpContext.Session.ExtSet("organizationToken", organization.OrganizationToken);
                        HttpContext.Session.ExtSet<string>("organizationName", organization.Name);
                        return RedirectToPage("./ChangeOrganizationView");
                    }

                    HttpContext.Session.ExtSet<string>("organizationName", organization.Name);
                    HttpContext.Session.ExtSet("organizationToken", organization.OrganizationToken);
                    return LocalRedirect(returnUrl);
                }

                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("Konto zostało zablokowane");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Niepoprawna próba logowania");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Email weryfikacyjny został wysłany. Sprawdź swoją skrzynkę e-mail.");
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                Input.Email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            ModelState.AddModelError(string.Empty, "Email weryfikacyjny został wysłany. Sprawdź swoją skrzynkę e-mail.");
            return Page();
        }
    }
}
