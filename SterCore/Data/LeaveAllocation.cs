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
    public class LeaveAllocations : IApplicationDataView
    {
        public int Id { get; set; }
        public int NumberOfDays { get; set; }
        public DateTime DateCreated { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public string EmployeeId { get; set; }
        [ForeignKey("CommonLeaveTypeId")]
        public CommonLeaveTypes CommonLeaveType { get; set; }
        public int? CommonLeaveTypeId { get; set; }
        [ForeignKey("ExplicitLeaveTypeId")]
        public ExplicitLeaveTypes ExplicitLeaveType { get; set; }
        public int? ExplicitLeaveTypeId { get; set; }
        public int Period { get; set; }
        public string OrganizationToken { get; set; }
        public string DepartmentToken { get; set; }
    }
}
