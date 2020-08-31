using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class ExpenseVM
    {
        public int Id { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Kwota")]
        public int Amount { get; set; }
        [Display(Name = "Waluta")]
        public int CurrencyId { get; set; }
        [Display(Name = "Waluta")]
        public string CurrencyName { get; set; }
        [Display(Name = "Kod wyjazdu służbowego")]
        public int BusinessTravelId { get; set; }
        [Display(Name = "Kod wyjazdu służbowego")]
        public string ApplicationId { get; set; }
        public string OrganizationToken { get; set; }
    }

    public class CreateExpenseVM
    {
        public int Id { get; set; }
        public string OrganizationToken { get; set; }

        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        
        [Display(Name = "Kwota")]
        public int Amount { get; set; }
        [Required]
        [Display(Name = "Kurs waluty względem PLN")]
        public decimal ExchangeRate { get; set; }
        [Display(Name = "Waluta")]
        public int CurrencyId { get; set; }
        
        [Display(Name = "Kod wyjazdu służbowego")]
        public int BusinessTravelId { get; set; }
        [Display(Name = "Kod wyjazdu służbowego")]
        public string ApplicationId { get; set; }
        public IEnumerable<SelectListItem> BusinessTravels { get; set; }

        public IEnumerable<SelectListItem> Curencies { get; set; }

    }
}
