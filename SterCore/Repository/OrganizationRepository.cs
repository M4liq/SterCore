using leave_management.Contracts;
using leave_management.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly ApplicationDbContext _db;

        public OrganizationRepository(ApplicationDbContext db)
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
            var organization= await _db.Organization
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

        public Task<Organization> GetOrganizationByUserId(int id)
        {
            throw new NotImplementedException(); //to do some logic here 
        }
    }
}
