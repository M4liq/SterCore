using leave_management.Contracts;
using leave_management.Data;
using leave_management.Services.Components.ORI;
using leave_management.Services.Extensions;
using leave_management.Services.ORI.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveTypeRepository : ILeaveTypeRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager<LeaveType> _organizationManager;

        public LeaveTypeRepository(ApplicationDbContext db, IOrganizationResourceManager<LeaveType> organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        } 

        public async Task<bool> Create(LeaveType entity)
        {

            //ORI separating data beetween organizations
            _organizationManager.SetAccess(entity);

            await _db.LeaveTypes.AddAsync(entity); 
            return await Save();
        }

        public async Task<bool> Delete(LeaveType entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }

            _db.LeaveTypes.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<LeaveType>> FindAll()
        {
            var leaveTypes = _organizationManager.FilterDbSetByView(_db.LeaveTypes);
            return await leaveTypes.ToListAsync(); 
        }

        public async Task<LeaveType> FindById(int id)
        {

            var leaveType = _organizationManager.FilterDbSetByView(_db.LeaveTypes);

            return await leaveType.FirstOrDefaultAsync(q => q.Id == id); ;
        }

        public ICollection<LeaveType> GetEmployeesByLeaveType(int id)
        {
            throw new NotImplementedException();
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

        public async Task<bool> Update(LeaveType entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }

            _db.LeaveTypes.Update(entity);
            return await Save();
        }

    }
}
