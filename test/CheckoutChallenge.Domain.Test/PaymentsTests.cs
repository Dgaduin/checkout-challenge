using System;
using Xunit;
using CheckoutChallenge.Domain.PaymentAggregate;
using FluentAssertions;

namespace CheckoutChallenge.Domain.Test
{
    public class PaymentsTests
    {
        private const string ValidCurrency = "GBP";
        private const decimal ValidAmount = 15;
        private const string ValidCardNumber = "6011563271013741";

        // We should be using an abstracted provider or a factory to generate this 
        private static DateTime ValidExpiryDate = new DateTime(3000, 12, 30);
        private const string ValidName = "MR J SMITH";
        private const string ValidCvv = "9292";
        private const string ValidMerchantId = "C56A4180-65AA-42EC-A945-5FD21DEC0538";
        private static PaymentStatus ValidStatus = PaymentStatus.ServiceError;

        [Fact]
        public void ValidParametersShouldntThrow()
        {
            Action a = () => new Payment(ValidCurrency,
                                         ValidAmount,
                                         ValidCardNumber,
                                         ValidExpiryDate,
                                         ValidName,
                                         ValidCvv,
                                         ValidMerchantId,
                                         ValidStatus);
            a.Should().NotThrow();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("123")]
        [InlineData("")]
        [InlineData("GB2")]
        public void CurrencyIsInvalid(string currency)
        {
            Action a = () => new Payment(currency,
                                         ValidAmount,
                                         ValidCardNumber,
                                         ValidExpiryDate,
                                         ValidName,
                                         ValidCvv,
                                         ValidMerchantId,
                                         ValidStatus);
            a.Should().Throw<Exception>();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        // We mark theese as double and not decimal
        // since attributes cant take decimal as parameter
        [InlineData(-0.23d)]
        [InlineData(-15.45232323d)]
        public void AmountIsInvalid(decimal amount)
        {
            Action a = () => new Payment(ValidCurrency,
                                         amount,
                                         ValidCardNumber,
                                         ValidExpiryDate,
                                         ValidName,
                                         ValidCvv,
                                         ValidMerchantId,
                                         ValidStatus);
            a.Should().Throw<Exception>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("12345")]
        [InlineData("12345123451234512345")]
        public void CardNumberIsInvalid(string number)
        {
            Action a = () => new Payment(ValidCurrency,
                                         ValidAmount,
                                         number,
                                         ValidExpiryDate,
                                         ValidName,
                                         ValidCvv,
                                         ValidMerchantId,
                                         ValidStatus);
            a.Should().Throw<Exception>();
        }

        [Fact]
        public void ExpiryDateIsInvalid()
        {
            Action a = () => new Payment(ValidCurrency,
                                         ValidAmount,
                                         ValidCardNumber,
                                         new DateTime(2018, 12, 30),
                                         ValidName,
                                         ValidCvv,
                                         ValidMerchantId,
                                         ValidStatus);
            a.Should().Throw<ArgumentException>();
        }


        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData("\n\r")]
        public void NameIsInvalid(string name)
        {
            Action a = () => new Payment(ValidCurrency,
                                         ValidAmount,
                                         ValidCardNumber,
                                         ValidExpiryDate,
                                         name,
                                         ValidCvv,
                                         ValidMerchantId,
                                         ValidStatus);
            a.Should().Throw<Exception>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData("\n\r")]
        public void CvvIsInvalid(string cvv)
        {
            Action a = () => new Payment(ValidCurrency,
                                         ValidAmount,
                                         ValidCardNumber,
                                         ValidExpiryDate,
                                         ValidName,
                                         cvv,
                                         ValidMerchantId,
                                         ValidStatus);
            a.Should().Throw<Exception>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData("\n\r")]
        [InlineData("C56A4180-65AA-42EC-A945-5FD21DEC")]
        [InlineData("x56a4180-h5aa-42ec-a945-5fd21dec0538")]
        public void GuidIsInvalid(string guid)
        {
            Action a = () => new Payment(ValidCurrency,
                                         ValidAmount,
                                         ValidCardNumber,
                                         ValidExpiryDate,
                                         ValidName,
                                         ValidCvv,
                                         guid,
                                         ValidStatus);
            a.Should().Throw<Exception>();
        }
    }
}