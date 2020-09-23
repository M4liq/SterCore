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
using leave_management.Services.Components.ORI;
using leave_management.Services.Extensions;

namespace leave_management.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "Administrator, Agent, Employer")]
    public class ChangeDepartmentView : PageModel
    {

        [BindProperty]
        public InputModel Input { get; set; }
        public IEnumerable<SelectListItem> Organizations { get; set; }
        public IEnumerable<SelectListItem> Departments { get; set; }
        public IOrganizationResourceManager organizationResourceManager { get; }
        public IDepartmentRepository departmentRepository { get; }
        public UserManager<Employee> userManager { get; }   
        public ChangeDepartmentView(
            IOrganizationResourceManager organizationResourceManager, 
            IDepartmentRepository departmentRepository, 
            UserManager<Employee> userManager)
        {
            this.organizationResourceManager = organizationResourceManager;
            this.departmentRepository = departmentRepository;
            this.userManager = userManager;
        }
        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Display(Name = "Dział")]
            [Required]
            public int DepartmentId { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl ?? Url.Content("~/");
            var allDepartments = await departmentRepository.FindAll();
            var organization = await organizationResourceManager.GetCurrentOrganization();
            var departments = allDepartments
                .Where(q => q.Organization.Id == organization.Id)
                .Where(q => q.Organization.AuthorizedOrganizationId == organization.Id);
                
      //  Issue: it gets all departments. Should get only those from this organization

        var departmentsItems = departments.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });

            this.Departments = departmentsItems;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {

            this.ReturnUrl = returnUrl ?? Url.Content("~/");
            var allDepartments = await departmentRepository.FindAll();
            var organization = await organizationResourceManager.GetCurrentOrganization();
            var departments = allDepartments
                .Where(q => q.Organization.Id == organization.Id)
                .Where(q => q.Organization.AuthorizedOrganizationId == organization.Id);

            //  Issue: it gets all departments. Should get only those from this organization

            var departmentsItems = departments.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });

            this.Departments = departmentsItems;


            var department = await departmentRepository.FindById(Input.DepartmentId);

            HttpContext.Session.ExtSet<string>("departmentToken", department.DepartmentToken);
            HttpContext.Session.ExtSet<string>("departmentName", department.Name);

            return LocalRedirect(this.ReturnUrl);
        }
    }
}
