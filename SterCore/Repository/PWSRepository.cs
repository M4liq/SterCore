using leave_management.Contracts;
using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class PWSRepository : IPWSRepository
    {
        private readonly ApplicationDbContext _db;

        public PWSRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public Task<bool> Create(PWS entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(PWS entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<PWS>> FindAll()
        {
            throw new NotImplementedException();
        }

        public Task<PWS> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(PWS entity)
        {
            throw new NotImplementedException();
        }
    }
}
