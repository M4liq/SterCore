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
    public class TrainingCourse : IApplicationDataView
    {
        [Key]
        public int Id { get; set; }
        public string OrganizationToken { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public string EmployeeId { get; set; }
        [ForeignKey("TrainingCourseTypeId")]
        public TrainingCourseType TrainingCourseType { get; set; }
        public int TrainingCourseTypeId { get; set; }
        public DateTime DateOfTrainingCourse { get; set; }
        public DateTime DateValidUntil { get; set; }
        public string AdditionalInfo { get; set; }
        public bool ShowSelectedEmployee { get; set; }
        public bool ShowSelectedDepartment { get; set; }
    }
}
