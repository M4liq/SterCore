using leave_management.Services.Components.ORI;
using leave_management.Services.ORI.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data
{
    public class Employee : IdentityUser, IApplicationDataView
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string TaxId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateJoined { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department  { get; set; }
        public int? DepartmentId { get; set; }
        public bool Deleted { get; set; }
        public bool ChangedPassword { get; set; }
        public string OrganizationToken { get; set; }
        public bool InitialAdministrator { get; set; }
    }
}
