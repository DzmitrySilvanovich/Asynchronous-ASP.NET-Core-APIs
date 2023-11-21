using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Ticketing.BAL.Contracts;
using Ticketing.BAL.Model;
using Ticketing.DAL.Contracts;
using Ticketing.DAL.Domain;

namespace Ticketing.BAL.Services
{
    public class VenueService : IVenueService
    {
        private readonly IRepository<Venue> _repository;
        private readonly IRepository<Section> _repositorySection;

        public VenueService(IRepository<Venue> repository, IRepository<Section> repositorySection)
        {
            _repository = repository;
            _repositorySection = repositorySection;
        }

        public async Task<IEnumerable<VenueReturnModel>> GetVenuesAsync()
        {
            var venues = await _repository.GetAllAsync();
            return venues.Select(v => new VenueReturnModel { Id = v.Id, Name = v.Name }).ToList();
        }

        public async Task<IEnumerable<SectionReturnModel>> GetSectionsOfVenue(int venue_id)
        {
            var ss = await _repositorySection.GetAllAsync();
            var sections = ss.Where(s => s.VenueId == venue_id).Select(s => new SectionReturnModel { Id = s.Id, Name = s.Name }).ToList();

            return sections;
        }
    }
}
