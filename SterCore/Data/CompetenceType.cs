using leave_management.Services.Components.ORI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data
{
    public class CompetenceType : OrganizationResurceIdentifier
    {
        public string name { get; set; }
    }
}
