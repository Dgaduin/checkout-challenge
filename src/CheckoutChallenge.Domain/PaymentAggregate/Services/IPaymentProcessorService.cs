using System;
using System.Threading.Tasks;

namespace CheckoutChallenge.Domain.PaymentAggregate.Services
{
    public interface IPaymentProcessorService
    {
        Task<Guid> ProcessPayment(Payment payment);
    }
}