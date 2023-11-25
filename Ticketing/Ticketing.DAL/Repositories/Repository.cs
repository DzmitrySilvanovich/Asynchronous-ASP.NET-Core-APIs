using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.DAL.Contracts;
using Ticketing.DAL.Domain;

namespace Ticketing.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationContext _db;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationContext context)
        {
            _db = context;
            _dbSet = _db.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            EntityEntry<T> addedEentity = await _dbSet.AddAsync(entity);

            await _db.SaveChangesAsync();
            return addedEentity.Entity;
        }

        public Task<IQueryable<T>> GetAllAsync()
        {
            return Task.FromResult(_dbSet.AsQueryable());
        }

        public async Task<T?> GetByIdAsync(object Id)
        {
            return await _db.FindAsync((Type)Id) as T;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAync(object Id)
        {
            if (await _db.FindAsync((Type)Id) is T entity)
            {
                await DeleteAync(entity);
            }
        }

        public async Task DeleteAync(T entity)
        {
            _dbSet.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}
