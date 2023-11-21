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
    public class SeatRepository : IRepository<Seat>
    {
        readonly ApplicationContext db;

        public SeatRepository(ApplicationContext context)
        {
            db = context;
        }

        public async Task<Seat> CreateAsync(Seat seat)
        {
            EntityEntry<Seat> entity = await db.Seats.AddAsync(seat);
            await db.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<IEnumerable<Seat>> GetAllAsync() => await db.Seats.ToListAsync();

        public async Task<Seat?> GetByIdAsync(object Id) => await db.FindAsync(typeof(Seat), (int)Id) as Seat;

        public async Task UpdateAsync(Seat seat)
        {
            db.Seats.Update(seat);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAync(object Id)
        {
            if (await db.FindAsync(typeof(Seat), (int)Id) is Seat seat)
            {
                await DeleteAync(seat);
            }
        }

        public async Task DeleteAync(Seat seat)
        {
            db.Seats.Remove(seat);
            await db.SaveChangesAsync();
        }
    }
}
