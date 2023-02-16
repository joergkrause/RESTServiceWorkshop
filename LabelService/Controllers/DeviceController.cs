using AutoMapper;
using LabelService.Controllers.Dtos;
using LabelService.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LabelService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DeviceController : ControllerBase
  {

    private readonly LabelRepository _labelRepo;
    private readonly IMapper _mapper;

    public DeviceController(IServiceProvider provider)
    {
      _labelRepo = provider.GetRequiredService<LabelRepository>();
      _mapper = provider.GetRequiredService<IMapper>();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      // Validierung
      // Datenbankabfrage via Repository
      // Anmerkung: Darf keine Domain-Objekte zurückgeben, keine Geschäftslogik
      // Konvertierung in DTO
      // Rückgabe HTTP konform
      return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      var model = await _labelRepo.GetLabelByIdAsync(id);
      if (model == null)
      {
        return NotFound();
      }
      var dto = _mapper.Map<LabelDto>(model);
      return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] DeviceDto deviceDto)
    {
      // BadRequest() -> 400
      // ValidationError -> 422
      return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] DeviceDto deviceDto)
    {
      // NotFound() -> 404
      // BadRequest() -> 400
      // Conflict() -> 409
      // ValidationError -> 422
      return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      return Ok();
    }
  }
}
