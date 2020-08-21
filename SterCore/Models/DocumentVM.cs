using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class DocumentVM
    {
        public int Id { get; set; }
        [Display(Name = "Dokument")]
        public string DocumentName { get; set; }
        [Display(Name = "Opis")]
        public string Description { get; set; }
        public EmployeeVM Employee { get; set; }
        public string EmployeeId { get; set; }
        [Display(Name = "Powiązany z pracownikiem")]
        public string EmployeeFullName { get; set; }

        [Display(Name = "Data utworzenia")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Pokaż wybranym pracownikom")]
        public bool ShowSelectedEmployee { get; set; }
        [Display(Name = "Pokaż wybranym działom")]
        public bool ShowSelectedDepartment { get; set; }
        [Display(Name = "Pokaż całej firmie")]
        public bool ShowCompanyWide { get; set; }
    }
    public class CreateDocumentVM
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nazwa dokumentu")]
        public string DocumentName { get; set; }
        public EmployeeVM Employee { get; set; }
        [Required]
        [Display(Name = "Powiązany z pracownikiem")]
        public string EmployeeId { get; set; }
        [Display(Name = "Powiązany z pracownikiem")]
        public string EmployeeFullName { get; set; }
        public IEnumerable<SelectListItem> Employees { get; set; }
        [Display(Name = "Data utworzenia")]
        public DateTime? DateCreated { get; set; }
        [Display(Name = "Opis")]
        public string Description { get; set; }
        [Display(Name = "Pokaż wybranym pracownikom")]
        public bool ShowSelectedEmployee { get; set; }
        [Display(Name = "Pokaż wybranym działom")]
        public bool ShowSelectedDepartment { get; set; }
        [Display(Name = "Pokaż całej firmie")]
        public bool ShowCompanyWide { get; set; }
    }
}
