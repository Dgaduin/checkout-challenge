using System;
using System.Threading.Tasks;

namespace CheckoutChallenge.Domain.PaymentAggregate.Services
{
    public interface IPaymentProcessorService
    {
        Task<Payment> ProcessPayment(Payment payment);
    }
}