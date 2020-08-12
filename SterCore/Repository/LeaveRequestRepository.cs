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
        private readonly IOrganizationResourceManager _organizationManager;

        public LeaveRequestRepository(ApplicationDbContext db, IOrganizationResourceManager organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }

        public async Task<bool> Create(LeaveRequests entity)
        {
            //ORI separating data beetween organizations
            entity.OrganizationToken = _organizationManager.GetOrganizationToken();

            _db.LeaveRequests.Add(entity);
            return await Save();
        }

        public async Task<bool> Delete(LeaveRequests entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (entity.OrganizationToken != _organizationManager.GetOrganizationToken())
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

            var LeaveRequests = await _db.LeaveRequests
                .Include(q=>q.ApprovedBy)
                .Include(q=>q.RequestingEmployee)
                .Include(q=>q.LeaveType)

                //ORI Filtring organizations by their tokens to get scope
                .Where(q => q.OrganizationToken == organizationToken)
                .ToListAsync();

            return LeaveRequests;
        }

        public async Task<LeaveRequests> FindById(int id)
        {
            //ORI getting token to find organization scope
            var organizationToken = _organizationManager.GetOrganizationToken();

            var LeaveHistory = await _db.LeaveRequests
                .Include(q => q.ApprovedBy)
                .Include(q => q.RequestingEmployee)
                .Include(q => q.LeaveType)

                //ORI Filtring organizations by their tokens to get scope
                .Where(q => q.OrganizationToken == organizationToken)

                .FirstOrDefaultAsync(q => q.Id == id);

            return LeaveHistory;
        }

        public async Task<ICollection<LeaveRequests>> GetLeaveRequestsByEmployee(string id)
        {
            //ORI getting token to find organization scope
            var organizationToken = _organizationManager.GetOrganizationToken();

            var LeaveRequests = await _db.LeaveRequests.Where(q => q.RequestingEmployeeId == id && q.OrganizationToken == organizationToken).ToListAsync();
            return LeaveRequests;
        }

        public async Task<bool> Exists(int id)
        {
            var organizationToken = _organizationManager.GetOrganizationToken();

            var exists = await _db.LeaveRequests

                //ORI Filtring organizations by their tokens to get scope
                .Where(q => q.OrganizationToken == organizationToken)
                .AnyAsync(q => q.Id == id);

            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(LeaveRequests entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (entity.OrganizationToken != _organizationManager.GetOrganizationToken())
            {
                throw new UnauthorizedAccessException();
            }

            _db.LeaveRequests.Update(entity);
            return await Save();
        }

        public void SetToken(LeaveRequests entity)
        {
            var token = _organizationManager.GetOrganizationToken();
            entity.OrganizationToken = token;
        }
    }
}
