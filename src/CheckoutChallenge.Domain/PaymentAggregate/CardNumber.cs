using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CheckoutChallenge.Domain.SeedWork;

namespace CheckoutChallenge.Domain.PaymentAggregate
{
    public class CardNumber : ValueObject
    {
        // We can add card type as well
        public string Value { get; private set; }

        public string LastFourDigits => Value.Substring(Value.Length - 4);

        private CardNumber() { }
        public CardNumber(string value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            if (value.Length < 12 || value.Length > 19)
                throw new PaymentAggregateException("Card number has incorrect length");

            if (!OnlyNumeric(value))
                throw new PaymentAggregateException("Card number non numerical symbols in it");

            if (!VerifyLuhnChecksum(value))
                throw new PaymentAggregateException("Card number failed checksum");

            Value = value;
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        private static bool VerifyLuhnChecksum(string cardNumber)
        {
            int sum = 0;
            for (int i = cardNumber.Length - 2, j = 1; i >= 0; i--, j++)
            {
                // This can be written in a better way probably
                int a = (int)cardNumber[i] - 48;
                int b = a * ((j % 2) + 1);
                int c = (b % 10) + (b / 10);
                sum += c;
            }
            return (sum * 9) % 10 == (int)cardNumber[cardNumber.Length - 1] - 48;
        }

        private static bool OnlyNumeric(string cardNumber)
        {
            Regex r = new Regex("^[0-9]*$", RegexOptions.None, TimeSpan.FromSeconds(1));
            return r.IsMatch(cardNumber);
        }
    }
}