using leave_management.Contracts;
using leave_management.Data;
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

        public LeaveRequestRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(LeaveRequests entity)
        {
            _db.LeaveRequests.Add(entity);
            return await Save();
        }

        public async Task<bool> Delete(LeaveRequests entity)
        {
            _db.LeaveRequests.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<LeaveRequests>> FindAll()
        {
            var LeaveRequests = await _db.LeaveRequests
                .Include(q=>q.ApprovedBy)
                .Include(q=>q.RequestingEmployee)
                .Include(q=>q.LeaveType)
                .ToListAsync();
            return LeaveRequests;
        }

        public async Task<LeaveRequests> FindById(int id)
        {
            var LeaveHistory = await _db.LeaveRequests
                .Include(q => q.ApprovedBy)
                .Include(q => q.RequestingEmployee)
                .Include(q => q.LeaveType)
                .FirstOrDefaultAsync(q => q.Id == id);

            return LeaveHistory;
        }

        public async Task<ICollection<LeaveRequests>> GetLeaveRequestsByEmployee(string id)
        {
            var LeaveRequests = await _db.LeaveRequests.Where(q => q.RequestingEmployeeId == id).ToListAsync();
            return LeaveRequests;
        }

        public async Task<bool> Exists(int id)
        {
            var exists = await _db.LeaveRequests.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(LeaveRequests entity)
        {
            _db.LeaveRequests.Update(entity);
            return await Save();
        }
    }
}
