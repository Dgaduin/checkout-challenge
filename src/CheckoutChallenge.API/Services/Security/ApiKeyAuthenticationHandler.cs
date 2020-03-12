using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using CheckoutChallenge.Infrastructure.Security;

namespace CheckoutChallenge.API.Services
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ISecurityProvider _securityProvider;

        public ApiKeyAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
                                           ILoggerFactory logger,
                                           UrlEncoder encoder,
                                           ISystemClock clock,
                                           ISecurityProvider securityProvider) : base(options, logger, encoder, clock) =>
            _securityProvider = securityProvider;

        /// <summary>
        /// Handles the authentication, asynchronously.
        /// </summary>
        /// <returns></returns>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (Request.Headers.TryGetValue("Api-Key", out var value))
            {
                if (value.Count > 0)
                {
                    var apiKey = value[0];
                    var merchantId = _securityProvider.GetMerchantFromApiKey(apiKey);
                    if (merchantId != null)
                    {
                        // Here we can fetch the additional details about the merchant if we care about them

                        ClaimsIdentity identity = new ClaimsIdentity(Scheme.Name);
                        identity.AddClaim(new Claim("MerchantId", merchantId.ToString()));

                        var principal = new ClaimsPrincipal(identity);
                        var ticket = new AuthenticationTicket(principal, Scheme.Name);
                        return AuthenticateResult.Success(ticket);
                    }
                }
            }

            return AuthenticateResult.Fail("Invalid");
        }
    }
}
