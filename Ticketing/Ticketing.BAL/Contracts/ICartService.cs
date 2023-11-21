using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Ticketing.BAL.Model;
using Ticketing.DAL.Domain;
using Ticketing.DAL.Domains;

namespace Ticketing.BAL.Contracts
{
    public interface ICartService
    {
        public Task<CartStateReturnModel> AddSeatToCartAsync(Guid CartId, OrderCartModel orderCartModel);

        public Task<int> BookSeatToCartAsync(Guid cartId);

        public Task DeleteSeatForCartAsync(Guid cart_id, int event_id, int seat_id);

        public Task<IEnumerable<ShoppingCartReturnModel>> CartItemsAsync(Guid cartId);
    }
}
