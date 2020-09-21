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
    public class LeaveRequests : IApplicationDataView
    {
        public int Id { get; set; }
        [ForeignKey("RequestingEmployeeId")]
        public Employee RequestingEmployee { get; set; }
        public string RequestingEmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [ForeignKey("LeaveTypeId")]
        public LeaveType LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime DateActioned { get; set; }
        public bool? Approved { get; set; }
        public bool? Cancelled { get; set; }
        [ForeignKey("ApprovedById")]
        public Employee ApprovedBy { get; set; }
        public string ApprovedById { get; set; }
        public string Comment { get; set; }
        public string OrganizationToken { get; set; }
        public string DepartmentToken { get; set; }
    }
}
