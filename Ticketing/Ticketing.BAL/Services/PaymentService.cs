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
    public class PaymentService : IPaymentService
    {
        readonly IRepository<Payment> _repositoryPayment;
        readonly IRepository<PaymentStatus> _repositoryPaymentStatus;
        readonly IRepository<ShoppingCart> _repositoryShoppingCart;
        readonly IRepository<Seat> _repositorySeat;

        public PaymentService(IRepository<Payment> repositoryPayment,
            IRepository<PaymentStatus> repositoryPaymentStatus,
            IRepository<ShoppingCart> repositoryShoppingCart,
            IRepository<Seat> repositorySeat)
        {
            _repositoryPayment = repositoryPayment;
            _repositoryPaymentStatus = repositoryPaymentStatus;
            _repositoryShoppingCart = repositoryShoppingCart;
            _repositorySeat = repositorySeat;
        }

        public async Task<PaymentStatusReturnModel> GetPaymentSatatusAsync(int paymentId)
        {
            var returnPaymentStatus = new PaymentStatusReturnModel();

            var payment = await _repositoryPayment.GetByIdAsync(paymentId);

            if (payment is null)
            {
                return returnPaymentStatus;
            }

            var paymentStatus = await _repositoryPaymentStatus.GetByIdAsync(payment.PaymentStatusId);

            if (paymentStatus is null)
            {
                return returnPaymentStatus;
            }

            return returnPaymentStatus;
        }

        public async Task CompletePaymentAsync(int paymentId)
        {
            var payment = await _repositoryPayment.GetByIdAsync(paymentId);

            if (payment is null)
            {
                return;
            }

            payment.PaymentStatusId = (int)PaymentStatusEnum.Full_payment;
            await _repositoryPayment.UpdateAsync(payment);

            var shoppingCarts = await _repositoryShoppingCart.GetAllAsync();

            var shoppingCartItems = shoppingCarts.Where(c => c.CartId == payment.CartId).ToList();

            var shoppingCartSeats = shoppingCartItems.Select(sh => sh.SeatId).ToList();

            var allSeats = await _repositorySeat.GetAllAsync();

            var seats = allSeats.Where(s => shoppingCartSeats.Contains(s.Id));

            foreach (var seat in seats)
            {
                seat.SeatStatusId = (int)SeatStatusEnum.Sold;
                await _repositorySeat.UpdateAsync(seat);
            }
        }

        public async Task FailedPaymentAsync(int paymentId)
        {
            var payment = await _repositoryPayment.GetByIdAsync(paymentId);

            if (payment is null)
            {
                return;
            }

            payment.PaymentStatusId = (int)PaymentStatusEnum.Payment_Failed;
            await _repositoryPayment.UpdateAsync(payment);

            var shoppingCarts = await _repositoryShoppingCart.GetAllAsync();

            var shoppingCartItems = shoppingCarts.Where(c => c.CartId == payment.CartId).ToList();

            var shoppingCartSeats = shoppingCartItems.Select(sh => sh.SeatId).ToList();

            var allSeats = await _repositorySeat.GetAllAsync();

            var seats = allSeats.Where(s => shoppingCartSeats.Contains(s.Id));

            foreach (var seat in seats)
            {
                seat.SeatStatusId = (int)SeatStatusEnum.Available;
                await _repositorySeat.UpdateAsync(seat);
            }
        }
    }
}
