using System.Threading.Tasks;

namespace CheckoutChallenge.Domain.PaymentAggregate.Services
{
    public interface IPaymentSenderService
    {
        Task<PaymentStatus> SendPayment(Payment payment);
    }
}