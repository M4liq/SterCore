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
        public EmployeeVM Employee { get; set; }
        [Display(Name = "Powiązany z pracownikiem")]
        public string EmployeeId { get; set; }
        [Display(Name = "Data utworzenia")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Pokaż wybranym pracownikom")]
        public bool ShowSelectedEmployee { get; set; }
        [Display(Name = "Pokaż wybranym działom")]
        public bool ShowSelectedDepartment { get; set; }
        [Display(Name = "Pokaż całej firmie")]
        public bool ShowCompanyWide { get; set; }
    }
}
