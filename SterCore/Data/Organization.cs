using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace leave_management.Data
{
    public class Organization
    {   
        [Key]
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
        public string OrganizationToken { get; set; }

        [ForeignKey("AuthorizedOrganizationId")]
        public AuthorizedOrganizations AuthorizedOrganizations { get; set; }
        public int AuthorizedOrganizationId { get; set; }
    }
}
