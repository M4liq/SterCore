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
    public class TrainingCourseTypeRepository : ITrainingCourseTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public TrainingCourseTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(TrainingCourseType entity)
        {
            await _db.TrainingCourseTypes.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(TrainingCourseType entity)
        {
            _db.TrainingCourseTypes.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            if (await FindById(id) == null)
                return false;
            else
                return true;
        }

        public async Task<ICollection<TrainingCourseType>> FindAll()
        {
            var TrainingCourseType = await _db.TrainingCourseTypes.ToListAsync();
            return TrainingCourseType;
        }

        public async Task<TrainingCourseType> FindById(int id)
        {
            var TrainingCourseType = await _db.TrainingCourseTypes.FirstOrDefaultAsync(q => q.Id == id);
            return TrainingCourseType;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(TrainingCourseType entity)
        {
            _db.TrainingCourseTypes.Update(entity);
            return await Save();
        }
    }
}