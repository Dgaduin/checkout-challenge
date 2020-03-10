using System;

namespace CheckoutChallenge.Models.DTOs
{
    public class PaymentRequestDTO
    {
        public decimal? Amount { get; }
        public string? Currency { get; }
        public string? CardNumber { get; }
        public string? NameOnCard { get; }
        public string? ExpiryDate { get; }
        public string? CVV { get; }
        public string? MerchantId { get; }
    }
}