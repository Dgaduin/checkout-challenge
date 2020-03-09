using CheckoutChallenge.Domain.SeedWork;

namespace CheckoutChallenge.Domain.PaymentAggregate
{
    public class Payment : Entity, IAggregateRoot
    {
        public Currency Currency { get; private set; }
        public Payment UpdateStatus(PaymentStatus status)
        {
            return this;
        }
    }
}