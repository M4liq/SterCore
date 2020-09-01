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
    public class CompetenceRepository : ICompetenceRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager<Competence> _organizationManager;

        public CompetenceRepository(ApplicationDbContext db, IOrganizationResourceManager<Competence> organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }
        public async Task<bool> Create(Competence entity)
        {
            _organizationManager.SetAccess(entity);
            await _db.Competences.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Competence entity)
        {
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }
            _db.Competences.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            if (await FindById(id) == null)
                return false;
            else
                return true;
        }

        public async Task<ICollection<Competence>> FindAll()
        {
            var competences = _organizationManager.FilterDbSetByView(_db.Competences);
            return await competences.ToListAsync();
        }

        public async Task<Competence> FindById(int id)
        {
            var competences = _organizationManager.FilterDbSetByView(_db.Competences);
            return await competences.FirstOrDefaultAsync(q => q.Id == id); ;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public void SetToken(Competence entity)
        {
            var token = _organizationManager.GetOrganizationToken();
            entity.OrganizationToken = token;
        }

        public async Task<bool> Update(Competence entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }

            _db.Competences.Update(entity);
            return await Save();
        }
    }
}