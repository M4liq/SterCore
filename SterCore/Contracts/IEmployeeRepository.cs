using leave_management.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Contracts
{
    public interface IEmployeeRepository
    {
        Task<ICollection<Employee>> FindAll();
        Task<ICollection<Employee>> FindUsersWithEmail(string email);
        Task<Employee> FindById(string id);
        Task<Employee> FindById(string id, bool disableORI);
        Task<bool> Exists(string id);
        Task<bool> Create(Employee entity);
        Task<bool> Update(Employee entity);
        Task<bool> Delete(Employee entity);
        Task<bool> Save();
        Task<IEnumerable<IdentityRole>> GetAgentIdentityRoles();
        Task<IEnumerable<IdentityRole>> GetAdministratorIdentityRoles();
    }
}

