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
    public class WorkTimeScheduleEmployeesRepository : IWorkTimeScheduleEmployeesRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager<WorkTimeScheduleEmployee> _organizationManager;

        public WorkTimeScheduleEmployeesRepository(ApplicationDbContext db, IOrganizationResourceManager<WorkTimeScheduleEmployee> organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }
        public async Task<bool> Create(WorkTimeScheduleEmployee entity)
        {
            _organizationManager.SetAccess(entity);
            await _db.WorkTimeScheduleEmployees.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(WorkTimeScheduleEmployee entity)
        {
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }
            _db.WorkTimeScheduleEmployees.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            if (await FindById(id) == null)
                return false;
            else
                return true;
        }

        public async Task<ICollection<WorkTimeScheduleEmployee>> FindAll()
        {
            var competences = _organizationManager.FilterDbSetByView(_db.WorkTimeScheduleEmployees);
            return await competences.ToListAsync();
        }

        public async Task<WorkTimeScheduleEmployee> FindById(int id)
        {
            var competences = _organizationManager.FilterDbSetByView(_db.WorkTimeScheduleEmployees);
            return await competences.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }



        public async Task<bool> Update(WorkTimeScheduleEmployee entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }
            _db.WorkTimeScheduleEmployees.Update(entity);
            return await Save();
        }
    }
}