using AutoMapper;
using LabelService.Controllers.Dtos;
using LabelService.Domain.Models;

namespace LabelService.Controllers.Mappings
{
  public class DomainMapping : Profile
  {
    public DomainMapping()
    {
      CreateMap<Label, LabelDto>()
        .ForMember(dest => dest.DeviceId, opt => opt.MapFrom(src => src.Device.Id));
      CreateMap<LabelDto, Label>()
        .ForMember(dest => dest.Device, opt => opt.Ignore());

      CreateMap<Device, DeviceListDto>();
      CreateMap<Device, DeviceDto>()
        .ForMember(dest => dest.HasLabels, opt => opt.MapFrom(src => src.Labels.Any()));
    }
  }
}
