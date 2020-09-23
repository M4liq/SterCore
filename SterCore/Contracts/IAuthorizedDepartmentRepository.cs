using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using leave_management.Data;

namespace leave_management.Contracts
{
    public interface IAuthorizedDepartmentRepository
    {
        public Task<bool> Create(AuthorizedDepartment authorizedDepartment);
        public Task<bool> Save();
    }
}
