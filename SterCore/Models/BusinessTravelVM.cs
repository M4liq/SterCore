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
        [Required]
        [Display(Name = "Miejsce docelowe")]
        public string DestinationCity { get; set; }
        [Display(Name = "Cel podróży")]
        public string PurposeOfTravel { get; set; }
        [Display(Name = "Środek transportu")]
        public string TransportVehicle { get; set; }
        [Display(Name = "Uwagi")]
        public string AdditionalInfo { get; set; }

        [Display(Name = "NumerPWS")]
        public string ApplicationId { get; set; }
        [Display(Name = "Pracownik")]
        public EmployeeVM Employee { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        [Display(Name = "Pracownik")]
        public string EmployeeFullName { get; set; }
        public int TransportVehicleId { get; set; }
        public int CountryId { get; set; }
        public string OrganizationToken { get; set; }
        [Display(Name = "Zestawienie kosztów w przeliczeniu na PLN")]
        public decimal DifferenceOfCostsAndBillings { get; set; }
    }
    public class CreateBusinessTravelVM
    {
        public int Id { get; set; }
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
        [Required]
        [Display(Name = "Miejsce docelowe")]
        public string DestinationCity { get; set; }
        [Display(Name = "Cel podróży")]
        public string PurposeOfTravel { get; set; }
        [Display(Name = "Środek transportu")]
        public string TransportVehicle { get; set; }
        [Display(Name = "Uwagi")]
        public string AdditionalInfo { get; set; }

        [Display(Name = "NumerPWS")]
        public string ApplicationId { get; set; }
        [Display(Name = "Pracownik")]
        public EmployeeVM Employee { get; set; }
        [Required]
        [Display(Name = "Pracownik")]
        public string EmployeeId { get; set; }
        [Display(Name = "Środek transportu")]
        public int TransportVehicleId { get; set; }
        [Display(Name = "Kraj docelowy")]
        public int CountryId { get; set; }
        public string OrganizationToken { get; set; }
        public IEnumerable<SelectListItem> Employees { get; set; }
        public IEnumerable<SelectListItem> TransportVehicles { get; set; }
        public IEnumerable<SelectListItem> DestinationCountries { get; set; }
    }


}
