using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class CompetenceVM
    {
        public int Id { get; set; }
        public EmployeeVM Employee { get; set; }
        public string EmployeeId { get; set; }
        public CompetenceTypeVM CompetenceType { get; set; }
        public int CompetenceTypeId { get; set; }
        public DateTime DateValidUntil { get; set; }
    }
}
