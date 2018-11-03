using Coinbase.Authentication.Coinbase;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinbase.Authentication.Coinbase
{
    public static class CoinbaseExtensions
    {

        public static AuthenticationBuilder AddCoinbase(this AuthenticationBuilder builder)
            => builder.AddCoinbase(CoinbaseDefaults.AuthenticationScheme, _ => { });

        public static AuthenticationBuilder AddCoinbase(this AuthenticationBuilder builder, Action<CoinbaseOptions> configureOptions)
            => builder.AddCoinbase(CoinbaseDefaults.AuthenticationScheme, configureOptions);

        public static AuthenticationBuilder AddCoinbase(this AuthenticationBuilder builder, string authenticationScheme, Action<CoinbaseOptions> configureOptions)
            => builder.AddCoinbase(authenticationScheme, CoinbaseDefaults.DisplayName, configureOptions);

        public static AuthenticationBuilder AddCoinbase(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<CoinbaseOptions> configureOptions)
            => builder.AddOAuth<CoinbaseOptions, CoinbaseHandler>(authenticationScheme, displayName, configureOptions);

    }
}
