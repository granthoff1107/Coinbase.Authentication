# Coinbase.Authentication
Support For Coinbase OAuth in .Net-Core Projects

# Example Application Url
https://github.com/granthoff1107/OAuthCoinbase

# Requirements 

You need a client Id and secret obtained from coinbase

#Use
            
         public static readonly List<string> COINBASE_SCOPES = new List<string> {
            "wallet:accounts:read",
            "wallet:addresses:read",
            "wallet:buys:read",
            "wallet:checkouts:read",
            "wallet:contacts:read",
            "wallet:notifications:read",
            "wallet:orders:read",
            "wallet:transactions:read",
            "wallet:transactions:request",
            "wallet:transactions:send",
            "wallet:transactions:transfer",
            "wallet:user:email",
            "wallet:user:read",
            "wallet:withdrawals:create",
        };   
            
         
        services.AddAuthentication(options =>
        {
             options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
             options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
             options.DefaultChallengeScheme = CoinbaseDefaults.AuthenticationScheme;
        })
        .AddCoinbase(options =>
        {
            //The wallet:transactions:send scope requires Send limit
            options.SendLimitAmount = 1;
            options.SendLimitCurrency = "USD";
            options.SendLimitPeriod = SendLimitPeriod.day;
            options.ClientId = Configuration["Coinbase:ClientId"];
            options.ClientSecret = Configuration["Coinbase:ClientSecret"];
            COINBASE_SCOPES.ForEach(scope => options.Scope.Add(scope));
            options.SaveTokens = true;
            
            //Map Additional Claims
            options.ClaimActions.MapJsonKey("urn:coinbase:avatar", "avatar_url");
        });
