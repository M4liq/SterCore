﻿using leave_management.Services.Components.ORI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using leave_management.Services.ORI.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data
{
    public class WorkTimeScheduleEmployee : IApplicationDataView
    {
        [Key]
        public int Id { get; set; }
        public string OrganizationToken { get; set; }
        [ForeignKey("ScheduleId")]
        public WorkTimeSchedule WorkTimeSchedule { get; set; }
        public int ScheduleId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public string EmployeeId { get; set; }
    }
}