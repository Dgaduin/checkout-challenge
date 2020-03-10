using System;

namespace CheckoutChallenge.Models.DTOs
{
    public class PaymentRequestDTO
    {
        public decimal? Amount { get; set; }
        public string? Currency { get; set; }
        public string? CardNumber { get; set; }
        public string? NameOnCard { get; set; }
        public string? ExpiryDate { get; set; }
        public string? CVV { get; set; }
        public string? MerchantId { get; set; }
    }
}