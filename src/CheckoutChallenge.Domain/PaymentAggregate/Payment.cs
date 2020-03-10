using System;
using CheckoutChallenge.Domain.SeedWork;

namespace CheckoutChallenge.Domain.PaymentAggregate
{
    // There is no address here, but it probably should be
    public class Payment : Entity, IAggregateRoot
    {
        public Currency Currency { get; private set; }

        public PaymentStatus? PaymentStatus { get; private set; }
        // Probably not the right value type for this
        // uint might be better and go for the lowest possible currency denomination
        // also JSON numerals have some caveats in terms of processing 
        // and this will need extra tests in regards to how the serialisers handle them
        public decimal Amount { get; private set; }
        public CardNumber CardNumber { get; private set; }
        public DateTime ExpiryDate { get; private set; }
        // This can be split into 2 names, but has a lot of variants between different providers
        // In addition someplaces have title and initials - this might call for another value object
        public string NameOnCard { get; private set; }
        public string CVV { get; }
        public Guid MerchantId { get; }

        public Payment(string currency,
                       decimal amount,
                       string carduNumber,
                       DateTime expiryDate,
                       string nameOnCard,
                       string cvv,
                       string merchantId,
                       PaymentStatus status)
        {
            Currency = new Currency(currency);

            if (amount <= 0)
                throw new ArgumentException("Amount is 0 or negative", nameof(amount));
            Amount = amount;

            CardNumber = new CardNumber(carduNumber);

            // The datetime provider can be abstracted for testing
            // Also this check is a bit primitive - no regards for the country of payment
            // or any other complex details around datetime
            if (expiryDate < DateTime.UtcNow)
                throw new ArgumentException("Expiry data is in the past", nameof(expiryDate));
            ExpiryDate = expiryDate;

            if (nameOnCard is null)
                throw new ArgumentNullException(nameof(nameOnCard));
            if (string.IsNullOrWhiteSpace(nameOnCard))
                throw new ArgumentException("Name is empty", nameof(nameOnCard));

            NameOnCard = nameOnCard;

            if (cvv is null)
                throw new ArgumentNullException(nameof(cvv));
            if (string.IsNullOrWhiteSpace(cvv))
                throw new ArgumentException("CVV is empty", nameof(cvv));

            CVV = cvv;

            if (merchantId is null)
                throw new ArgumentNullException(nameof(merchantId));
            if (string.IsNullOrWhiteSpace(merchantId))
                throw new ArgumentException("MerchantId is empty", nameof(merchantId));
            if (Guid.TryParse(merchantId, out var guid))
                MerchantId = guid;
            else
                throw new ArgumentException("MerchantId is not a valid guid", nameof(merchantId));

            if (!(status is null))
                PaymentStatus = status;
        }

        public Payment UpdateStatus(PaymentStatus status)
        {
            if (status is null)
                throw new ArgumentNullException(nameof(status));

            PaymentStatus = status;
            return this;
        }
    }
}