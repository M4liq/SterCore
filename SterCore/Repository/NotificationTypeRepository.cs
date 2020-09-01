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
    public class NotificationTypeRepository : INotificationTypeRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager<NotificationType> _organizationManager;
        public NotificationTypeRepository(ApplicationDbContext db, IOrganizationResourceManager<NotificationType> organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }
        public async Task<bool> Create(NotificationType entity)
        {
            _organizationManager.SetAccess(entity);
            await _db.NotificationTypes.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(NotificationType entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }

            _db.NotificationTypes.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            if (await FindById(id) == null)
                return false;
            else
                return true;
        }

        public async Task<ICollection<NotificationType>> FindAll()
        {
            var NotificationType = _organizationManager.FilterDbSetByView(_db.NotificationTypes);
            return await NotificationType.ToListAsync();
        }

        public async Task<NotificationType> FindById(int id)
        {
            var NotificationType = _organizationManager.FilterDbSetByView(_db.NotificationTypes);
            return await NotificationType.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public void SetToken(NotificationType entity)
        {
            var token = _organizationManager.GetOrganizationToken();
            entity.OrganizationToken = token;
        }

        public async Task<bool> Update(NotificationType entity)
        {
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }
            _db.NotificationTypes.Update(entity);
            return await Save();
        }
    }
}
