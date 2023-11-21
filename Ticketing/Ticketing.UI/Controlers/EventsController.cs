using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ticketing.BAL.Contracts;
using Ticketing.BAL.Model;
using Ticketing.BAL.Services;
using Ticketing.DAL.Domain;

namespace Ticketing.UI.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        //GET /events
        [HttpGet]
        public async IAsyncEnumerable<EventReturnModel> Get()
        {
            var events = await _eventService.GetEventsAsync();

            foreach (var @event in events)
            {
                yield return @event;
            }
        }

        //GET /events/{event_id}/sections/{section_id}/ seats
        [HttpGet("{eventId}/sections/{sectionId}/seats")]
        public async IAsyncEnumerable<SeatReturnModel> GetSeatsAsync(int eventId, int sectionId)
        {
            var seats = await _eventService.GetSeatsAsync( eventId, sectionId);

            foreach (var seat in seats)
            {
                yield return seat;
            }
        }
    }
}
