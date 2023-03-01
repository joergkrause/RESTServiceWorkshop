using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace LabelServiceClient;

public class ApiAuthDelegatingHandler : DelegatingHandler
{

  private readonly IHttpContextAccessor _context;
  private readonly string backendSecret;
  
  public ApiAuthDelegatingHandler(IHttpContextAccessor context, IConfiguration configuration)
  {
    _context = context;
    backendSecret = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{configuration["BackendUsername"]}:{configuration["BackendPassword"]}"));    
    InnerHandler = new HttpClientHandler();
  }

  protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
  {
    request.Headers.Authorization = new AuthenticationHeaderValue("Basic", $"{backendSecret}");
    return base.SendAsync(request, cancellationToken);
  }

}
