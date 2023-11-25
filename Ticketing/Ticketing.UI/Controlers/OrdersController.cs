using Microsoft.AspNetCore.Mvc;
using Ticketing.BAL.Contracts;
using Ticketing.BAL.Model;

namespace Ticketing.UI.Controlers
{
    /// <summary>
    /// Orders API
    /// </summary>
    [Route("api/[controller]/carts")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ICartService _cartService;

        public OrdersController(ICartService cartService)
        {
            _cartService = cartService;

        }

        /// <summary>
        /// Get items of the cart.
        /// <param name="id">Cart id</param>
        /// <returns>The collection of items of the cart</returns>
        /// <response code="200">Return a status of request</response>
        /// <response code="400">Bad request</response>        
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var shoppingCarts = await _cartService.CartItemsAsync(id);

            if (shoppingCarts is null)
            {
                return BadRequest(string.Empty);
            }
            else if (!shoppingCarts.Any())
            {
                return NoContent();
            }

            return Ok(shoppingCarts);
        }

        /// <summary>
        /// Add Seat To Cart.
        /// <param name="Id">Cart id</param>
        /// <param name="orderCartModel">Payload for the action</param>
        /// <returns>The collection of items of the cart</returns>
        /// <response code="200">Return a status of request with result</response>
        /// <response code="400">Bad request</response>        
        /// </summary>
        [HttpPost("{Id}")]
         public async Task<IActionResult> Post(Guid Id, [FromBody] OrderCartModel orderCartModel)
        {
            var result = await _cartService.AddSeatToCartAsync(Id, orderCartModel);

            if (result is null)
            {
                return BadRequest(string.Empty);
            }

            return Ok(result);
        }

        /// <summary>
        /// Book Seat To Cart.
        /// <param name="cartId">Cart id</param>
        /// <response code="200">Return a status of request</response>
        /// </summary>
        [HttpPut("{cartId}/book")]
        public async Task<IActionResult> PutAsync(Guid cartId)
        {
            var result = await _cartService.BookSeatToCartAsync(cartId);

            return Ok(result);
        }

        /// <summary>
        /// Delete Seat For Cart.
        /// <param name="cartId">Cart id</param>
        /// <param name="eventId">Event id</param>
        /// <param name="seatId">Seat id</param>
        /// <response code="200">Return a status of request</response>
        /// </summary>
        [HttpDelete("{cartId}/events/{eventId}/seats/{seatId}")]
        public async Task<IActionResult> DeleteAsync(Guid cartId, int eventId, int seatId)
        {
            await _cartService.DeleteSeatForCartAsync(cartId, eventId, seatId);
            return Ok();
        }
    }
}
