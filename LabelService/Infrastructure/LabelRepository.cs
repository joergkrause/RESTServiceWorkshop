using LabelService.Domain.Interface;
using LabelService.Domain.Models;
using LabelService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace LabelService.Infrastructure
{
    public class LabelRepository : ILabelRepository
  {

    private readonly LabelContext _context;

    public LabelRepository(LabelContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Label>> GetLabels()
    {
      var count = await _context.Labels.CountAsync();
      if (count > 1000)
      {
        throw new ArgumentOutOfRangeException("to many labels, use paging method");
      }
      var models = await _context.Labels.ToListAsync();
      return models;
    }

    public async Task<int> CountLabels()
    {
      return await _context.Labels.CountAsync();
    }

    public async Task<Label?> GetLabelById(int id)
    {
      var label = await _context.Labels
        .Include(e => e.Device)
        .SingleOrDefaultAsync(e => e.Id == id);
      return label;
    }

    public async Task<IEnumerable<Label>> GetLabelsByName(string labelName)
    {
      var labels = await _context.Labels
        .Include(e => e.Device)
        .Where(e => e.Name.Contains(labelName))
        .ToListAsync();
      return labels;
    }

    public async Task<Label?> GetLabelByTrackingId(string trackingId)
    {
      var label = await _context.Labels
        .Include(e => e.Device)
        .SingleOrDefaultAsync(e => e.TrackingId == trackingId);
      return label;
    }

    public async Task<bool> InsertOrUpdate(Label label, int deviceId = 0)
    {
      if (deviceId > 0)
      {
        var device = await _context.Devices.FindAsync(deviceId);
        if (device == null)
        {
          return false;
        }
        label.Device = device;
      }
      
      _context.Entry(label).State = label.Id == 0 ? EntityState.Added : EntityState.Modified;
      var result = await _context.SaveChangesAsync();
      return result > 0;
    }
  }
}
