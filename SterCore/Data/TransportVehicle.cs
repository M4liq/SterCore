using System;
using System.Collections.Generic;
using System.Linq;
using leave_management.Services.Components.ORI;
using System.Threading.Tasks;

namespace leave_management.Data
{
    public class TransportVehicle : OrganizationResurceIdentifier
    {
        public string Name { get; set; }
        public int Value { get; set; }

    }
}
