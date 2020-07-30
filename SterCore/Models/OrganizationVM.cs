using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class OrganizationVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string TaxId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string HouseNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public bool? Disabled { get; set; }
        public string ZipCode { get; set; }
    }

    public class OrganizationsVM
    {
        public ICollection<OrganizationVM> Organizations { get; set; }
    }
}
