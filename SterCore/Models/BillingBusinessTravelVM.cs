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
        [Display(Name = "Kod wyjazdu służbowego")]

        public int BusinessTravelId { get; set; }
        [Display(Name = "Kod wyjazdu służbowego")]
        public string ApplicationId { get; set; }
        [Display(Name = "Kwota")]
        public decimal Amount { get; set; }
        [Display(Name = "Kurs waluty względem PLN")]
        public decimal ExchangeRate { get; set; }
        [Display(Name = "Czy dokonano operację?")]
        public bool IsPaid { get; set; }
        [Display(Name = "Waluta")]
        public int CurrencyId { get; set; }
        [Display(Name = "Waluta")]
        public string CurrencyName { get; set; }
        [Display(Name = "Rodzaj operacji")]
        public int TypeOfBillingId { get; set; }
        [Display(Name = "Rodzaj operacji")]
        public string TypeOfBillingName { get; set; }

    }

    public class CreateBillingBusinessTravelVM
    {
        public int Id { get; set; }
        [Display(Name = "Kod wyjazdu służbowego")]
        public string ApplicationId { get; set; }
        [Display(Name = "Kwota")]
        public decimal Amount { get; set; }
        [Display(Name = "Czy dokonano operację?")]
        public bool IsPaid { get; set; }
        [Display(Name = "Waluta")]
        public int CurrencyId { get; set; }
        [Display(Name = "Rodzaj operacji")]
        public int TypeOfBillingId { get; set; }
        [Required]
        [Display(Name = "Kod wyjazdu służbowego")]
        public int BusinessTravelId { get; set; }
        public string OrganizationToken { get; set; }
        [Required]
        [Display(Name = "Kurs waluty względem PLN")]
        public decimal ExchangeRate { get; set; }

        public IEnumerable<SelectListItem> BusinessTravels { get; set; }
        public IEnumerable<SelectListItem> Curencies { get; set; }
        public IEnumerable<SelectListItem> TypeOfBillings { get; set; }

    }
}
