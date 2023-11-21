using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.DAL.Domains;

namespace Ticketing.DAL.Domain
{
    public class Cart
    {
        public Guid Id { get; set; }
        public List<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();
    }
}
