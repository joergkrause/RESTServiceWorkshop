using System.Security.Claims;

namespace LabelService.Security
{
  public class UserContextService : IUserContextService
  {
    public ClaimsPrincipal Principal { get; set; }
  }
}
