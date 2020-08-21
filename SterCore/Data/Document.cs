using leave_management.Services.Components.ORI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data
{
    public class Document : OrganizationResurceIdentifier
    {
        [Key]
        public int Id { get; set; }
        public string DocumentName { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public string EmployeeId { get; set; }
        public DateTime DateCreated { get; set; }
        public bool ShowSelectedEmployee { get; set; }
        public bool ShowSelectedDepartment { get; set; }
        public bool ShowCompanyWide { get; set; }
    }
}
