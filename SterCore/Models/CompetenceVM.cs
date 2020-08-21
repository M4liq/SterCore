using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class CompetenceVM
    {
        public int Id { get; set; }
        public EmployeeVM Employee { get; set; }
        public string EmployeeId { get; set; }
        [Display(Name = "Osoba")]
        public string EmployeeFullName { get; set; }
        public CompetenceTypeVM CompetenceType { get; set; }
        public int CompetenceTypeId { get; set; }
        [Display(Name = "Kompetencje")]
        public string CompetenceName{ get; set; }
        [Display(Name = "Ważne do")]
        public DateTime DateValidUntil { get; set; }
    }
}
