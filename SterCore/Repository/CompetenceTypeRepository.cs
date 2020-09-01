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
    public class CompetenceTypeRepository : ICompetenceTypeRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager<CompetenceType> _organizationManager;

        public CompetenceTypeRepository(ApplicationDbContext db, IOrganizationResourceManager<CompetenceType> organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }
        public async Task<bool> Create(CompetenceType entity)
        {
            _organizationManager.SetAccess(entity);
            await _db.CompetenceTypes.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(CompetenceType entity)
        {
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }

            _db.CompetenceTypes.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            if (await FindById(id) == null)
                return false;
            else
                return true;
        }

        public async Task<ICollection<CompetenceType>> FindAll()
        {
            var CompetenceType = _organizationManager.FilterDbSetByView(_db.CompetenceTypes);
            return await CompetenceType.ToListAsync();
        }

        public async Task<CompetenceType> FindById(int id)
        {
            var CompetenceType = _organizationManager.FilterDbSetByView(_db.CompetenceTypes);
            return await CompetenceType.FirstOrDefaultAsync(q => q.Id == id); ;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }



        public async Task<bool> Update(CompetenceType entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }
            _db.CompetenceTypes.Update(entity);
            return await Save();
        }
    }
}
