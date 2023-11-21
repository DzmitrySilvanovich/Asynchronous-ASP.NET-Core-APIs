using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ticketing.DAL.Contracts;
using Ticketing.DAL.Domain;

namespace Ticketing.DAL.Repositories
{
    public class CartRepository : IRepository<Cart>
    {
        readonly ApplicationContext db;

        public CartRepository(ApplicationContext context)
        {
            db = context;
        }

        public async Task<Cart> CreateAsync(Cart cart)
        {
            EntityEntry<Cart> entity = await db.Carts.AddAsync(cart);
            await db.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<IEnumerable<Cart>> GetAllAsync() => await db.Carts.ToListAsync();

        public async Task<Cart?> GetByIdAsync(object Id) => await db.FindAsync(typeof(Cart), (Guid)Id) as Cart;

        public async Task UpdateAsync(Cart cart)
        {
            db.Carts.Update(cart);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAync(object Id)
        {
            if (await db.FindAsync(typeof(Cart), (Guid)Id) is Cart cart)
            {
                await DeleteAync(cart);
            }
        }

        public async Task DeleteAync(Cart cart)
        {
            db.Carts.Remove(cart);
            await db.SaveChangesAsync();
        }
    }
}
