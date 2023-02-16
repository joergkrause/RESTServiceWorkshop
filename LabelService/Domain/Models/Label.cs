namespace LabelService.Domain.Models
{
  public class Label : EntityBase
  {

    public string Name { get; set; }

    public string TrackingId { get; set; }

    public string Address { get; set; }

    public Device Device { get; set; }

  }
}
