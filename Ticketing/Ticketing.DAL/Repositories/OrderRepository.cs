using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.DAL.Domain;
using Ticketing.DAL.Contracts;

namespace Ticketing.DAL.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        readonly ApplicationContext db;

        public OrderRepository(ApplicationContext context)
        {
            db = context;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            EntityEntry<Order> entity = await db.Orders.AddAsync(order);
            await db.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<IEnumerable<Order>> GetAllAsync() => await db.Orders.ToListAsync();

        public async Task<Order?> GetByIdAsync(object Id) => await db.FindAsync(typeof(Order), (int)Id) as Order;

        public async Task UpdateAsync(Order order)
        {
            db.Orders.Update(order);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAync(object Id)
        {
            if (await db.FindAsync(typeof(Order), (int)Id) is Order order)
            {
                await DeleteAync(order);
            }
        }

        public async Task DeleteAync(Order order)
        {
            db.Orders.Remove(order);
            await db.SaveChangesAsync();
        }
    }
}
