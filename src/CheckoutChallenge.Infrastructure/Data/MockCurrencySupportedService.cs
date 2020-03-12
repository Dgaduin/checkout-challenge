using System.Threading.Tasks;
using CheckoutChallenge.Domain.PaymentAggregate;
using CheckoutChallenge.Domain.PaymentAggregate.Services;

namespace CheckoutChallenge.Infrastructure.Data
{
    public class MockCurrencySupportedService : ICurrencySupportedService
    {
        public Task<bool> IsCurrencySupported(Currency currency) => Task.FromResult(true);
    }
}