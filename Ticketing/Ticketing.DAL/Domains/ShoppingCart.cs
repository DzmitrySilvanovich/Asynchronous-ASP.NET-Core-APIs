﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.DAL.Domain;

namespace Ticketing.DAL.Domains
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int SeatId { get; set; }
        public int PriceTypeId { get; set; }
        public decimal Price { get; set; }
        public Guid CartId { get; set; }
    }
}
