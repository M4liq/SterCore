using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class BusinessTravelVM
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data Zlecenia")]
        public DateTime DateCreated { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data od")]
        public DateTime DateFrom { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data do")]
        public DateTime DateTo { get; set; }
        [Display(Name = "Kraj docelowy")]
        public string DestinationCountry { get; set; }
        [Display(Name = "Miejsce docelowe")]
        public string DestinationCity { get; set; }
        [Display(Name = "Cel podróży")]
        public string PurposeOfTravel { get; set; }
        [Display(Name = "Środek transportu")]
        public string TransportVehicle { get; set; }
        [Display(Name = "Uwagi")]
        public string AdditionalInfo { get; set; }
        [Display(Name = "Kwota zaliczki")]
        public int PrepaymentAmount { get; set; }
        [Display(Name = "Waluta zaliczki")]
        public string PrepaymentCurrency { get; set; }
        [Display(Name = "NumerPWS")]
        public string ApplicationId { get; set; }
        
        public EmployeeVM Employee { get; set; }
        public string EmployeeId { get; set; }
        public IEnumerable<SelectListItem> Employees { get; set; }
    }
}
