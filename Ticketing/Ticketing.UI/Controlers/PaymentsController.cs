using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Ticketing.BAL.Contracts;
using Ticketing.BAL.Model;
using Ticketing.DAL.Domain;

namespace Ticketing.UI.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        //GET payments/{payment_id}
        [HttpGet("{id}")]
        public async Task<PaymentStatusReturnModel> GetAsync(int id)
        {
            var res = await _paymentService.GetPaymentSatatusAsync(id);

            return res;
        }

        //POST payments/{payment_id}/complete
        [HttpPost("{id}/complete")]
        public async Task PostCompleteAsync(int id)
        {
            await _paymentService.CompletePaymentAsync(id);
        }

        // POST payments/{payment_id}/failed
        [HttpPost("{id}/failed")]
        public async Task PostFailedAsync(int id)
        {
            await _paymentService.FailedPaymentAsync(id);
        }
    }
}
