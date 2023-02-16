using LabelService.Domain.Models;
using LabelService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LabelService.Infrastructure
{
  public class LabelRepository
  {

    private readonly LabelContext _context;
    
    public LabelRepository(LabelContext context) {
      _context = context;
    }

    public async Task<Label?> GetLabelByIdAsync(int id)
    {
      var label = await _context.Labels
        .Include(e => e.Device)
        .SingleOrDefaultAsync(e => e.Id == id);      
      return label;
    }



  }
}
