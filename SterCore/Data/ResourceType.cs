using leave_management.Services.Components.ORI;
using leave_management.Services.ORI.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data
{
    public class ResourceType : IApplicationDataView
    {
        [Key]
        public int Id { get; set; }
        public string OrganizationToken { get; set; }
        public string Name { get; set; }
        public bool showDateFrom { get; set; }
        public bool showDateUntil { get; set; }
        public bool showReturned { get; set; }
        public string DepartmentToken { get; set; }
    }
}
