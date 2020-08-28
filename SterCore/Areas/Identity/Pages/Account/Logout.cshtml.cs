using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using leave_management.Data;
using leave_management.Services.Components;
using leave_management.Services.Extensions;

namespace leave_management.Areas.Identity.Pages.Account
{
    [Authorize]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<Employee> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<Employee> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            await _signInManager.SignOutAsync();

            _logger.LogInformation("User logged out.");

                return RedirectToPage();

        }
    }
}
