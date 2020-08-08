using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using leave_management.Contracts;
using leave_management.Contracts.IServiecies;
using leave_management.Data;
using leave_management.Repository;
using leave_management.Services.Components;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace leave_management.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "Employer,Agent,Administrator")]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Employee> _signInManager;
        private readonly UserManager<Employee> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IOrganizationManager _organizationManager;

        public RegisterModel(
            UserManager<Employee> userManager,
            SignInManager<Employee> signInManager,
            ILogger<RegisterModel> logger,
            IEmployeeRepository employeeRepository,
            IEmailSender emailSender,
            IOrganizationManager organizationManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _organizationManager = organizationManager;
            _employeeRepository = employeeRepository;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IEnumerable<SelectListItem> Organizations { get; set; }
        public IEnumerable<SelectListItem> SystemRoles { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {

            [Required]
            [EmailAddress]
            [Display(Name = "Email Address")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Display(Name = "Organizacja")]
            public int OrganizationId { get; set; }

            [Display(Name = "Rola użytkownika")]
            public string UserRoleId { get; set; }

        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var roles = await _userManager.GetRolesAsync(user);
            

            if (roles.Contains("Agent") || roles.Contains("Administrator"))
            {
                var organizations = await _organizationManager.FindAll();
                var organizationItems = organizations.Select(q => new SelectListItem
                {
                    Text = q.Name,
                    Value = q.Id.ToString()
                });

                Organizations = organizationItems;

            }


            if (roles.Contains("Agent"))
            {
                var systemRoles = await _employeeRepository.GetAgentIdentityRoles();
                var systemRolesItems = systemRoles.Select(q => new SelectListItem
                {
                    Text = q.Name,
                    Value = q.Name
                });

                SystemRoles = systemRolesItems;

            }

            if (roles.Contains("Administrator"))
            {
                var systemRoles = await _employeeRepository.GetAdministratorIdentityRoles();
                var systemRolesItems = systemRoles.Select(q => new SelectListItem
                {
                    Text = q.Name,
                    Value = q.Name
                });

                SystemRoles = systemRolesItems;

            }

            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                    var superior = await _userManager.GetUserAsync(HttpContext.User);
                    var roles = await _userManager.GetRolesAsync(superior);
                    var superiorWithOrganization = await _employeeRepository.FindById(superior.Id);
                    var organizaiton = superiorWithOrganization.Organization;

                    var user = new Employee
                    {
                        UserName = Input.Email,
                        Email = Input.Email,
                        Firstname = Input.FirstName,
                        Lastname = Input.LastName,
                        ChangedPassword = false,
                        DateJoined = DateTime.Now,
                        DateOfBirth = DateTime.Now.AddYears(-35).Date
                    };
               
                if (roles.Contains("Administrator")||roles.Contains("Agent"))
                {
                    user.Organization = await _organizationManager.FindById(Input.OrganizationId);
                    user.OrganizationId = Input.OrganizationId;
                }
                else
                {
                    user.Organization = organizaiton;
                    user.OrganizationId = organizaiton.Id;
                }

                if (roles.Contains("Agent"))
                {
                    var systemRoles = await _employeeRepository.GetAgentIdentityRoles();
                    var systemRolesItems = systemRoles.Select(q => new SelectListItem
                    {
                        Text = q.Name,
                        Value = q.Name
                    });

                    SystemRoles = systemRolesItems;

                }

                if (roles.Contains("Administrator"))
                {
                    var systemRoles = await _employeeRepository.GetAdministratorIdentityRoles();
                    var systemRolesItems = systemRoles.Select(q => new SelectListItem
                    {
                        Text = q.Name,
                        Value = q.Name
                    });

                    SystemRoles = systemRolesItems;

                }

                var result = await _userManager.CreateAsync(user, "P@ssword1");
                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, Input.UserRoleId).Wait();
                    _logger.LogInformation("User created a new account without password.");


                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ResetPassword",
                        pageHandler: null,
                        values: new { area = "Identity", code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(
                        Input.Email,
                        "Ustawienie hasła",
                        $"Ustaw nowe hasło <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>klikając tutaj</a>.");

                    return RedirectToAction("Index", "Employee");
                    
                }

                if (roles.Contains("Agent") || roles.Contains("Administrator"))
                {
                    var organizations = await _organizationManager.FindAll();
                    var organizationItems = organizations.Select(q => new SelectListItem
                    {
                        Text = q.Name,
                        Value = q.Id.ToString()
                    });

                    Organizations = organizationItems;

                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            // If we got this far, something failed, redisplay form
            return Page();
        }


    }
}
