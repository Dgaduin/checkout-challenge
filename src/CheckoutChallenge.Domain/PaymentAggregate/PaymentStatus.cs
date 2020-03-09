using CheckoutChallenge.Domain.SeedWork;

namespace CheckoutChallenge.Domain.PaymentAggregate
{
    public class PaymentStatus : Enumeration
    {
        public static PaymentStatus Success = new PaymentStatus(1, nameof(Success).ToLowerInvariant());
        public static PaymentStatus InsufficientFunds = new PaymentStatus(2, nameof(InsufficientFunds).ToLowerInvariant());
        public static PaymentStatus Denied = new PaymentStatus(3, nameof(Denied).ToLowerInvariant());
        public static PaymentStatus DetailsNotRecognised = new PaymentStatus(4, nameof(DetailsNotRecognised).ToLowerInvariant());
        public static PaymentStatus ServiceError = new PaymentStatus(5, nameof(ServiceError).ToLowerInvariant());

        public PaymentStatus(int id, string name) : base(id, name) { }
    }
}