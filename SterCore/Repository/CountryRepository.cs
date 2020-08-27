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
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager _organizationManager;

        public CountryRepository(ApplicationDbContext db, IOrganizationResourceManager organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }
        public async Task<bool> Create(Country entity)
        {
            await _db.Countries.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Country entity)
        {
            _db.Countries.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            var exists = await _db.Countries.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<ICollection<Country>> FindAll()
        {
          
            var Country = await _db.Countries
                .ToListAsync();
            return Country;
        }

        public async Task<Country> FindById(int id)
        {
            var Country = await _db.Countries
                .FirstOrDefaultAsync(q => q.Id == id);

            return Country;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Country entity)
        {
            _db.Countries.Update(entity);
            return await Save();
        }
    }
}
