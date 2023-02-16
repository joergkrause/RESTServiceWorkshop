namespace LabelService.Domain.Models
{
  public class Device : EntityBase
  {
    public string Name { get; set; } = default!;

    public int Type { get; set; }

    public ICollection<Label> Labels { get; set; } = new HashSet<Label>();
  }
}
