using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Services.Components.ORI;
using Microsoft.EntityFrameworkCore;

namespace leave_management.Repository
{
    public class ExplicitLeaveRepository : IExplicitLeaveTypeRepository
    {
        private readonly IOrganizationResourceManager<ExplicitLeaveTypes> _organizationResourceManager;
        private readonly ApplicationDbContext _db;

        public ExplicitLeaveRepository(IOrganizationResourceManager<ExplicitLeaveTypes> organizationResourceManager, 
            ApplicationDbContext db)
        {
            _organizationResourceManager = organizationResourceManager;
            _db = db;
        }

        public async Task<bool> Create(ExplicitLeaveTypes entity)
        {
            entity.DateCreated = DateTime.Now;
            await _db.ExplicitLeaveTypes.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(ExplicitLeaveTypes entity)
        {
            _db.ExplicitLeaveTypes.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<ExplicitLeaveTypes>> FindAll()
        {
            var leaveTypes = _db.ExplicitLeaveTypes;
            return await leaveTypes.ToListAsync();
        }

        public async Task<ExplicitLeaveTypes> FindById(int id)
        {
            var leaveType = _db.ExplicitLeaveTypes;
            return await leaveType.FirstOrDefaultAsync(q => q.Id == id); ;
        }

        public async Task<bool> Exists(int id)
        {
            return await FindById(id) != null;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(ExplicitLeaveTypes entity)
        {
            _db.ExplicitLeaveTypes.Update(entity);
            return await Save();
        }
    }
}
