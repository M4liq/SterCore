using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class DepartmentVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime DateCreated { get; set; }
        public bool InitialDepartment { get; set; }
        public string OrganizationToken { get; set; }
        public int OrganizationId { get; set; }
    }
}
