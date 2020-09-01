using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class TrainingCourseVM
    {
        public int Id { get; set; }
        public string OrganizationToken { get; set; }
        public EmployeeVM Employee { get; set; }
        public string EmployeeId { get; set; }
        public TrainingCourseTypeVM TrainingCourseType { get; set; }
        public int TrainingCourseTypeId { get; set; }
        [Display(Name = "Data szkolenia:")]
        public DateTime DateOfTrainingCourse { get; set; }
        [Display(Name = "Ważne do:")]
        public DateTime DateValidUntil { get; set; }
        [Display(Name = "Opis:")]
        public string AdditionalInfo { get; set; }
        [Display(Name = "Pokaż pracownikowi")]
        public bool ShowSelectedEmployee { get; set; }
        [Display(Name = "Pokaż przełożonemu")]
        public bool ShowSelectedDepartment { get; set; }
        [Display(Name = "Osoba:")]
        public string EmployeeFullName { get; set; }
        [Display(Name = "Typ szkolenia:")]
        public string TrainingCourseTypeName { get; set; }
    }

    public class CreateTrainingCourseVM
    {
        public int Id { get; set; }
        public string OrganizationToken { get; set; }
        public EmployeeVM Employee { get; set; }
        public string EmployeeId { get; set; }
        public TrainingCourseTypeVM TrainingCourseType { get; set; }
        public int TrainingCourseTypeId { get; set; }
        [Display(Name = "Data szkolenia:")]
        public DateTime DateOfTrainingCourse { get; set; }
        [Display(Name = "Ważne do:")]
        public DateTime DateValidUntil { get; set; }
        [Display(Name = "Opis:")]
        public string AdditionalInfo { get; set; }
        [Display(Name = "Pokaż pracownikowi")]
        public bool ShowSelectedEmployee { get; set; }
        [Display(Name = "Pokaż przełożonemu")]
        public bool ShowSelectedDepartment { get; set; }

        public IEnumerable<SelectListItem> Employees { get; set; }
        public IEnumerable<SelectListItem> TrainingCourseTypes { get; set; }
    }
}
