using leave_management.Services.Components.ORI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data
{
    public class MedicalCheckUp : OrganizationResurceIdentifier
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateOfMedicalExamination{ get; set; }
        public DateTime ValidUntil{ get; set; }
        public string Comment { get; set; }
        public bool isDisplayedToEmployee{ get; set; }
        public bool isDisplayedToSupervisor{ get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public string EmployeeId { get; set; }
        [ForeignKey("TypeOfMedicalCheckUpId")]
        public TypeOfMedicalCheckUp typeOfMedicalCheckUp { get; set; }
        public int TypeOfMedicalCheckUpId { get; set; }
    }
}
