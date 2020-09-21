using leave_management.Services.ORI.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data
{
    public class Department : IApplicationDataView
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string DepartmentToken { get; set; }
        public DateTime DateCreated { get; set; }
        public bool InitialDepartment { get; set; }
        public string OrganizationToken { get; set; }
        [ForeignKey("OrganizationId")]
        public Organization Organization { get; set; }
        public int OrganizationId { get; set; }
    }
}
