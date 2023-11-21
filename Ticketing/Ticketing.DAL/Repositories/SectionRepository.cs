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
    public class SectionRepository : IRepository<Section>
    {
        readonly ApplicationContext db;

        public SectionRepository(ApplicationContext context)
        {
            db = context;
        }

        public async Task<Section> CreateAsync(Section section)
        {
            EntityEntry<Section> entity = await db.Sections.AddAsync(section);
            await db.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<IEnumerable<Section>> GetAllAsync() => await db.Sections.ToListAsync();

        public async Task<Section?> GetByIdAsync(object Id) => await db.FindAsync(typeof(Section), (int)Id) as Section;

        public async Task UpdateAsync(Section section)
        {
            db.Sections.Update(section);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAync(object Id)
        {
            if (await db.FindAsync(typeof(Section), (int)Id) is Section section)
            {
                await DeleteAync(section);
            }
        }

        public async Task DeleteAync(Section section)
        {
            db.Sections.Remove(section);
            await db.SaveChangesAsync();
        }
    }
}
