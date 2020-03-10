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
            Action a = () => new Currency(null);
            a.Should().ThrowExactly<ArgumentNullException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData("1")]
        [InlineData("12345")]
        public void LengthNotThree(string currency)
        {
            Action a = () => new Currency(currency);
            a.Should().ThrowExactly<PaymentAggregateException>();
        }

        [Theory]
        [InlineData("123")]
        [InlineData("GB1")]
        [InlineData("SD#")]
        [InlineData("SDЪ")]
        public void NotOnlyCapitalLetters(string currency)
        {
            Action a = () => new Currency(currency);
            a.Should().ThrowExactly<PaymentAggregateException>();
        }

        [Theory]
        [InlineData("USD")]
        [InlineData("GBP")]
        [InlineData("BGN")]
        [InlineData("CND")]
        public void ValidCurrencies(string currency)
        {
            Action a = () => new Currency(currency);
            a.Should().NotThrow();
        }
    }
}