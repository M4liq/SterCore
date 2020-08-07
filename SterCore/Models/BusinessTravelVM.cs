using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class BusinessTravelVM
    {
        public int Id { get; set; }
        [Required]
        public DateTime DateFrom { get; set; }
        [Required]
        public DateTime DateTo { get; set; }
        public string DestinationCountry { get; set; }
        public string DestinationCity { get; set; }
        public string PurposeOfTravel { get; set; }
        public string TransportVehicle { get; set; }
        public string AdditionalInfo { get; set; }
        public int PrepaymentAmount { get; set; }
        public string PrepaymentCurrency { get; set; }
        public string ApplicationId { get; set; }
        [Required]
        public EmployeeVM Employee { get; set; }
        public string EmployeeId { get; set; }

        public IEnumerable<SelectListItem> Employees { get; set; }
    }
}
