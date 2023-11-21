using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.DAL.Contracts;
using Ticketing.DAL.Domain;
using Ticketing.DAL.Domains;

namespace Ticketing.DAL.Repositories
{
    public class ShoppingCartRepository : IRepository<ShoppingCart>
    {
        readonly ApplicationContext db;

        public ShoppingCartRepository(ApplicationContext context)
        {
            db = context;
        }

        public async Task<ShoppingCart> CreateAsync(ShoppingCart shoppingCart)
        {
            EntityEntry<ShoppingCart> entity = await db.ShoppingCarts.AddAsync(shoppingCart);
            await db.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<IEnumerable<ShoppingCart>> GetAllAsync() => await db.ShoppingCarts.ToListAsync();

        public async Task<ShoppingCart?> GetByIdAsync(object Id) => await db.FindAsync(typeof(ShoppingCart), (int)Id) as ShoppingCart;

        public async Task UpdateAsync(ShoppingCart shoppingCart)
        {
            db.ShoppingCarts.Update(shoppingCart);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAync(object Id)
        {
            if (await db.FindAsync(typeof(ShoppingCart), (int)Id) is ShoppingCart shoppingCart)
            {
                await DeleteAync(shoppingCart);
            }
        }

        public async Task DeleteAync(ShoppingCart shoppingCart)
        {
            db.ShoppingCarts.Remove(shoppingCart);
            await db.SaveChangesAsync();
        }
    }
}
