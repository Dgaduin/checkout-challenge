using System;
using Xunit;
using CheckoutChallenge.Domain.PaymentAggregate;
using FluentAssertions;

namespace CheckoutChallenge.Domain.Test
{
    public class CurrencyTests
    {
        [Fact]
        public void ValueIsNull()
        {
            Action a = () => new CardNumber(null);
            a.Should().ThrowExactly<ArgumentNullException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData("1")]
        [InlineData("12345")]
        public void LengthNotThree(string currency)
        {
            Action a = () => new CardNumber(currency);
            a.Should().ThrowExactly<PaymentAggregateException>();
        }

        [Theory]
        [InlineData("123")]
        [InlineData("GB1")]
        [InlineData("SD#")]
        [InlineData("SDЪ")]
        public void NotOnlyCapitalLetters(string currency)
        {
            Action a = () => new CardNumber(currency);
            a.Should().ThrowExactly<PaymentAggregateException>();
        }
    }
}