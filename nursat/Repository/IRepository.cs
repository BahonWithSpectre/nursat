using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nursat.Repository
{
    public interface IRepository<Child>
    {
        IQueryable<Child> Get();

        IEnumerable<Child> GetAll();

        Task<Child> AddAsync(Child entity);

        Task<Child> UpdateAsync(Child entity);

        Child Delete(Child entity);
    }
}
