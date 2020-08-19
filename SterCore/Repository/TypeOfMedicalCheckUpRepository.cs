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
    public class TypoeOfMedicalCheckUpRepository : ITypeOfMedicalCheckUpRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager _organizationManager;
        public TypoeOfMedicalCheckUpRepository(ApplicationDbContext db, IOrganizationResourceManager organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }
        public async Task<bool> Create(TypeOfMedicalCheckUp entity)
        {
            //ORI separating data beetween organizations
            entity.OrganizationToken = _organizationManager.GetOrganizationToken();

            await _db.TypeOfMedicalCheckUps.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(TypeOfMedicalCheckUp entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (entity.OrganizationToken != _organizationManager.GetOrganizationToken())
            {
                throw new UnauthorizedAccessException();
            }

            _db.TypeOfMedicalCheckUps.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            var organizationToken = _organizationManager.GetOrganizationToken();

            var exists = await _db.TypeOfMedicalCheckUps
                //ORI Filtring leave types by their tokens to get scope
                .Where(q => q.OrganizationToken == organizationToken)
                .AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<ICollection<TypeOfMedicalCheckUp>> FindAll()
        {
            //ORI getting token to find organization scope
            var organizationToken = _organizationManager.GetOrganizationToken();


            var TypeOfMedicalCheckUps = await _db.TypeOfMedicalCheckUps
                .Where(q => q.OrganizationToken == organizationToken)
                .ToListAsync();
            return TypeOfMedicalCheckUps;
        }

        public async Task<TypeOfMedicalCheckUp> FindById(int id)
        {
            //ORI getting token to find organization scope
            var organizationToken = _organizationManager.GetOrganizationToken();

            var TypeOfMedicalCheckUp = await _db.TypeOfMedicalCheckUps
                //ORI Filtring organizations by their tokens to get scope
                .Where(q => q.OrganizationToken == organizationToken)
                .FirstOrDefaultAsync(q => q.Id == id);
            return TypeOfMedicalCheckUp;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public void SetToken(TypeOfMedicalCheckUp entity)
        {
            var token = _organizationManager.GetOrganizationToken();
            entity.OrganizationToken = token;
        }

        public async Task<bool> Update(TypeOfMedicalCheckUp entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (entity.OrganizationToken != _organizationManager.GetOrganizationToken())
            {
                throw new UnauthorizedAccessException();
            }

            _db.TypeOfMedicalCheckUps.Update(entity);
            return await Save();
        }
    }
}
