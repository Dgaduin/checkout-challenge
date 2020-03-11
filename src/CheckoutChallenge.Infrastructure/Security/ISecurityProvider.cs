using System;
using System.Threading.Tasks;

namespace CheckoutChallenge.Infrastructure.Security
{
    public interface ISecurityProvider
    {
        Task<Guid?> GetMerchantFromApiKey(string apiKey);
    }
}
