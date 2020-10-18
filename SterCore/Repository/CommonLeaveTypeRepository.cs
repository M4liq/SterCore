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
    public class CommonLeaveTypeRepository : ICommonLeaveTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public CommonLeaveTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        } 

        public async Task<bool> Create(CommonLeaveTypes entity)
        {
            entity.DateCreated = DateTime.Now;
            await _db.CommonLeaveTypes.AddAsync(entity); 
            return await Save();
        }

        public async Task<bool> Delete(CommonLeaveTypes entity)
        {
            _db.CommonLeaveTypes.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<CommonLeaveTypes>> FindAll()
        {
            var leaveTypes = _db.CommonLeaveTypes;
            return await leaveTypes.ToListAsync(); 
        }

        public async Task<CommonLeaveTypes> FindById(int id)
        {
            var leaveType = _db.CommonLeaveTypes;
            return await leaveType.FirstOrDefaultAsync(q => q.Id == id); ;
        }

        public ICollection<CommonLeaveTypes> GetEmployeesByLeaveType(int id)
        {
            throw new NotImplementedException();
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

        public async Task<bool> Update(CommonLeaveTypes entity)
        {
            _db.CommonLeaveTypes.Update(entity);
            return await Save();
        }

    }
}
