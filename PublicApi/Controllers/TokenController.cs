using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PublicApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TokenController : ControllerBase
  {


    [HttpGet]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public IActionResult Get(string clientId, string clientSecret)
    {
      //
      return Ok("Token");
    }
  }
}
