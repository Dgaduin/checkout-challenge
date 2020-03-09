using System;
using System.Threading.Tasks;
using CheckoutChallenge.Domain.SeedWork;

namespace CheckoutChallenge.Domain.PaymentAggregate
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Payment Add(Payment buyer);

        Task<Payment> FindByIdAsync(Guid id);
    }
}