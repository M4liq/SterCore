using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Services.Components.ORI
{ 
        public interface IOrganizationResourceManager
        {
            public string GenerateToken();

            public string GetOrganizationToken();
        }

}
