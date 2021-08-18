using nursat.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nursat.Repository
{
    public class Repository<Child> : IRepository<Child> where Child : class, new()
    {
        private readonly AppDb db;

        public Repository(AppDb _db)
        {
            db = _db;
        }

        

        public Task<Child> AddAsync(Child entity)
        {
            throw new NotImplementedException();
        }

        public Child Delete(Child entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Child> Get()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Child> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Child> UpdateAsync(Child entity)
        {
            throw new NotImplementedException();
        }
    }
}
