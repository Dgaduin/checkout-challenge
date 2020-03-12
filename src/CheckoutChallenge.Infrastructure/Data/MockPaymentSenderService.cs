using System.Threading.Tasks;
using CheckoutChallenge.Domain.PaymentAggregate;
using CheckoutChallenge.Domain.PaymentAggregate.Services;

namespace CheckoutChallenge.Infrastructure.Data
{
    /// <summary>
    /// This service will return different responses based on the modulo 10 of the ammount
    /// </summary>
    public class MockPaymentSenderService : IPaymentSenderService
    {
        public Task<PaymentStatus> SendPayment(Payment payment)
        {
            var key = (((int)payment.Amount % 10) + 1) / 2;
            var response = key switch
            {
                0 => PaymentStatus.Success,
                1 => PaymentStatus.DetailsNotRecognised,
                2 => PaymentStatus.Denied,
                3 => PaymentStatus.InsufficientFunds,
                _ => PaymentStatus.ServiceError
            };
            return Task.FromResult(response);
        }
    }
}