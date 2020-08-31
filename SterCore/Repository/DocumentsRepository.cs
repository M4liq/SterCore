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
    public class DocumentsRepository : IDocumentsRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager<Document> _organizationManager;

        public DocumentsRepository(ApplicationDbContext db, IOrganizationResourceManager<Document> organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }
        public async Task<bool> Create(Document entity)
        {
            _organizationManager.SetAccess(entity);

            await _db.Documents.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Document entity)
        {
            //ORI checking if data is from appropirate organization scope
            if ( _organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }

            _db.Documents.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            if (await FindById(id) == null)
                return false;
            else
                return true;
        }

        public async Task<ICollection<Document>> FindAll()
        {
            //ORI filtering by token
            var Document = await _organizationManager.FilterDbSetByView(_db.Documents)   
                .ToListAsync();
            return Document;
        }

        public async Task<Document> FindById(int id)
        {
            var Document = await _organizationManager.FilterDbSetByView(_db.Documents)
                .FirstOrDefaultAsync(q => q.Id == id);

            return Document;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Document entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }

           _db.Documents.Update(entity);
            return await Save();
        }
    }
}
