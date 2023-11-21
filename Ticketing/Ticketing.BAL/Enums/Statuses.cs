using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.BAL.Enums
{
    public class Statuses
    {
        public enum PaymentStatusEnum { No_payment = 1, Part_payment = 2, Full_payment = 3, Payment_Failed = 4 };
        public enum SeatStatusEnum { Available = 1, Booked = 2, Sold = 3};
    }
}
