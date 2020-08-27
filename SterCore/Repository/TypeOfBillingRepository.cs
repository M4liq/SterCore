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
    public class TypeOfBillingRepository : ITypeOfBillingRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager _organizationManager;

        public TypeOfBillingRepository(ApplicationDbContext db, IOrganizationResourceManager organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }
        public async Task<bool> Create(TypeOfBilling entity)
        {
            await _db.TypeOfBillings.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(TypeOfBilling entity)
        {
            _db.TypeOfBillings.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            var exists = await _db.TypeOfBillings.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<ICollection<TypeOfBilling>> FindAll()
        {
            var TypeOfBilling = await _db.TypeOfBillings.ToListAsync();
            return TypeOfBilling;
        }

        public async Task<TypeOfBilling> FindById(int id)
        {
            var TypeOfBilling = await _db.TypeOfBillings.FirstOrDefaultAsync(q => q.Id == id);
            return TypeOfBilling;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(TypeOfBilling entity)
        {
            _db.TypeOfBillings.Update(entity);
            return await Save();
        }
    }
}
