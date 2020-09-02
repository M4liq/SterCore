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
    public class TrainingCourseRepository : ITrainingCourseRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager<TrainingCourse> _organizationManager;
        public TrainingCourseRepository(ApplicationDbContext db, IOrganizationResourceManager<TrainingCourse> organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }
        public async Task<bool> Create(TrainingCourse entity)
        {
            _organizationManager.SetAccess(entity);
            await _db.TrainingCourses.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(TrainingCourse entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }

            _db.TrainingCourses.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            if (await FindById(id) == null)
                return false;
            else
                return true;
        }

        public async Task<ICollection<TrainingCourse>> FindAll()
        {
            var TrainingCourse = _organizationManager.FilterDbSetByView(_db.TrainingCourses);
            return await TrainingCourse.Include(q => q.Employee).ToListAsync();
        }

        public async Task<TrainingCourse> FindById(int id)
        {
            var TrainingCourse = _organizationManager.FilterDbSetByView(_db.TrainingCourses);
            return await TrainingCourse.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(TrainingCourse entity)
        {
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }
            _db.TrainingCourses.Update(entity);
            return await Save();
        }
    }
}