using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using leave_management.Services.ORI.Contracts;

namespace leave_management.Data
{
    public class ExplicitLeaveTypes : IApplicationDataView
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Limit { get; set; }
        public DateTime DateCreated { get; set; }
        public string OrganizationToken { get; set; }
        public string DepartmentToken { get; set; }
    }
}
