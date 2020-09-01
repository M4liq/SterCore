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
    public class ContractTypeRepository : IContractTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public ContractTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(ContractType entity)
        {
            await _db.ContractTypes.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(ContractType entity)
        {
            _db.ContractTypes.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            if (await FindById(id) == null)
                return false;
            else
                return true;
        }

        public async Task<ICollection<ContractType>> FindAll()
        {
            var ContractType = await _db.ContractTypes.ToListAsync();
            return ContractType;
        }

        public async Task<ContractType> FindById(int id)
        {
            var ContractType = await _db.ContractTypes.FirstOrDefaultAsync(q => q.Id == id);
            return ContractType;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(ContractType entity)
        {
            _db.ContractTypes.Update(entity);
            return await Save();
        }
    }
}