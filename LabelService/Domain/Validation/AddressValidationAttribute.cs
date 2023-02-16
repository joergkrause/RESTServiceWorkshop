using System.ComponentModel.DataAnnotations;

namespace LabelService.Domain.Validation
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
  public class AddressValidationAttribute: ValidationAttribute
  {

    public AddressValidationAttribute(char startsWith)
    {
      StartsWith = startsWith;
    }

    public override bool IsValid(object? value)
    {
      var val = value as string;
      return val.StartsWith(StartsWith);
    }

    public char StartsWith { get; set; }

  }
}
