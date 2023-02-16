using LabelService.Domain.Validation;
using System.ComponentModel.DataAnnotations;

namespace LabelService.Controllers.Dtos
{
  public class LabelDto
  {
    public int Id { get; set; }

    [Required]
    [StringLength(25)]
    public string Name { get; set; } = default!;

    [Required]
    [RegularExpression(@"\d{10}")]
    public string TrackingId { get; set; } = default!;

    [Required()]
    [StringLength(100)]
    [AddressValidation('A')]
    public string Address { get; set; } = default!;

    public int DeviceId { get; set; }
  }
}
