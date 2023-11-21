using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.BAL.Contracts;
using Ticketing.BAL.Model;
using Ticketing.DAL.Contracts;
using Ticketing.DAL.Domain;
using Ticketing.DAL.Domains;
using static Ticketing.BAL.Enums.Statuses;

namespace Ticketing.BAL.Services
{
    public class CartService : ICartService
    {
        readonly IRepository<ShoppingCart> _repositoryShoppingCart;
        readonly IRepository<Seat> _repositorySeat;
        readonly IRepository<Payment> _repositoryPayment;

        public CartService(IRepository<ShoppingCart> repository, IRepository<Seat> repositorySeat, IRepository<Payment> repositoryPayment)
        {
            _repositoryShoppingCart = repository;
            _repositorySeat = repositorySeat;
            _repositoryPayment = repositoryPayment;
        }

        public async Task<CartStateReturnModel> AddSeatToCartAsync(Guid CartId, OrderCartModel orderCartModel)
        {

            var cartId = CartId;

            var shoppingCarts = await _repositoryShoppingCart.GetAllAsync();

            var item = shoppingCarts.Where(c => c.CartId == cartId && c.EventId == orderCartModel.EventId && c.SeatId == orderCartModel.SeatId).FirstOrDefault();

            var shoppingCartDto = new ShoppingCart
            {
                EventId = orderCartModel.EventId,
                SeatId = orderCartModel.SeatId,
                PriceTypeId = orderCartModel.PriceTypeId,
                Price = orderCartModel.Price,
                CartId = CartId,
            };

            if (item == null)
            {
                await _repositoryShoppingCart.CreateAsync(shoppingCartDto);
            }

            item.PriceTypeId = orderCartModel.PriceTypeId;
            item.Price = orderCartModel.Price;

            await _repositoryShoppingCart.UpdateAsync(shoppingCartDto);

            shoppingCarts = await _repositoryShoppingCart.GetAllAsync();

            var totalAmount = shoppingCarts.Sum(sc => sc.Price);

            return new CartStateReturnModel
            {
                CartId = cartId,
                TotalAmount = totalAmount
            };
        }

        public async Task<int> BookSeatToCartAsync(Guid cartId)
        {
            var shoppingCarts = await _repositoryShoppingCart.GetAllAsync();

            var shoppingCartItems = shoppingCarts.Where(c => c.CartId == cartId).ToList();

            var shoppingCartSeats = shoppingCartItems.Select(sh => sh.SeatId).ToList();

            var allSeats = await _repositorySeat.GetAllAsync();

            var seats = allSeats.Where(s => shoppingCartSeats.Contains(s.Id));

            decimal totalAmount = shoppingCartItems.Sum(i => i.Price);

            foreach (var seat in seats)
            {
                seat.SeatStatusId = (int)SeatStatusEnum.Booked;
                await _repositorySeat.UpdateAsync(seat);
            }

            var payment = new Payment
            {
                Amount = totalAmount,
                CartId = cartId,
                PaymentStatusId = (int)PaymentStatusEnum.No_payment
            };

            var newPayment = await _repositoryPayment.CreateAsync(payment);

            return newPayment.Id;
        }

        public async Task DeleteSeatForCartAsync(Guid cartId, int eventId, int seatId)
        {
            var shoppingCarts = await _repositoryShoppingCart.GetAllAsync();

            var shoppingCartItems = shoppingCarts.Where(c => c.CartId == cartId && c.EventId == eventId && c.SeatId == seatId).ToList();

            foreach (var item in shoppingCartItems)
            {
                await _repositoryShoppingCart.DeleteAync(item);
            }

            return;
        }

        public async Task<IEnumerable<ShoppingCartReturnModel>> CartItemsAsync(Guid cartId)
        {
             var items = await _repositoryShoppingCart.GetAllAsync();
            return items.Where(i => i.CartId == cartId).Select(s => new ShoppingCartReturnModel
            {
                Id = s.Id,
                EventId = s.EventId,
                SeatId = s.SeatId,
                PriceTypeId = s.PriceTypeId,
                Price = s.Price,
                CartId = s.CartId
            });
        }
    }
}
