using System.Text;

namespace LabelServiceClient
{
  public abstract class MySwaggerClientBase
  {
    public string Auth { get; private set; }

    public void SetAuth(string username, string password)
    {
      var encodedCred = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
      Auth = encodedCred;
    }

    // Called by implementing swagger client classes
    protected Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
    {
      var msg = new HttpRequestMessage();
      msg.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Auth);
      return Task.FromResult(msg);
    }

  }
}
