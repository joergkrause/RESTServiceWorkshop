using LabelService.Domain.Models;

namespace LabelService.Controllers.Dtos
{
  public class LabelDto
  {
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string TrackingId { get; set; } = default!;

    public string Address { get; set; } = default!;

    public int DeviceId { get; set; }
  }
}
