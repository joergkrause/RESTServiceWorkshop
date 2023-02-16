using Microsoft.EntityFrameworkCore;

namespace LabelService.Infrastructure.Persistence
{
  public class LabelContext : DbContext
  {
    public LabelContext(DbContextOptions<LabelContext> options) : base(options)
    {
    }

    public DbSet<Domain.Models.Device> Devices { get; set; }
    public DbSet<Domain.Models.Label> Labels { get; set; }
  }
}
