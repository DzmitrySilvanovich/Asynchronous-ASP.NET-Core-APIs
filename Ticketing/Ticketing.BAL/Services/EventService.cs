using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.BAL.Contracts;
using Ticketing.BAL.Enums;
using Ticketing.BAL.Model;
using Ticketing.DAL.Contracts;
using Ticketing.DAL.Domain;
using Ticketing.DAL.Domains;

namespace Ticketing.BAL.Services
{
    public class EventService : IEventService
    {
        private readonly IRepository<Event> _repositoryEvent;
        private readonly IRepository<Section> _repositorySection;
        private readonly IRepository<Seat> _repositorySeat;
        private readonly IRepository<SeatStatus> _repositorySeatStatus;
        private readonly IRepository<PriceType> _repositoryPriceType;
        private readonly IRepository<ShoppingCart> _repositoryShoppingCart;

        public EventService(IRepository<Event> repositoryEvent,
                            IRepository<Section> repositorySection,
                            IRepository<Seat> repositorySeat,
                            IRepository<SeatStatus> repositorySeatStatus,
                            IRepository<PriceType> repositoryPriceType,
                            IRepository<ShoppingCart> repositoryShoppingCart)
        {
            _repositoryEvent = repositoryEvent;
            _repositorySection = repositorySection;
            _repositorySeat = repositorySeat;
            _repositorySeatStatus = repositorySeatStatus;
            _repositoryPriceType = repositoryPriceType;
            _repositoryShoppingCart = repositoryShoppingCart;
        }




        public async Task<IEnumerable<EventReturnModel>> GetEventsAsync()
        {
            var events = await _repositoryEvent.GetAllAsync();
            return events.Select(e => new EventReturnModel { Id = e.Id, Name = e.Name, EventDate = e.EventDate }).ToList();
        }

        public async Task<List<SeatReturnModel>> GetSeatsAsync(int eventId, int sectionId)
        {
            var seatStatuses = (await _repositorySeatStatus.GetAllAsync()).ToList();
            var priceTypes = (await _repositoryPriceType.GetAllAsync()).ToList();

            var shoppingCarts = await _repositoryShoppingCart.GetAllAsync();
            var shoppingCartsFiltered = shoppingCarts.Where(sh => sh.EventId == eventId).ToList();

            var seatIdShippingCart = shoppingCartsFiltered.Select(sh => sh.Id);

            var seats = await _repositorySeat.GetAllAsync();
            var seatFiltered = seats.Where(s => s.SectionId == sectionId).ToList();

            List<SeatReturnModel> seatReturnModels = seatFiltered.Where(s => seatIdShippingCart.Contains(s.Id))
                .Select(s => new SeatReturnModel
                {
                    SeatId = s.Id,
                    SectionId = s.SectionId,
                    RowNumber = s.RowNumber,
                    SeatNumber = s.SeatNumber,
                    SeatStatusId = s.SeatStatusId,
                    NameSeatStatus = string.Empty,
                    PriceTypeId = 0,
                    NamePriceType = string.Empty
                }).ToList();

            foreach (var item in seatReturnModels)
            {
              var priceTypeId = shoppingCartsFiltered.FirstOrDefault(sh => sh.SeatId == item.SeatId)!.PriceTypeId;
              item.PriceTypeId = priceTypeId;
              item.NamePriceType = priceTypes.FirstOrDefault(p => p.Id == priceTypeId)!.Name;
              item.NameSeatStatus = seatStatuses.FirstOrDefault(s => s.Id == item.SeatStatusId)!.Name;
            }

            return seatReturnModels;
        }
    }
}
