using LabelService.Domain.Models;

namespace LabelService.Controllers.Dtos
{
  public class LabelDto
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string TrackingId { get; set; }

    public string Address { get; set; }

    public int DeviceId { get; set; }
  }
}
