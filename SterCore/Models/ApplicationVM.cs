using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace leave_management.Models
{
    public class ApplicationVM
    {
        public int Id { get; set; }
        public string OrganizationToken { get; set; }
        public EmployeeVM Employee { get; set; }
        [Display(Name ="Osoba")]
        public string EmployeeId { get; set; }
        [Display(Name = "Wniosek")]
        public string ApplicationName { get; set; }
        [Display(Name = "Data wniosku")]
        public DateTime DateOfApplication { get; set; }
        [Display(Name = "Status")]
        public bool Status { get; set; }
        [Display(Name = "Osoba")]
        public string EmployeeFullName { get; set; }
    }
    public class CreateApplicationVM
    {
        public int Id { get; set; }
        public string OrganizationToken { get; set; }
        public EmployeeVM Employee { get; set; }
        [Display(Name = "Osoba")]
        public string EmployeeId { get; set; }
        [Display(Name = "Wniosek")]
        public string ApplicationName { get; set; }
        [Display(Name = "Data wniosku")]
        public DateTime DateOfApplication { get; set; }
        [Display(Name = "Status")]
        public bool Status { get; set; }
        public IEnumerable<SelectListItem> Employees { get; set; }
    }
}
