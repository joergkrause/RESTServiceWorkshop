using System.Net.Http.Headers;
using System.Text;

namespace PublicApi.Security.Middleware
{
  public class BasicAuthMessageHandler : DelegatingHandler
  {
    private readonly string _username;
    private readonly string _password;

    public BasicAuthMessageHandler(string username, string password)
    {
      _username = username;
      _password = password;
      InnerHandler = new HttpClientHandler();
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      var encodedCred = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_username}:{_password}"));


      request.Headers.Authorization = new AuthenticationHeaderValue("Basic", encodedCred);
      return await base.SendAsync(request, cancellationToken);
    }
  }
}
