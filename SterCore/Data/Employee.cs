using leave_management.Services.Components.ORI;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data
{
    public class Employee : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string TaxId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateJoined { get; set; }
        [ForeignKey("OrganizationId")]
        public Organization Organization { get; set; }
        public int OrganizationId { get; set; }
        public bool Deleted { get; set; }
        public bool ChangedPassword { get; set; }
        public string OrganizationToken { get; set; }
    }
}
