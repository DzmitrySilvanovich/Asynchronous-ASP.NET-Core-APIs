using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
    public class VenueRepository : IRepository<Venue>
    {
        private readonly ApplicationContext db;

        public VenueRepository(ApplicationContext context)
        {
            db = context;
        }

        public async Task<Venue> CreateAsync(Venue venue)
        {
            EntityEntry<Venue> entity = await db.Venues.AddAsync(venue);
            await db.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<IEnumerable<Venue>> GetAllAsync() => await db.Venues.ToListAsync();

        public async Task<Venue?> GetByIdAsync(object Id) => await db.FindAsync(typeof(Venue), (int)Id) as Venue;

        public async Task UpdateAsync(Venue venue)
        {
            db.Venues.Update(venue);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAync(object Id)
        {
            if (await db.FindAsync(typeof(Venue), (int)Id) is Venue venue)
            {
                await DeleteAync(venue);
            }
        }

        public async Task DeleteAync(Venue venue)
        {
            db.Venues.Remove(venue);
            await db.SaveChangesAsync();
        }
    }
}
