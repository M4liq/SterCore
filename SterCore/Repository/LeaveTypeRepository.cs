using leave_management.Contracts;
using leave_management.Data;
using leave_management.Services.Components.ORI;
using leave_management.Services.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveTypeRepository : ILeaveTypeRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager _organizationManager;

        public LeaveTypeRepository(ApplicationDbContext db, IOrganizationResourceManager organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        } 

        public async Task<bool> Create(LeaveType entity)
        {

            //ORI separating data beetween organizations
            entity.OrganizationToken = _organizationManager.GetOrganizationToken();

            await _db.LeaveTypes.AddAsync(entity); 
            return await Save();
        }

        public async Task<bool> Delete(LeaveType entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (entity.OrganizationToken != _organizationManager.GetOrganizationToken())
            {
                throw new UnauthorizedAccessException();
            }

            _db.LeaveTypes.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<LeaveType>> FindAll()
        {
            //ORI getting token to find organization scope
            var organizationToken = _organizationManager.GetOrganizationToken();

            //ORI Filtring leave types by their tokens to get scope
            var leaveTypes = _db.LeaveTypes
                .Where(q => q.OrganizationToken == organizationToken);

            return await leaveTypes.ToListAsync(); 
        }

        public async Task<LeaveType> FindById(int id)
        {
            //ORI getting token to find organization scope
            var organizationToken = _organizationManager.GetOrganizationToken();

            var leaveType = _db.LeaveTypes

                //ORI Filtring organizations by their tokens to get scope
                .Where(q => q.OrganizationToken == organizationToken);
                
            return await leaveType.FirstOrDefaultAsync(q => q.Id == id); ;
        }

        public ICollection<LeaveType> GetEmployeesByLeaveType(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Exists(int id)
        {
            var organizationToken = _organizationManager.GetOrganizationToken();

            var exists = await _db.LeaveTypes

                //ORI Filtring leave types by their tokens to get scope
                .Where(q => q.OrganizationToken == organizationToken)
                .AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(LeaveType entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (entity.OrganizationToken != _organizationManager.GetOrganizationToken())
            {
                throw new UnauthorizedAccessException();
            }

            _db.LeaveTypes.Update(entity);
            return await Save();
        }

    }
}
