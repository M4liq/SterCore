using leave_management.Services.Components.ORI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data
{
    public class BillingBusinessTravel : OrganizationResurceIdentifier
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public string EmployeeId { get; set; }
        [ForeignKey("BusinessTravelId")]
        public BusinessTravel BusinessTravel { get; set; }
        public int BusinessTravelId { get; set; }
        public int Amount { get; set; }
        public bool IsPaidOut { get; set; }


    }
}
