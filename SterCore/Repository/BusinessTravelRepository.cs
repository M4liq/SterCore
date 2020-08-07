using leave_management.Contracts;
using leave_management.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class BusinessTravelRepository : IBusinessTravelRepository
    {
        private readonly ApplicationDbContext _db;

        public BusinessTravelRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(BusinessTravel entity)
        {
            await _db.BusinessTravel.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(BusinessTravel entity)
        {
            _db.BusinessTravel.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            var exists = await _db.BusinessTravel.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<ICollection<BusinessTravel>> FindAll()
        {
            var BusinessTravel = await _db.BusinessTravel.ToListAsync();
            return BusinessTravel;
        }

        public async Task<BusinessTravel> FindById(int id)
        {
            var BusinessTravel = await _db.BusinessTravel.FirstOrDefaultAsync(q => q.Id == id);
            return BusinessTravel;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(BusinessTravel entity)
        {
            _db.BusinessTravel.Update(entity);
            return await Save();
        }
    }
}
