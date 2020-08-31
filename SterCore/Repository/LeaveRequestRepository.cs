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
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager<LeaveRequests> _organizationManager;

        public LeaveRequestRepository(ApplicationDbContext db, IOrganizationResourceManager<LeaveRequests> organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }

        public async Task<bool> Create(LeaveRequests entity)
        {
            _organizationManager.SetAccess(entity);
            _db.LeaveRequests.Add(entity);
            return await Save();
        }

        public async Task<bool> Delete(LeaveRequests entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }

            _db.LeaveRequests.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<LeaveRequests>> FindAll()
        {
            //ORI getting token to find organization scope
            var organizationToken = _organizationManager.GetOrganizationToken();

            var LeaveRequests = await _organizationManager.FilterDbSetByView(_db.LeaveRequests)
                .Include(q=>q.ApprovedBy)
                .Include(q=>q.RequestingEmployee)
                .Include(q=>q.LeaveType)
                .ToListAsync();

            return LeaveRequests;
        }

        public async Task<LeaveRequests> FindById(int id)
        {
            var LeaveHistory = await _organizationManager.FilterDbSetByView(_db.LeaveRequests)
                .Include(q => q.ApprovedBy)
                .Include(q => q.RequestingEmployee)
                .Include(q => q.LeaveType)
                .FirstOrDefaultAsync(q => q.Id == id);

            return LeaveHistory;
        }

        public async Task<ICollection<LeaveRequests>> GetLeaveRequestsByEmployee(string id)
        {
            var LeaveRequests = await _organizationManager.FilterDbSetByView(_db.LeaveRequests)
                .Where(q => q.RequestingEmployeeId == id).ToListAsync();
            return LeaveRequests;
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

        public async Task<bool> Update(LeaveRequests entity)
        {

            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }

            _db.LeaveRequests.Update(entity);
            return await Save();
        }

    }
}
