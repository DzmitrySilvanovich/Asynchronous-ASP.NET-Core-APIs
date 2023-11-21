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
    public class PaymentRepository : IRepository<Payment>
    {
        readonly ApplicationContext db;

        public PaymentRepository(ApplicationContext context)
        {
            db = context;
        }

        public async Task<Payment> CreateAsync(Payment payment)
        {
            EntityEntry<Payment> entity = await db.Payments.AddAsync(payment);
            await db.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<IEnumerable<Payment>> GetAllAsync() => await db.Payments.ToListAsync();

        public async Task<Payment?> GetByIdAsync(object Id) => await db.FindAsync(typeof(Payment), (int)Id) as Payment;

        public async Task UpdateAsync(Payment payment)
        {
            db.Payments.Update(payment);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAync(object Id)
        {
            if (await db.FindAsync(typeof(Payment), (int)Id) is Payment payment)
            {
                await DeleteAync(payment);
            }
        }

        public async Task DeleteAync(Payment payment)
        {
            db.Payments.Remove(payment);
            await db.SaveChangesAsync();
        }
    }
}
