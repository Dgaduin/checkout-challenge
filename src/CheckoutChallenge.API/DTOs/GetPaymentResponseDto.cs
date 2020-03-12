
using System;

namespace CheckoutChallenge.API.DTOs
{
    public class GetPaymentResponseDTO
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string LastFourDigits { get; set; }
        public string NameOnCard { get; set; }
        public string MerchantId { get; set; }
    }
}