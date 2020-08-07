using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class EmployeeVM
    {
        public string Id { get; set; }
        [Display(Name = "Nick")]
        public string UserName { get; set; }
        [Display(Name = "Adres Email")]
        public string Email { get; set; }
        [Display(Name = "Numer Telefonu")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Imię")]
        public string Firstname { get; set; }
        [Display(Name = "Nazwisko")]
        public string Lastname { get; set; }
        [Display(Name = "Numer Podatkowy")]
        public string TaxId { get; set; }
        [Display(Name = "Data Urodzenia")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Data Dodania")]
        public DateTime DateJoined { get; set; }

    }

    public class EditEmployeeVM
    {
        public string Id { get; set; }
        [Display(Name = "Nick")]
        public string UserName { get; set; }
        [Display(Name = "Adres Email")]
        public string Email { get; set; }
        [Display(Name = "Numer Telefonu")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Imię")]
        public string Firstname { get; set; }
        [Display(Name = "Nazwisko")]
        public string Lastname { get; set; }
        [Display(Name = "Data urodzenia")]
        public DateTime DateOfBirth { get; set; }
  
    }
}
