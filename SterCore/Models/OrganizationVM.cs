using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class OrganizationVM
    {
        public int Id { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Kod")]
        public string Code { get; set; }
        [Display(Name = "NIP")]
        public string TaxId { get; set; }
        [Display(Name = "Ulica")]
        public string Street { get; set; }
        [Display(Name = "Miasto")]
        public string City { get; set; }
        [Display(Name = "Numer Domu")]
        public string HouseNumber { get; set; }
        [Display(Name = "Data utworzenia")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Aktywna")]
        public bool? Disabled { get; set; }
        [Display(Name = "Kod pocztowy")]
        public string ZipCode { get; set; }
        public int AuthorizedOrganizationId { get; set; }
    }

    public class OrganizationsVM
    {
        public ICollection<OrganizationVM> Organizations { get; set; }
    }
}
