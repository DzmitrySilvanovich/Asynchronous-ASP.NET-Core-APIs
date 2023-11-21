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
    public class PaymentStatusRepository : IRepository<PaymentStatus>
    {
        readonly ApplicationContext db;

        public PaymentStatusRepository(ApplicationContext context)
        {
            db = context;
        }

        public async Task<PaymentStatus> CreateAsync(PaymentStatus paymentStatus)
        {
            EntityEntry<PaymentStatus> entity = await db.PaymentStatuses.AddAsync(paymentStatus);
            await db.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<IEnumerable<PaymentStatus>> GetAllAsync() => await db.PaymentStatuses.ToListAsync();

        public async Task<PaymentStatus?> GetByIdAsync(object Id) => await db.FindAsync(typeof(PaymentStatus), (int)Id) as PaymentStatus;

        public async Task UpdateAsync(PaymentStatus paymentStatus)
        {
            db.PaymentStatuses.Update(paymentStatus);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAync(object Id)
        {
            if (await db.FindAsync(typeof(PaymentStatus), (int)Id) is PaymentStatus paymentStatus)
            {
                await DeleteAync(paymentStatus);
            }
        }

        public async Task DeleteAync(PaymentStatus paymentStatus)
        {
            db.PaymentStatuses.Remove(paymentStatus);
            await db.SaveChangesAsync();
        }
    }
}
