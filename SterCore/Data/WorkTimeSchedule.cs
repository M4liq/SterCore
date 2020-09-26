using leave_management.Services.Components.ORI;
using leave_management.Services.ORI.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data
{
    public class WorkTimeSchedule : IApplicationDataView
    {
        [Key]
        public int Id { get; set; }
        public string OrganizationToken { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string WorkingTimeSystem { get; set; }
        public bool EnableChangeOfEmployees { get; set; }
        public string AdditionalInfo { get; set; }
        public bool IsApproved{ get; set; }

        public string DepartmentToken { get; set; }
    }
}
