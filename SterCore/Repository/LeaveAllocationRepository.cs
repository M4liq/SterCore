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
    public class LeaveAllocationRepository : ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager<LeaveAllocations> _organizationManager;

        public LeaveAllocationRepository(ApplicationDbContext db, IOrganizationResourceManager<LeaveAllocations> organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }

        public async Task<bool> CheckAllocation(int leavetypeid, string employeeid)
        {
            
            var period = DateTime.Now.Year;

            //ORI filtering in find all 
            var leaveAllocations =  await FindAll();

            return leaveAllocations
                .Where(q => q.EmployeeId == employeeid && q.LeaveTypeId == leavetypeid && q.Period == period).Any();
        }

        public async Task<bool> Create(LeaveAllocations entity)
        {
            _organizationManager.SetAccess(entity);

            await _db.LeaveAllocations.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(LeaveAllocations entity)
        {
            _organizationManager.VerifyAccess(entity);
            _db.LeaveAllocations.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<LeaveAllocations>> FindAll()
        {
            var LeaveAllocations = await _organizationManager.FilterDbSetByView(_db.LeaveAllocations)
                .Include(q => q.LeaveType)
                .Include(q => q.Employee)
                .ToListAsync();

            return LeaveAllocations;
        }

        public async Task<LeaveAllocations> FindById(int id)
        {
            var LeaveAllocation = await _organizationManager.FilterDbSetByView(_db.LeaveAllocations)
                .Include(q => q.LeaveType)
                .Include(q => q.Employee)
                .FirstOrDefaultAsync(q => q.Id == id);

            return LeaveAllocation;
        }

        public async Task<ICollection<LeaveAllocations>> GetLeaveAllocationsByEmployee(string id)
        {
            var period = DateTime.Now.Year;
            var leaveAllocations = await FindAll();
            return leaveAllocations
                .Where(q => q.EmployeeId == id && q.Period == period)
                .ToList();
        }

        public async Task<LeaveAllocations> GetLeaveAllocationsByEmployeeAndType(string id, int leavetypeid)
        {
            //ORI implemented in find all
            var period = DateTime.Now.Year;
            var allocations = await FindAll();
            return allocations.FirstOrDefault(q => q.EmployeeId == id && q.Period == period && q.LeaveTypeId == leavetypeid);
        }

        public async Task<bool> Exists(int id)
        {
            if (await FindById(id) == null)
                return false;
            else
                return true;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(LeaveAllocations entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }

            _db.LeaveAllocations.Update(entity);
            return await Save();
        }
    }
}
