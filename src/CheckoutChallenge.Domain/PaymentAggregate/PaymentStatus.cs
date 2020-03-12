using CheckoutChallenge.Domain.SeedWork;

namespace CheckoutChallenge.Domain.PaymentAggregate
{
    public class PaymentStatus : Enumeration
    {
#pragma warning disable MA0069
        public static PaymentStatus Success = new PaymentStatus(1, nameof(Success).ToLowerInvariant());
        public static PaymentStatus InsufficientFunds = new PaymentStatus(2, nameof(InsufficientFunds).ToLowerInvariant());
        public static PaymentStatus Denied = new PaymentStatus(3, nameof(Denied).ToLowerInvariant());
        public static PaymentStatus DetailsNotRecognised = new PaymentStatus(4, nameof(DetailsNotRecognised).ToLowerInvariant());
        public static PaymentStatus ServiceError = new PaymentStatus(5, nameof(ServiceError).ToLowerInvariant());
        public static PaymentStatus NotProcessed = new PaymentStatus(6, nameof(NotProcessed).ToLowerInvariant());
#pragma warning restore MA0069
        public PaymentStatus(int id, string name) : base(id, name) { }
    }
}