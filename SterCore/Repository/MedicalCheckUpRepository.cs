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
    public class MedicalCheckUpRepository : IMedicalCheckUpRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager<MedicalCheckUp> _organizationManager;
        public MedicalCheckUpRepository(ApplicationDbContext db, IOrganizationResourceManager<MedicalCheckUp> organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }
        public async Task<bool> Create(MedicalCheckUp entity)
        {
            _organizationManager.SetAccess(entity);
            await _db.MedicalCheckUps.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(MedicalCheckUp entity)
        {
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }
            _db.MedicalCheckUps.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            if (await FindById(id) == null)
                return false;
            else
                return true;
        }

        public async Task<ICollection<MedicalCheckUp>> FindAll()
        {
            var MedicalCheckUps = _organizationManager.FilterDbSetByView(_db.MedicalCheckUps);
            return await MedicalCheckUps
                .Include(q => q.Employee)
                .Include(q => q.typeOfMedicalCheckUp)
                .ToListAsync();
        }

        public async Task<MedicalCheckUp> FindById(int id)
        {
            var MedicalCheckUp = _organizationManager.FilterDbSetByView(_db.MedicalCheckUps);
            return await MedicalCheckUp.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }


        public async Task<bool> Update(MedicalCheckUp entity)
        {
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }
            _db.MedicalCheckUps.Update(entity);
            return await Save();
        }
    }
}
