using leave_management.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class WorkTimeScheduleEmployeeVM
    {
        public int Id { get; set; }
        public string OrganizationToken { get; set; }
        public WorkTimeScheduleVM WorkTimeSchedule { get; set; }
        [Display(Name = "Harmonogram")]
        public int ScheduleId { get; set; }
        public EmployeeVM Employee { get; set; }
        [Display(Name = "Osoba")]
        public int EmployeeId { get; set; }
    }
}
