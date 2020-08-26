using leave_management.Contracts;
using leave_management.Data;
using leave_management.Services.Components.ORI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class AuthorizedOrganizationRepository : IAuthorizedOrganizationRepository
    {
        private readonly ApplicationDbContext _db;

        public AuthorizedOrganizationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(AuthorizedOrganizations entity)
        {
            await _db.AuthorizedOrganizations.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(AuthorizedOrganizations entity)
        {
            _db.AuthorizedOrganizations.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<AuthorizedOrganizations>> FindAll()
        {
            var organizations = _db.AuthorizedOrganizations;
            return  await organizations.ToListAsync();

        }

        public async Task<AuthorizedOrganizations> FindById(int id)
        {
            var organization = _db.AuthorizedOrganizations;

            return await organization.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<bool> Exists(int id)
        {

            var exists = await _db.AuthorizedOrganizations.AnyAsync(q => q.Id == id);

            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(AuthorizedOrganizations entity)
        {
            _db.AuthorizedOrganizations.Update(entity);
            return await Save();
        }

    }
}
