using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CheckoutChallenge.Domain.SeedWork;

namespace CheckoutChallenge.Domain.PaymentAggregate
{
    public class Currency : ValueObject
    {
        public string Value { get; private set; }
        public Currency(string value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            if (value.Length != 3)
                throw new PaymentAggregateException("Currency code has wrong length");

            if (!OnlyCapitalLetters(value))
                throw new PaymentAggregateException("Currency code has wrong length");

            Value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        private static bool OnlyCapitalLetters(string cardNumber)
        {
            Regex r = new Regex("^[A-Z]*$", RegexOptions.None, TimeSpan.FromSeconds(1));
            return r.IsMatch(cardNumber);
        }
    }
}