using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckoutChallenge.Infrastructure.Security
{
    public class MockSecurityProvider : ISecurityProvider
    {
        private readonly Dictionary<string, Guid> _merchantKeys = new Dictionary<string, Guid>{
            {"thisIsKey1",new Guid("B75D838E-E7B0-4B7B-BDA3-1903008085E6")},
            {"thisIsKey2",new Guid("DA240535-2B38-4376-AD5C-75520BED9BF2")},
            {"thisIsKey3",new Guid("D4102F94-A06E-48D5-9251-00746C0370F2")},
            {"thisIsKey4",new Guid("F8876D21-FC27-4BC6-9DB9-5252AF00FD87")}
        };

        public Task<Guid?> GetMerchantFromApiKey(string apiKey)
        {
            if (_merchantKeys.TryGetValue(apiKey, out Guid id))
            {
                return Task.FromResult<Guid?>(id);
            }
            return Task.FromResult<Guid?>(null);
        }
    }
}
