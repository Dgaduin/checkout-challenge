using System.Threading.Tasks;

namespace CheckoutChallenge.Domain.PaymentAggregate.Services
{
    public interface ICurrencySupportedService
    {
        Task<bool> IsCurrencySupported(Currency currency);
    }
}