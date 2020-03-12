using System;

namespace CheckoutChallenge.API.DTOs
{
    public class MakePaymentResponseDTO
    {
        public string PaymentStatus { get; set; }
        public Guid PaymentId { get; set; }
    }
}