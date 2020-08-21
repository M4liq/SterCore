using leave_management.Services.Components.ORI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data
{
    public class Competence : OrganizationResurceIdentifier
    {
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set;}
        public string EmployeeId { get; set;}
        [ForeignKey("CompetenceTypeId")]
        public CompetenceType CompetenceType { get; set; }
        public int CompetenceTypeId { get; set; }
        public DateTime DateValidUntil { get; set; }
    }
}
