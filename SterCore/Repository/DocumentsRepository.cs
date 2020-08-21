using leave_management.Contracts;
using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class DocumentsRepository : IDocumentsRepository
    {
        public Task<bool> Create(Document entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Document entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Document>> FindAll()
        {
            throw new NotImplementedException();
        }

        public Task<Document> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }

        public void SetToken(Document entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Document entity)
        {
            throw new NotImplementedException();
        }
    }
}
