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
        [Display(Name = "Data Badania")]
        [DataType(DataType.Date)]
        public DateTime DateOfMedicalExamination { get; set; }
        [Display(Name = "Ważne do")]
        [DataType(DataType.Date)]
        public DateTime ValidUntil { get; set; }
        [Display(Name = "Uwagi")]
        public string Comment { get; set; }
        [Display(Name = "Pokaż Pracownikowi")]
        public bool IsDisplayedToEmployee { get; set; }
        [Display(Name = "Pokaż Przełożonemu")]
        public bool IsDisplayedToSupervisor { get; set; }
        public EmployeeVM Employee { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        [Display(Name = "Osoba")]
        public string EmployeeFullName { get; set; }
        [Required]
        public int TypeOfMedicalCheckUpId { get; set; }
        [Display(Name = "Rodzaj badania")]
        public string TypeOfMedicalCheckUpName { get; set; }


    }
    public class CreateMedicalCheckUpVM
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data Badania")]
        public DateTime DateOfMedicalExamination { get; set; }
        [Display(Name = "Ważne do")]
        [DataType(DataType.Date)]
        public DateTime ValidUntil { get; set; }
        [Display(Name = "Uwagi")]
        public string Comment { get; set; }
        [Display(Name = "Pokaż Pracownikowi")]
        public bool IsDisplayedToEmployee { get; set; }
        [Display(Name = "Pokaż Przełożonemu")]
        public bool IsDisplayedToSupervisor { get; set; }
        public EmployeeVM Employee { get; set; }
        [Required]
        [Display(Name = "Osoba")]
        public string EmployeeId { get; set; }
        [Required]
        [Display(Name = "Rodzaj Badania")]
        public int TypeOfMedicalCheckUpId { get; set; }
        public IEnumerable<SelectListItem> Employees { get; set; }
        public IEnumerable<SelectListItem> TypeOfMedicalCheckUps { get; set; }
        public IEnumerable<SelectListItem> isDisplayedToEmployees { get; set; }
        public IEnumerable<SelectListItem> isDisplayedToSupervisors { get; set; }
    }
    public class EditMedicalCheckUpVM
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data Badania")]
        public DateTime DateOfMedicalExamination { get; set; }
        [Display(Name = "Ważne do")]
        [DataType(DataType.Date)]
        public DateTime ValidUntil { get; set; }
        [Display(Name = "Uwagi")]
        public string Comment { get; set; }
        [Display(Name = "Pokaż Pracownikowi")]
        public bool IsDisplayedToEmployee { get; set; }
        [Display(Name = "Pokaż Przełożonemu")]
        public bool IsDisplayedToSupervisor { get; set; }
        public EmployeeVM Employee { get; set; }
        [Required]
        [Display(Name = "Osoba")]
        public string EmployeeId { get; set; }
        [Required]
        [Display(Name = "Rodzaj Badania")]
        public int TypeOfMedicalCheckUpId { get; set; }
        public IEnumerable<SelectListItem> isDisplayedToEmployees { get; set; }
        public IEnumerable<SelectListItem> isDisplayedToSupervisors { get; set; }
    }

}
