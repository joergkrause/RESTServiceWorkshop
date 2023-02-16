using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LabelService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DeviceController : ControllerBase
  {

    [HttpGet]
    public IActionResult Get()
    {
      // Validierung
      // Datenbankabfrage via Repository
      // Anmerkung: Darf keine Domain-Objekte zurückgeben, keine Geschäftslogik
      // Konvertierung in DTO
      return Ok();
    }
  }
}
