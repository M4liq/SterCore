using leave_management.Contracts;
using leave_management.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class PWSRepository : IPWSRepository
    {
        private readonly ApplicationDbContext _db;

        public PWSRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(PWS entity)
        {
            await _db.PWS.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(PWS entity)
        {
            _db.PWS.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            var exists = await _db.PWS.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<ICollection<PWS>> FindAll()
        {
            var PWS = await _db.PWS.ToListAsync();
            return PWS;
        }

        public async Task<PWS> FindById(int id)
        {
            var PWS = await _db.PWS.FirstOrDefaultAsync(q => q.Id == id);
            return PWS;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(PWS entity)
        {
            _db.PWS.Update(entity);
            return await Save();
        }
    }
}
