using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticketing.BAL.Contracts;
using Ticketing.BAL.Model;

namespace Ticketing.UI.Controlers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VenuesController : ControllerBase
    {
        private IVenueService _venueService;

        public VenuesController(IVenueService venueService)
        {
            _venueService = venueService;
        }


        // GET /venues
        [HttpGet]
        public async IAsyncEnumerable<VenueReturnModel> Get()
        {
            var venues = await _venueService.GetVenuesAsync();

            foreach (var venue in venues)
            {
                yield return venue;
            }
        }


        //GET /venues/{venue_id}/sections
        [HttpGet("{venue_id}/sections")]
        public async IAsyncEnumerable<SectionReturnModel> GetSectionsOfVenue(int venue_id)
        {
            var sections = await _venueService.GetSectionsOfVenue(venue_id);
            foreach (var section in sections)
            {
                yield return section;
            }
        }
    }
}
