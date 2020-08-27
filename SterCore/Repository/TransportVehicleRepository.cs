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
    public class TransportVehicleRepository : ITransportVehicleRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager _organizationManager;

        public TransportVehicleRepository(ApplicationDbContext db, IOrganizationResourceManager organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }
        public async Task<bool> Create(TransportVehicle entity)
        {
            await _db.TransportVehicles.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(TransportVehicle entity)
        {
            _db.TransportVehicles.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            var exists = await _db.TransportVehicles.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<ICollection<TransportVehicle>> FindAll()
        {
            var TransportVehicle = await _db.TransportVehicles.ToListAsync();
            return TransportVehicle;
        }

        public async Task<TransportVehicle> FindById(int id)
        {
            var TransportVehicle = await _db.TransportVehicles.FirstOrDefaultAsync(q => q.Id == id);
            return TransportVehicle;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(TransportVehicle entity)
        {
            _db.TransportVehicles.Update(entity);
            return await Save();
        }
    }
}
