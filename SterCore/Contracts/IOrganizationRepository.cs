using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Contracts
{
    public interface IOrganizationRepository : IRepositoryBase<Organization>
    {
        public Task<bool> Create(Organization organization, string organizationToken);
    }
}
