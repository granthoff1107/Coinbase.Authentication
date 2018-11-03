using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Coinbase.Authentication.Coinbase
{
    public class CoinbaseOptions : OAuthOptions
    {

        public CoinbaseOptions()
        {
            CallbackPath = new PathString("/signin-coinbase");
            AuthorizationEndpoint = CoinbaseDefaults.AuthorizationEndpoint;
            TokenEndpoint = CoinbaseDefaults.TokenEndpoint;
            UserInformationEndpoint = CoinbaseDefaults.UserInformationEndpoint;
            Scope.Add("wallet:user:read");
            Scope.Add("wallet:user:email");

            ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
            ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
       }
        /// <summary>
        /// A limit to the amount of money your application can send from the user’s account. This will be displayed on the authorize screen
        /// </summary>
        public int? SendLimitAmount { get; set; }

        /// <summary>
        /// How often the send money limit expires
        /// </summary>
        public SendLimitPeriod? SendLimitPeriod { get; set; }

        /// <summary>
        /// Supported fiat currency of send_limit_amount in ISO format, ex. EUR, USD 
        /// (For Bitcoin use BTC...)
        /// </summary>
        public string SendLimitCurrency { get; set; }
    }
}