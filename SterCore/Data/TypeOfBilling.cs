using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using leave_management.Services.Components.ORI;

namespace leave_management.Data
{
    public class TypeOfBilling:OrganizationResurceIdentifier
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
