using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.DAL.Domain
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int PaymentStatusId { get; set; }
        public PaymentStatus? PaymentStatus { get; set; }
        public Guid CartId { get; set; }
        public Cart? Cart { get; set;}
    }
}
