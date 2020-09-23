using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Contracts
{
    public interface IDepartmentRepository : IRepositoryBase<Department>
    {   
        //The reason of this method is to bypass token verification in dataSeeding
        public Task<bool> Create(Department entity, string authorizedToken);
        public Task<Department> FindInitialDepartment(Organization organization);

    }
}
