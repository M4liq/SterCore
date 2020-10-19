﻿using leave_management.Contracts;
using leave_management.Data;
using leave_management.Services.Components.ORI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class WorkTimeScheduleEventRepository : IWorkTimeScheduleEventRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager<WorkTimeScheduleEvent> _organizationManager;
        public WorkTimeScheduleEventRepository(ApplicationDbContext db, IOrganizationResourceManager<WorkTimeScheduleEvent> organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }
        public async Task<bool> Create(WorkTimeScheduleEvent entity)
        {
            _organizationManager.SetAccess(entity);
            await _db.WorkTimeScheduleEvents.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(WorkTimeScheduleEvent entity)
        {
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }
            _db.WorkTimeScheduleEvents.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            if (await FindById(id) == null)
                return false;
            else
                return true;
        }

        public async Task<ICollection<WorkTimeScheduleEvent>> FindAll()
        {
            var WorkTimeScheduleEvents = _organizationManager.FilterDbSetByView(_db.WorkTimeScheduleEvents);
            return await WorkTimeScheduleEvents.ToListAsync();
        }

        public async Task<ICollection<WorkTimeScheduleEvent>> FindBySchedulerId(int id)
        {
            var WorkTimeScheduleEvents = _organizationManager.FilterDbSetByView(_db.WorkTimeScheduleEvents);
            return await WorkTimeScheduleEvents.Where(q => q.SchedulerId == id).ToListAsync();
        }
        public async Task<bool> RemoveBySchedulerId(int id)
        {
            var WorkTimeScheduleEvents = _organizationManager.FilterDbSetByView(_db.WorkTimeScheduleEvents);
            ICollection<WorkTimeScheduleEvent> list = await WorkTimeScheduleEvents.ToListAsync();
            IEnumerable<WorkTimeScheduleEvent> listOfEvents = list.Where(q => q.SchedulerId == id);
            foreach (var item in listOfEvents)
            {
                _db.WorkTimeScheduleEvents.Remove(item);
            }
            return await Save();
        }

        public async Task<WorkTimeScheduleEvent> FindById(int id)
        {
            var WorkTimeScheduleEvents = _organizationManager.FilterDbSetByView(_db.WorkTimeScheduleEvents);
            return await WorkTimeScheduleEvents.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }



        public async Task<bool> Update(WorkTimeScheduleEvent entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }
            _db.WorkTimeScheduleEvents.Update(entity);
            return await Save();
        }
    }
}