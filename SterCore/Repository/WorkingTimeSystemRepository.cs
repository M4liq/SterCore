using leave_management.Contracts;
using leave_management.Services.Components.ORI;
using leave_management.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class WorkingTimeSystemRepository : IWorkingTimeSystemRepository
    {
        private readonly ApplicationDbContext _db;

        public WorkingTimeSystemRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(WorkingTimeSystem entity)
        {
            await _db.WorkingTimeSystems.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(WorkingTimeSystem entity)
        {
            _db.WorkingTimeSystems.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            if (await FindById(id) == null)
                return false;
            else
                return true;
        }

        public async Task<ICollection<WorkingTimeSystem>> FindAll()
        {
            var WorkingTimeSystem = await _db.WorkingTimeSystems.ToListAsync();
            return WorkingTimeSystem;
        }

        public async Task<WorkingTimeSystem> FindById(int id)
        {
            var WorkingTimeSystem = await _db.WorkingTimeSystems.FirstOrDefaultAsync(q => q.Id == id);
            return WorkingTimeSystem;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(WorkingTimeSystem entity)
        {
            _db.WorkingTimeSystems.Update(entity);
            return await Save();
        }
    }
}