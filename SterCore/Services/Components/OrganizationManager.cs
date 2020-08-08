using leave_management.Contracts.IServiecies;
using leave_management.Data;
using leave_management.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Services.Components
{
    public class OrganizationManager : IOrganizationManager
    {
        public string GenerateToken()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");

            return GuidString;
        }
        private readonly ApplicationDbContext _db;

        public OrganizationManager(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Organization entity)
        {
            await _db.Organization.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Organization entity)
        {
            _db.Organization.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<Organization>> FindAll()
        {
            var organizations = await _db.Organization.ToListAsync();
            return organizations;
        }

        public async Task<Organization> FindById(int id)
        {
            var organization = await _db.Organization
            .FirstOrDefaultAsync(q => q.Id == id);
            return organization;
        }

        public async Task<bool> Exists(int id)
        {
            var exists = await _db.Organization.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Organization entity)
        {
            _db.Organization.Update(entity);
            return await Save();
        }
    }
}
