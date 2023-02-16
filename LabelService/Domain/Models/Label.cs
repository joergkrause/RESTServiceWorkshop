namespace LabelService.Domain.Models
{
  public class Label : EntityBase
  {
    public string Name { get; set; } = default!;

    public string TrackingId { get; set; } = default!;

    public string Address { get; set; } = default!;

    public Device Device { get; set; } = default!;

  }
}
