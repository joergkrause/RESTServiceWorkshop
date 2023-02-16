using LabelService.Domain.Models;
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


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Device>().Property(d => d.Name).HasMaxLength(50).IsRequired();

      modelBuilder.Entity<Label>().Property(d => d.Name).HasMaxLength(25).IsRequired();
      modelBuilder.Entity<Label>().Property(d => d.TrackingId).HasMaxLength(25).IsUnicode(false).IsRequired();
      modelBuilder.Entity<Label>().Property(d => d.Address).HasMaxLength(100).IsRequired();
      
    }

  }
}
