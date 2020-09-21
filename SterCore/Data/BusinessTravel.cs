using leave_management.Services.Components.ORI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using leave_management.Services.ORI.Contracts;


namespace leave_management.Data
{
    public class BusinessTravel : IApplicationDataView
    {
        [Key]
        public int Id { get; set; }
        public string OrganizationToken { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string DestinationCity { get; set; }
        public string PurposeOfTravel { get; set; }
        public string AdditionalInfo{ get; set; }
        public string ApplicationId{ get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public string EmployeeId { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }
        public int CountryId { get; set; }

        [ForeignKey("TransportVehicleId")]
        public TransportVehicle TransportVehicle { get; set; }
        public int TransportVehicleId { get; set; }
        public string DepartmentToken { get; set; }
    }
}
