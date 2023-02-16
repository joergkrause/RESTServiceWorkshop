namespace LabelService.Domain.Models
{
  public class Device : EntityBase
  {
    public string Name { get; set; }

    public int Type { get; set; }

    public ICollection<Label> Labels { get; set; }
  }
}
