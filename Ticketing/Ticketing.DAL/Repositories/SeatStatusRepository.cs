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
    public class SeatStatusRepository : IRepository<SeatStatus>
    {
        readonly ApplicationContext db;

        public SeatStatusRepository(ApplicationContext context)
        {
            db = context;
        }

        public async Task<SeatStatus> CreateAsync(SeatStatus seatStatus)
        {
            EntityEntry<SeatStatus> entity = await db.SeatStatuses.AddAsync(seatStatus);
            await db.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<IEnumerable<SeatStatus>> GetAllAsync() => await db.SeatStatuses.ToListAsync();

        public async Task<SeatStatus?> GetByIdAsync(object Id) => await db.FindAsync(typeof(SeatStatus), (int)Id) as SeatStatus;

        public async Task UpdateAsync(SeatStatus seatStatus)
        {
            db.SeatStatuses.Update(seatStatus);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAync(object Id)
        {
            if (await db.FindAsync(typeof(SeatStatus), (int)Id) is SeatStatus seatStatus)
            {
                await DeleteAync(seatStatus);
            }
        }

        public async Task DeleteAync(SeatStatus seatStatus)
        {
            db.SeatStatuses.Remove(seatStatus);
            await db.SaveChangesAsync();
        }
    }
}
