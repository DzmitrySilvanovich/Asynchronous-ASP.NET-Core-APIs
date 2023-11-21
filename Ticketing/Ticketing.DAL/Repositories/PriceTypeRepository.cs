using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.DAL.Contracts;
using Ticketing.DAL.Domain;

namespace Ticketing.DAL.Repositories
{
    public class PriceTypeRepository : IRepository<PriceType>
    {
        readonly ApplicationContext db;

        public PriceTypeRepository(ApplicationContext context)
        {
            db = context;
        }

        public async Task<PriceType> CreateAsync(PriceType priceType)
        {
            EntityEntry<PriceType> entity = await db.PriceTypes.AddAsync(priceType);
            await db.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<IEnumerable<PriceType>> GetAllAsync() => await db.PriceTypes.ToListAsync();

        public async Task<PriceType?> GetByIdAsync(object Id) => await db.FindAsync(typeof(PriceType), (int)Id) as PriceType;

        public async Task UpdateAsync(PriceType priceType)
        {
            db.PriceTypes.Update(priceType);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAync(object Id)
        {
            if (await db.FindAsync(typeof(PriceType), (int)Id) is PriceType priceType)
            {
                await DeleteAync(priceType);
            }
        }

        public async Task DeleteAync(PriceType priceType)
        {
            db.PriceTypes.Remove(priceType);
            await db.SaveChangesAsync();
        }
    }
}
