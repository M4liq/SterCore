using Microsoft.AspNetCore.Mvc.Rendering;
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

        [Display(Name = "Tytuł")]
        public string Title { get; set; }
        [Display(Name = "Data utworzenia")]
        public DateTime? DateCreated { get; set; }
        [Display(Name = "Data od")]
        [DataType(DataType.Date)]
        public DateTime DateFrom { get; set; }
        [Display(Name = "Data do")]
        [DataType(DataType.Date)]
        public DateTime DateTo { get; set; }
        [Display(Name = "System czasu pracy")]
        public string WorkingTimeSystem { get; set; }
        [Display(Name = "Umożliwiaj zmianę pracowników")]
        public bool EnableChangeOfEmployees { get; set; }
        [Display(Name = "Opis")]
        public string AdditionalInfo { get; set; }
        public bool IsApproved{ get; set; }
    }

    public class CreateWorkTimeScheduleVM
    {
        public int Id { get; set; }
        public string OrganizationToken { get; set; }

        [Display(Name = "Tytuł")]
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }

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
        public List<string> EmployeeIds { get; set; }

        public IEnumerable<SelectListItem> Employees { get; set; }
        public IEnumerable<SelectListItem> WorkingTimeSystems { get; set; }
    }
    public class Step2WorkTimeScheduleVM
    {
        public int Id { get; set; }
        public string OrganizationToken { get; set; }

        public List<string> EmployeeIds { get; set; }
        public List<string> EmployeeFullNames { get; set; }

        public int ScheduleId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public IEnumerable<SelectListItem> Employees { get; set; }

    }

}
