# Coinbase.Authentication
Support For Coinbase OAuth in .Net-Core Projects


# Requirements 

You need a client Id and secret obtained from coinbase

# Use Case From Startup
            
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

# Create an Authentication Controller

    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        public AccountController()
        {
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            return Challenge(new AuthenticationProperties() { RedirectUri = returnUrl });
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect(Url.Content("~/"));
        }
    }
    
# Create a View Model

    public class IndexModel : PageModel
    {
        public string CoinbaseId { get; set; }

        public string CoinbaseAvatar { get; set; }

        public string CoinbaseName { get; set; }
         
        public List<AccountDetail> AccountDetails { get; set; } = new List<AccountDetail>();

        public async Task OnGetAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                CoinbaseName = User.FindFirst(c => c.Type == ClaimTypes.Name)?.Value;
                CoinbaseId = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                CoinbaseAvatar = User.FindFirst(c => c.Type == "urn:coinbase:avatar")?.Value;
            }
        }
    }

# Simple View 

    @page
    @model IndexModel
    @{
        ViewData["Title"] = "Home page";
    }

    <div class="row">
        <div class="col-md-12">
            @if (!User.Identity.IsAuthenticated)
            {
                <a asp-action="Login" asp-controller="Account" class="btn btn-default">Coinbase Login</a>
            }
            else
            {

                <div class="row">
                    <div class="col-md-2">
                        <h4>@Model.CoinbaseName</h4>
                    </div>
                </div>
            }
        </div>
    </div>

                        
