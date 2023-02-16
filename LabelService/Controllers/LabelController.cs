using AutoMapper;
using LabelService.Controllers.Dtos;
using LabelService.Domain.Interface;
using LabelService.Domain.Models;
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

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
      var model = await _labelRepo.GetLabelById(id);
      if (model == null)
      {
        return NotFound();
      }
      var dto = _mapper.Map<LabelDto>(model);
      return Ok(dto);
    }

    [HttpGet]
    [Route("bytrackingid/{trackingId:long}")]
    public async Task<IActionResult> Get([FromRoute] long trackingId)
    {      
      var model = await _labelRepo.GetLabelByTrackingId(trackingId.ToString());
      if (model == null)
      {
        return NotFound();
      }
      var dto = _mapper.Map<LabelDto>(model);
      return Ok(dto);
    }

    [HttpGet]
    [Route("searchbyname")]
    public async Task<IActionResult> SearchByName([FromQuery(Name = "name")] string labelName)
    {
      if (String.IsNullOrEmpty(labelName))
      {
        return BadRequest("empty query is not allowed");
      }
      var models = await _labelRepo.GetLabelsByName(labelName);
      if (!models.Any())
      {
        return NoContent();
      }
      var dto = _mapper.Map<IEnumerable<LabelDto>>(models);
      return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> AddLabel([FromBody] LabelDto labelDto)
    {
      if (ModelState.IsValid)
      {
        var label = _mapper.Map<Label>(labelDto);
        var success = await _labelRepo.InsertOrUpdate(label, labelDto.DeviceId);
        return success ? Ok() : BadRequest();
      }
      return UnprocessableEntity();     
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ChangeLabel(int id, [FromBody] LabelDto labelDto)
    {
      if (id != labelDto.Id)
      {
        return Conflict();
      }
      if (ModelState.IsValid)
      {
        var label = _mapper.Map<Label>(labelDto);
        var success = await _labelRepo.InsertOrUpdate(label, labelDto.DeviceId);
        return success ? Ok() : BadRequest();
      }
      return UnprocessableEntity();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLabel(int id)
    {
      // NotFound()
      return Accepted();
    }
  }
}
