﻿using leave_management.Contracts;
using leave_management.Data;
using leave_management.Services.Components.ORI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class TypeOfMedicalCheckUpRepository : ITypeOfMedicalCheckUpRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager _organizationManager;
        public TypeOfMedicalCheckUpRepository(ApplicationDbContext db, IOrganizationResourceManager organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }

        public async Task<bool> Create(TypeOfMedicalCheckUp entity)
        {
            await _db.TypeOfMedicalCheckUps.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(TypeOfMedicalCheckUp entity)
        {
            _db.TypeOfMedicalCheckUps.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            var exists = await _db.TypeOfMedicalCheckUps
                .AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<ICollection<TypeOfMedicalCheckUp>> FindAll()
        {
            var TypeOfMedicalCheckUps = await _db.TypeOfMedicalCheckUps
                .ToListAsync();
            return TypeOfMedicalCheckUps;
        }

        public async Task<TypeOfMedicalCheckUp> FindById(int id)
        {

            var TypeOfMedicalCheckUp = await _db.TypeOfMedicalCheckUps
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
            //nothing is here
        }

        public async Task<bool> Update(TypeOfMedicalCheckUp entity)
        {

            _db.TypeOfMedicalCheckUps.Update(entity);
            return await Save();
        }
    }
}
