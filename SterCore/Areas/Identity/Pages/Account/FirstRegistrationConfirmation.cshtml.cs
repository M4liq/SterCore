﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace leave_management.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class FirstRegistrationConfirmation : PageModel
    {
        public void OnGet()
        {
        }
    }
}
