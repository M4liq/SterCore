using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Contracts.IServiecies
{
    public interface IOrganizationManager : IRepositoryBase<Organization>
    {
        public string GenerateToken();
    }
}
