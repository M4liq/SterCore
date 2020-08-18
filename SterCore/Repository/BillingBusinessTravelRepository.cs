﻿using leave_management.Contracts;
using leave_management.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class BillingBusinessTravelRepository : IBillingBusinessTravelRepository
    {
        private readonly ApplicationDbContext _db;

        public BillingBusinessTravelRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(BillingBusinessTravel entity)
        {
            await _db.billingBusinessTravels.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(BillingBusinessTravel entity)
        {
            _db.billingBusinessTravels.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            var exists = await _db.billingBusinessTravels.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<ICollection<BillingBusinessTravel>> FindAll()
        {
            var BillingBusinessTravel = await _db.billingBusinessTravels
                .Include(q => q.Employee)
                .ToListAsync();
            return BillingBusinessTravel;
        }

        public async Task<BillingBusinessTravel> FindById(int id)
        {
            var BillingBusinessTravel = await _db.billingBusinessTravels.FirstOrDefaultAsync(q => q.Id == id);
            return BillingBusinessTravel;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(BillingBusinessTravel entity)
        {
            _db.billingBusinessTravels.Update(entity);
            return await Save();
        }
    }
}
