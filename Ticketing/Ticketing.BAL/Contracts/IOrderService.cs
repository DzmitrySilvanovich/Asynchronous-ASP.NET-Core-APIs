﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.BAL.Contracts
{
    public interface IOrderService
    {
        public Task<bool> ReleaseCartsFromOrderAsync(int orderId);
    }
}
