using Ticketing.BAL.Contracts;
using Ticketing.BAL.Model;
using Ticketing.DAL.Contracts;
using Ticketing.DAL.Domain;
using Ticketing.DAL.Repositories;
using Mapster;

namespace Ticketing.BAL.Services
{
    public class VenueService : IVenueService
    {
        private readonly IRepository<Venue> _repository;
        private readonly IRepository<Section> _repositorySection;

        public VenueService(Repository<Venue> repository, Repository<Section> repositorySection)
        {
            _repository = repository;
            _repositorySection = repositorySection;
        }

        public async Task<IEnumerable<VenueReturnModel>> GetVenuesAsync()
        {
            var venues = await _repository.GetAllAsync();
            return venues.AsQueryable().ProjectToType<VenueReturnModel>().ToList();
        }

        public async Task<IEnumerable<SectionReturnModel>> GetSectionsOfVenue(int venueId)
        {
            var ss = await _repositorySection.GetAllAsync();
            var sections = ss.Where(s => s.VenueId == venueId).AsQueryable().ProjectToType<SectionReturnModel>().ToList();

            return sections;
        }
    }
}
