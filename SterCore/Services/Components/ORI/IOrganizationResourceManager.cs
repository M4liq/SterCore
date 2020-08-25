using leave_management.Data;
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

            public Task<int> GetAuthorizedOrganizationId(string token);

            public Task<Organization> GetCurrentOrganization();

            public Task<AuthorizedOrganizations> Authorize(string ogranizationToken);

            public bool HasPrivilegeGranted();
        }

}
