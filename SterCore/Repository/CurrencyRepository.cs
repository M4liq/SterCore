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
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager _organizationManager;

        public CurrencyRepository(ApplicationDbContext db, IOrganizationResourceManager organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }
        public async Task<bool> Create(Currency entity)
        {
            await _db.Currencies.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Currency entity)
        {
            _db.Currencies.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            var exists = await _db.Currencies.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<ICollection<Currency>> FindAll()
        {
            var Currency = await _db.Currencies.ToListAsync();
            return Currency;
        }

        public async Task<Currency> FindById(int id)
        {
            var Currency = await _db.Currencies.FirstOrDefaultAsync(q => q.Id == id);
            return Currency;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }


        public async Task<bool> Update(Currency entity)
        {
            _db.Currencies.Update(entity);
            return await Save();
        }
    }
}
