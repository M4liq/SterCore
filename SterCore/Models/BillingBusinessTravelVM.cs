using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Kwota")]
        public int Amount { get; set; }
        [Display(Name = "Czy wypłacono kwotę?")]
        public bool IsPaidOut { get; set; }

    }

    public class CreateBillingBusinessTravelVM
    {
        public int Id { get; set; }
        public EmployeeVM Employee { get; set; }
        public IEnumerable<SelectListItem> Employees { get; set; }
        public string EmployeeId { get; set; }
        public BusinessTravelVM BusinessTravel { get; set; }
        public IEnumerable<SelectListItem> BusinessTravels { get; set; }
        public int BusinessTravelId { get; set; }
        [Display(Name = "Kwota")]
        public int Amount { get; set; }
        [Display(Name = "Czy wypłacono kwotę?")]
        public bool IsPaidOut { get; set; }
    }
}
