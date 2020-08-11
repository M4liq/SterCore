using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class BillingBusinessTravelVM
    {
        public int Id { get; set; }
        public EmployeeVM Employee { get; set; }
        public string EmployeeId { get; set; }
        public BusinessTravelVM BusinessTravel { get; set; }
        public int BusinessTravelId { get; set; }
        public int Amount { get; set; }
        public bool IsPaidOut { get; set; }

    }
}
