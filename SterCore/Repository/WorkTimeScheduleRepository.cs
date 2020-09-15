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
    public class WorkTimeScheduleRepository : IWorkTimeScheduleRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager<WorkTimeSchedule> _organizationManager;
        public WorkTimeScheduleRepository(ApplicationDbContext db, IOrganizationResourceManager<WorkTimeSchedule> organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }
        public async Task<bool> Create(WorkTimeSchedule entity)
        {
            _organizationManager.SetAccess(entity);
            await _db.WorkTimeSchedules.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(WorkTimeSchedule entity)
        {
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }
            _db.WorkTimeSchedules.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            if (await FindById(id) == null)
                return false;
            else
                return true;
        }

        public async Task<ICollection<WorkTimeSchedule>> FindAll()
        {
            var competences = _organizationManager.FilterDbSetByView(_db.WorkTimeSchedules);
            return await competences.ToListAsync();
        }

        public async Task<WorkTimeSchedule> FindById(int id)
        {
            var competences = _organizationManager.FilterDbSetByView(_db.WorkTimeSchedules);
            return await competences.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }



        public async Task<bool> Update(WorkTimeSchedule entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }
            _db.WorkTimeSchedules.Update(entity);
            return await Save();
        }
    }
}