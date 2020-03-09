namespace CheckoutChallenge.Domain.PaymentAggregate
{
    [System.Serializable]
    public class PaymentAggregateException : System.Exception
    {
        public PaymentAggregateException() { }
        public PaymentAggregateException(string message) : base(message) { }
        public PaymentAggregateException(string message, System.Exception inner) : base(message, inner) { }
        protected PaymentAggregateException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}