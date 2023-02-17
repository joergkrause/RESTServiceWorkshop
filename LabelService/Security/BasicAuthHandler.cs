using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace LabelService.Security
{
  public class BasicAuthHandler : AuthenticationHandler<BasicAuthOptions>
  {    
      
    private readonly IConfiguration _configuration;
    private readonly IUserContextService _userContextService;

    public BasicAuthHandler(
      IOptionsMonitor<BasicAuthOptions> options, 
      ILoggerFactory logger, 
      UrlEncoder encoder, 
      ISystemClock clock,
      IConfiguration configuration,
      IUserContextService userContextService
      )
      : base(options, logger, encoder, clock)
    {
      _configuration = configuration;
      _userContextService = userContextService;
    }


    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {

      if (!Request.Headers.ContainsKey("Authorization"))
        return AuthenticateResult.Fail("Missing Authorization Header");

      if (!AuthenticationHeaderValue.TryParse(Request.Headers["Authorization"], out AuthenticationHeaderValue headerValue))
      {
        //Invalid Authorization header
        return AuthenticateResult.NoResult();
      }

      if (!headerValue.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase))
      {
        //Invalid Authorization header
        return AuthenticateResult.NoResult();
      }

      var credentialBytes = Convert.FromBase64String(headerValue.Parameter!);
      var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
      var username = credentials[0];
      var password = credentials[1];

      if (username != _configuration["BackendUsername"] || password != _configuration["BackendPassword"])
      {
        //Invalid Authorization header
        return AuthenticateResult.NoResult();
      }

      var claims = new Claim[] { }; //new Claim(ClaimTypes.NameIdentifier, username)
      var identity = new ClaimsIdentity(claims, Scheme.Name);
      var principal = new ClaimsPrincipal(identity);

      _userContextService.Principal = principal;

      var ticket = new AuthenticationTicket(principal, Scheme.Name);

      return await Task.FromResult(AuthenticateResult.Success(ticket));

    }
  }

  public class BasicAuthOptions : AuthenticationSchemeOptions
  {
    public string Realm { get; set; } = "Default";
    public string Charset { get; set; } = "ASCII";
  }
}
