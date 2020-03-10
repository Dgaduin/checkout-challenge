using System;
using Xunit;
using CheckoutChallenge.Domain.PaymentAggregate;
using FluentAssertions;

namespace CheckoutChallenge.Domain.Test
{
    public class CardNumberTests
    {
        [Fact]
        public void ValueIsNull()
        {
            Action a = () => new CardNumber(null);
            a.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void NumberIsTooShort()
        {
            Action a = () => new CardNumber("tooShort");
            a.Should().ThrowExactly<PaymentAggregateException>();
        }

        [Fact]
        public void NumberIsTooLong()
        {
            Action a = () => new CardNumber("this card number is going to be way to long");
            a.Should().ThrowExactly<PaymentAggregateException>();
        }

        [Fact]
        public void ContainsNonDigits()
        {
            Action a = () => new CardNumber("1234567891011abc");
            a.Should().ThrowExactly<PaymentAggregateException>();
        }

        // https://www.getcreditcardnumbers.com/
        [Theory]
        [InlineData("38520000023231")]
        [InlineData("4024007181932131")]
        [InlineData("4556072498801391")]
        [InlineData("4929061424591831")]
        [InlineData("4916550755213031")]
        [InlineData("4929500402283891")]
        [InlineData("5108023381750291")]
        [InlineData("5193605957623551")]
        [InlineData("5595108497359041")]
        [InlineData("5536689242785661")]
        [InlineData("5514271119717971")]
        [InlineData("6011480225585801")]
        [InlineData("6011386661160021")]
        [InlineData("6011495544959551")]
        [InlineData("6011563271013742")]
        [InlineData("6011278332089551")]
        [InlineData("377867071471101")]
        [InlineData("375415041309221")]
        [InlineData("344409498386871")]
        [InlineData("346192973990071")]
        [InlineData("343692298309051")]
        public void FailsChecksum(string cardNumber)
        {
            Action a = () => new CardNumber(cardNumber);
            a.Should().ThrowExactly<PaymentAggregateException>();
        }

        [Theory]
        [InlineData("38520000023237")]
        [InlineData("4024007181932137")]
        [InlineData("4556072498801397")]
        [InlineData("4929061424591834")]
        [InlineData("4916550755213039")]
        [InlineData("4929500402283898")]
        [InlineData("5108023381750290")]
        [InlineData("5193605957623558")]
        [InlineData("5595108497359043")]
        [InlineData("5536689242785667")]
        [InlineData("5514271119717978")]
        [InlineData("6011480225585809")]
        [InlineData("6011386661160025")]
        [InlineData("6011495544959550")]
        [InlineData("6011563271013741")]
        [InlineData("6011278332089552")]
        [InlineData("377867071471103")]
        [InlineData("375415041309227")]
        [InlineData("344409498386877")]
        [InlineData("346192973990078")]
        [InlineData("343692298309055")]
        public void ValidCardNumbers(string cardNumber)
        {
            Action a = () => new CardNumber(cardNumber);
            a.Should().NotThrow();
        }
    }
}
