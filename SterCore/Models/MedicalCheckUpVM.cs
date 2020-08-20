using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class MedicalCheckUpVM
    {
        public int Id { get; set; }
        [Required]
        public DateTime DateOfMedicalExamination { get; set; }
        public DateTime ValidUntil { get; set; }
        public string Comment { get; set; }
        public bool IsDisplayedToEmployee { get; set; }
        public bool IsDisplayedToSupervisor { get; set; }
        public EmployeeVM Employee { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public int TypeOfMedicalCheckUpId { get; set; }
        public IEnumerable<SelectListItem> Employees { get; set; }
        public IEnumerable<SelectListItem> TypeOfMedicalCheckUps { get; set; }
        public IEnumerable<SelectListItem> isDisplayedToEmployees { get; set; }
        public IEnumerable<SelectListItem> isDisplayedToSupervisors { get; set; }

    }
}
