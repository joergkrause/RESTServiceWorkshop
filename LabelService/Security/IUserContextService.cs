using System.Security.Claims;

namespace LabelService.Security
{
  public interface IUserContextService
  {
    ClaimsPrincipal Principal { get; set; }
  }
}