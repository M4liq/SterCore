using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using leave_management.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading;
using leave_management.Repository;
using leave_management.Contracts;
using System.Linq;
using leave_management.Controllers;
using leave_management.Services.Extensions;

namespace leave_management.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "Administrator, Agent")]
    public class ChangeOrganizationView : PageModel
    {

        [BindProperty]
        public InputModel Input { get; set; }
        public IEnumerable<SelectListItem> Organizations { get; set; }
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public ChangeOrganizationView(IOrganizationRepository organizationRepository, IDepartmentRepository departmentRepository)
        {
            _organizationRepository = organizationRepository;
            _departmentRepository = departmentRepository;
        }
        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Display(Name = "Organizacja")]
            [Required]
            public int OrganizationId { get; set; }

        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl ?? Url.Content("~/");
            var organizations = await _organizationRepository.FindAll();
            var organizationItems = organizations.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });

            this.Organizations = organizationItems;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl ?? Url.Content("~/");

            var organization = await _organizationRepository.FindById(Input.OrganizationId);
            var department = await _departmentRepository.FindInitialDepartment(organization);

            HttpContext.Session.ExtSet<string>("organizationToken", organization.OrganizationToken);
            HttpContext.Session.ExtSet<string>("organizationName", organization.Name);
            HttpContext.Session.ExtSet<string>("departmentToken", department.DepartmentToken);

            return RedirectToPage("./ChangeDepartment");
        }
    }
}
