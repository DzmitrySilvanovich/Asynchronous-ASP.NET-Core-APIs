using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.DAL.Contracts;
using Ticketing.DAL.Domain;
using Microsoft.Extensions.Logging;

namespace Ticketing.DAL.Repositories
{
    public class EventRepository : IRepository<Event>
    {
        readonly ApplicationContext db;

        public EventRepository(ApplicationContext context)
        {
            db = context;
        }

        public async Task<Event> CreateAsync(Event @event)
        {
            EntityEntry<Event> entity = await db.Events.AddAsync(@event);
            await db.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<IEnumerable<Event>> GetAllAsync() => await db.Events.ToListAsync();

        public async Task<Event?> GetByIdAsync(object Id) => await db.FindAsync(typeof(Event), (int)Id) as Event;

        public async Task UpdateAsync(Event @event)
        {
            db.Events.Update(@event);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAync(object Id)
        {
            if (await db.FindAsync(typeof(Event), (int)Id) is Event @event)
            {
                await DeleteAync(@event);
            }
        }

        public async Task DeleteAync(Event @event)
        {
            db.Events.Remove(@event);
            await db.SaveChangesAsync();
        }
    }
}
