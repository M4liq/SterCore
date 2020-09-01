using leave_management.Contracts;
using leave_management.Services.Components.ORI;
using leave_management.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class ContractRepository : IContractRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager<Contract> _organizationManager;
        public ContractRepository(ApplicationDbContext db, IOrganizationResourceManager<Contract> organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }
        public async Task<bool> Create(Contract entity)
        {
            _organizationManager.SetAccess(entity);
            await _db.Contracts.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Contract entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }

            _db.Contracts.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            if (await FindById(id) == null)
                return false;
            else
                return true;
        }

        public async Task<ICollection<Contract>> FindAll()
        {
            var Contract = _organizationManager.FilterDbSetByView(_db.Contracts);
            return await Contract.Include(q => q.Employee).ToListAsync();
        }

        public async Task<Contract> FindById(int id)
        {
            var Contract = _organizationManager.FilterDbSetByView(_db.Contracts);
            return await Contract.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Contract entity)
        {
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }
            _db.Contracts.Update(entity);
            return await Save();
        }
    }
}