using System;
using System.Threading.Tasks;

namespace CheckoutChallenge.Infrastructure.Security
{
    public interface ISecurityProvider
    {
        Task<bool> IsKeyValid(string apiKey, Guid merchantId);
    }
}
