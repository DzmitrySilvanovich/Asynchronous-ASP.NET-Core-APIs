using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Ticketing.BAL.Contracts;
using Ticketing.BAL.Model;
using Ticketing.BAL.Services;
using Ticketing.DAL.Domain;
using Ticketing.DAL.Domains;

namespace Ticketing.UI.Controlers
{
    [Route("api/[controller]/carts")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ICartService _cartService;

        public OrdersController(ICartService cartService)
        {
            _cartService = cartService;

        }

        //GET orders/carts /{cart_id}
        [HttpGet("{id}")]
        public async IAsyncEnumerable<ShoppingCartReturnModel> GetAsync(Guid id)
        {
            var shoppingCarts = await _cartService.CartItemsAsync(id);

            foreach (var shoppingCart in shoppingCarts)
            {
                yield return shoppingCart;
            }
        }

        // POST orders/carts/{cart_id}
        [HttpPost("{id}")]
         public async Task<CartStateReturnModel> Post(Guid Id, [FromBody] OrderCartModel orderCartModel)
        {
            var result = await _cartService.AddSeatToCartAsync(Id, orderCartModel);
            return result;
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{cart_id}/book")]
        public async Task<int> PutAsync(Guid cart_id)
        {
            var result = await _cartService.BookSeatToCartAsync(cart_id);

            return result;
        }

        //DELETE orders/carts/{cart_id}/events/{event_id}/seats/{seat_id}
        [HttpDelete("{cart_id}/events/{event_id}/seats/{seat_id}")]
        public async Task DeleteAsync(Guid cart_id, int event_id, int seat_id)
        {
            await _cartService.DeleteSeatForCartAsync(cart_id, event_id, seat_id);
        }
    }
}
