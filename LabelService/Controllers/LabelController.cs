using AutoMapper;
using LabelService.Controllers.Dtos;
using LabelService.Domain.Interface;
using LabelService.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LabelService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LabelController : ControllerBase
  {

    private readonly ILabelRepository _labelRepo;
    private readonly IMapper _mapper;

    public LabelController(IServiceProvider provider)
    {
      _labelRepo = provider.GetRequiredService<ILabelRepository>();
      _mapper = provider.GetRequiredService<IMapper>();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var models = await _labelRepo.GetLabels();
      var dtos = _mapper.Map<IEnumerable<LabelDto>>(models);
      return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      var model = await _labelRepo.GetLabelById(id);
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
