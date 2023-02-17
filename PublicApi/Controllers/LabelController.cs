using LabelServiceClient;
using Microsoft.AspNetCore.Mvc;

namespace LabelService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
  [Produces("application/json")]
  public class LabelController : ControllerBase
  {

    private readonly LabelServiceClient.IClient _labelClient;

    public LabelController(IServiceProvider provider)
    {
      _labelClient = provider.GetRequiredService<LabelServiceClient.IClient>();
    }

    /// <summary>
    /// Get all labels.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<LabelDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
      var labels = await _labelClient.LabelAllAsync();
      return Ok(labels);
    }

    /// <summary>
    /// Get single label.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(typeof(IEnumerable<LabelDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
      try
      {
        var labels = await _labelClient.LabelAll2Async(id);
        return Ok(labels);
      }
      catch (SwaggerLabelException ex) when (ex.StatusCode == StatusCodes.Status404NotFound)
      {
        return BadRequest();
      }
    }

    [HttpGet]
    [Route("bytrackingid/{trackingId:long}")]
    [ProducesResponseType(typeof(LabelDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get([FromRoute] long trackingId)
    {
      try
      {
        var labels = await _labelClient.BytrackingidAsync(trackingId);
        return Ok(labels);
      }
      catch (SwaggerLabelException ex) when (ex.StatusCode == StatusCodes.Status404NotFound)
      {
        return BadRequest();
      }
    }

    [HttpGet]
    [Route("searchbyname")]
    [ProducesResponseType(typeof(IEnumerable<LabelDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SearchByName([FromQuery(Name = "name")] string labelName)
    {
      try
      {
        var labels = await _labelClient.SearchbynameAsync(labelName);
        return Ok(labels);
      }
      catch (SwaggerLabelException ex) 
      {
        return BadRequest();
      }
    }

    [HttpPost]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddLabel([FromBody] LabelDto labelDto)
    {
      try
      {
        await _labelClient.LabelPOSTAsync(labelDto);
        return Ok();
      }
      catch (SwaggerLabelException ex) 
      {
        return BadRequest();
      }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    public async Task<IActionResult> ChangeLabel(int id, [FromBody] LabelDto labelDto)
    {
      try
      {
        await _labelClient.LabelPUTAsync(id, labelDto);
        return Ok();
      }
      catch (SwaggerLabelException ex) 
      {
        return BadRequest();
      }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status202Accepted)]
    public async Task<IActionResult> DeleteLabel(int id)
    {
      try
      {
        await _labelClient.LabelDELETEAsync(id);
        return Accepted();
      }
      catch (SwaggerLabelException ex)
      {
        return BadRequest();
      }
    }
  }
}
