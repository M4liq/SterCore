using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class WorkTimeScheduleVM
    {
        public int Id { get; set; }
        public string OrganizationToken { get; set; }
        public EmployeeVM Employee { get; set; }
        [Display(Name = "Pracownicy")]
        public string EmployeeId { get; set; }
        [Display(Name = "Tytuł")]
        public string Title { get; set; }
        [Display(Name = "Data od")]
        public DateTime DateFrom { get; set; }
        [Display(Name = "Data do")]
        public DateTime DateTo { get; set; }
        [Display(Name = "System czasu pracy")]
        public string WorkingTimeSystem { get; set; }
        [Display(Name = "Umożliwiaj zmianę pracowników")]
        public bool EnableChangeOfEmployees { get; set; }
        [Display(Name = "Opis")]
        public string AdditionalInfo { get; set; }
    }

}
