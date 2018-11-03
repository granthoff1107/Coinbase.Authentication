using System;
using System.Collections.Generic;
using System.Text;

namespace Coinbase.Authentication.Coinbase
{
    public static class CoinbaseDefaults
    {
        public const string AuthenticationScheme = "Coinbase";

        public static readonly string DisplayName = "Coinbase";

        public static readonly string AuthorizationEndpoint = "https://www.coinbase.com/oauth/authorize";

        public static readonly string TokenEndpoint = "https://api.coinbase.com/oauth/token";

        public static readonly string UserInformationEndpoint = "https://api.coinbase.com/v2/user";
    }
}
