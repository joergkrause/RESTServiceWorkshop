﻿namespace LabelService.Controllers.Dtos
{
  public class DeviceDto
  {

    public int Id { get; set; }
    
    public string Name { get; set; }

    public int Type { get; set; }

    public bool HasLabels { get; set; }
  }
}
