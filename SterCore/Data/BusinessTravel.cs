using leave_management.Services.Components.ORI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data
{
    public class BusinessTravel : OrganizationResurceIdentifier
    {
        public DateTime? DateCreated { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string DestinationCountry { get; set; }
        public string DestinationCity { get; set; }
        public string PurposeOfTravel { get; set; }
        public string TransportVehicle{ get; set; }
        public string AdditionalInfo{ get; set; }
        public string ApplicationId{ get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public string EmployeeId { get; set; }
    }
}
