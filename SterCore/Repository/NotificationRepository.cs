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
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager<Notification> _organizationManager;
        public NotificationRepository(ApplicationDbContext db, IOrganizationResourceManager<Notification> organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }
        public async Task<bool> Create(Notification entity)
        {
            _organizationManager.SetAccess(entity);
            await _db.Notifications.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Notification entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }

            _db.Notifications.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            if (await FindById(id) == null)
                return false;
            else
                return true;
        }

        public async Task<ICollection<Notification>> FindAll()
        {
            var Notification = _organizationManager.FilterDbSetByView(_db.Notifications);
            return await Notification.Include(q => q.Employee).ToListAsync();
        }

        public async Task<Notification> FindById(int id)
        {
            var Notification = _organizationManager.FilterDbSetByView(_db.Notifications);
            return await Notification.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Notification entity)
        {
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }
            _db.Notifications.Update(entity);
            return await Save();
        }
    }
}