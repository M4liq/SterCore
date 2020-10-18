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
    public class WorkTimeScheduleEvent : IApplicationDataView
    {
        [Key]
        public int Id { get; set; }
        public string OrganizationToken { get; set; }
        public string DepartmentToken { get; set; }
        public int SchedulerId { get; set; }
        public int EventId { get; set; }
        public string Subject { get; set; }
        public string Guid { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string ShiftStartTime { get; set; }
        public string ShiftEndTime { get; set; }
        public bool IsAllDay { get; set; }
        public string EmployeeId { get; set; }
        public string Description { get; set; }
        public int PauseTimeLength { get; set; }
        public string WorkTimeLength { get; set; }

    }
}
