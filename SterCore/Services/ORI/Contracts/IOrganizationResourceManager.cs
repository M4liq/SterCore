using leave_management.Data;
using leave_management.Services.ORI.Contracts;
using Microsoft.EntityFrameworkCore;
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
            public string GetDepartmentToken();

            public Task<int> GetAuthorizedOrganizationId(string token);

            public Task<Organization> GetCurrentOrganization();

            public Task<AuthorizedOrganizations> Authorize(string ogranizationToken);

            public bool HasPrivilegeGranted();

        }

    public interface IOrganizationResourceManager<T>
        where T : class, IApplicationDataView
    {
        public string GenerateToken();

        public string GetOrganizationToken();

        public string GetDepartmentToken();

        public Task<int> GetAuthorizedOrganizationId(string token);
        public Task<int> GetAuthorizedDepartmentId(string token);

        public Task<Organization> GetCurrentOrganization();

        public bool HasPrivilegeGranted();

        public IQueryable<T> FilterDbSetByView(DbSet<T> dbSet);

        public bool VerifyAccess(T entity);

        public T SetAccess(T entity);

    }

}
