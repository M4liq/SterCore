using leave_management.Contracts;
using leave_management.Data;
using leave_management.Repository;
using leave_management.Services.Components.ORI;
using leave_management.Services.Extensions;
using leave_management.Services.ORI.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using leave_management.Helpers.Enums;

namespace leave_management.Services.Components
{
    public class OrganizationResourceManager : IOrganizationResourceManager
    {
        private readonly IHttpContextAccessor _session;
        private readonly ApplicationDbContext _db;

        public OrganizationResourceManager(IHttpContextAccessor session, ApplicationDbContext db)
        {
            _session = session;
            _db = db;
        }

        public async Task<AuthorizedOrganizations> Authorize(string ogranizationToken)
        {
            var authorize = new AuthorizedOrganizations
            {
                AuthorizedOrganizationToken = ogranizationToken
            };

            //requires to save
            await _db.AuthorizedOrganizations.AddAsync(authorize);
            return authorize;
    }

        public string GenerateToken()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");

            return GuidString;
        }

        public async Task<int> GetAuthorizedOrganizationId(string token)
        {
            var authorizedOrganization = 
                await _db.AuthorizedOrganizations
                .Where(q => q.AuthorizedOrganizationToken == token)
                .FirstOrDefaultAsync();
            
         //In case authorizaton is not set
            if (authorizedOrganization == null)
                return -1;

            return authorizedOrganization.Id ;
        }

        public async Task<Organization> GetCurrentOrganization()
        {
            var token = GetOrganizationToken();
            var organization = await _db.Organization
                .Where(q => q.OrganizationToken == token)
                .FirstOrDefaultAsync();
            return organization;
        }

        public string GetOrganizationToken()
        {
            return _session.ExtGet<string>("organizationToken");
        }
        public string GetDepartmentToken()
        {
            return _session.ExtGet<string>("departmentToken");
        }

        public bool HasPrivilegeGranted()
        {
            return _session.HttpContext.User.IsInRole(RoleEnum.Administrator);
        }

    }

    public class OrganizationResourceManager<T> : IOrganizationResourceManager<T> 
        where T : class, IApplicationDataView

    {
        private readonly IHttpContextAccessor _session;
        private readonly ApplicationDbContext _db;

        public OrganizationResourceManager(IHttpContextAccessor session, ApplicationDbContext db)
        {
            _session = session;
            _db = db;
        }


        public IQueryable<T> FilterDbSetByView(DbSet<T> dbSet)
        {
            var organizationToken = GetOrganizationToken();
            var departmentToken = GetDepartmentToken();
            return dbSet.Where(q => q.OrganizationToken == organizationToken && q.DepartmentToken == departmentToken);
        }

        public bool VerifyAccess(T entity)
        {   
            var organizationToken = GetOrganizationToken();
            var departmentToken = GetDepartmentToken();

            if (entity.OrganizationToken == null || entity.DepartmentToken == null)
                throw new Exception("Token is null. Check if your model implements OrganizationToken and DepartmentToken.");

            return entity.OrganizationToken == organizationToken && entity.DepartmentToken == departmentToken;
        }

        public T SetAccess(T entity)
        {
            entity.OrganizationToken = GetOrganizationToken();
            entity.DepartmentToken = GetDepartmentToken();
            return entity;
        }

        public string GenerateToken()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");

            return GuidString;
        }

        public async Task<int> GetAuthorizedOrganizationId(string token)
        {
            var authorizedOrganization =
                await _db.AuthorizedOrganizations
                .Where(q => q.AuthorizedOrganizationToken == token)
                .FirstOrDefaultAsync();

            //In case authorizaton is not set
            if (authorizedOrganization == null)
                return -1;

            return authorizedOrganization.Id;
        }

        public async Task<int> GetAuthorizedDepartmentId(string token)
        { 
                var authorizedOrganization =
                await _db.AuthorizedDepartments
                .Where(q => q.AuthorizedDepartmentToken == token)
                .FirstOrDefaultAsync();

            //In case authorizaton is not set
            if (authorizedOrganization == null)
                return -1;

            return authorizedOrganization.Id;
        }

        public async Task<Organization> GetCurrentOrganization()
        {
            var token = GetOrganizationToken();
            var organization = await _db.Organization.Where(q => q.OrganizationToken == token).FirstOrDefaultAsync();
            return organization;
        }

        public string GetOrganizationToken()
        {
            return _session.ExtGet<string>("organizationToken");
        }

        public string GetDepartmentToken()
        {
            return _session.ExtGet<string>("departmentToken");
        }

        public bool HasPrivilegeGranted()
        {
            return _session.HttpContext.User.IsInRole(RoleEnum.Administrator);
        }

    }
}
