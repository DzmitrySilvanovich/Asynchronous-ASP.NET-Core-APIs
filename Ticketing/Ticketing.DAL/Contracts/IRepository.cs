using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ticketing.DAL.Domain;

namespace Ticketing.DAL.Contracts
{
    public interface IRepository<T>
    {
        public Task<T> CreateAsync(T entity);

        public Task<IEnumerable<T>> GetAllAsync();

        public Task<T?> GetByIdAsync(object Id);

        public Task UpdateAsync(T entity);

        public Task DeleteAync(object Id);

        public Task DeleteAync(T entity);
    }
}
