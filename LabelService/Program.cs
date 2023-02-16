using LabelService.Controllers.Mappings;
using LabelService.Domain.Models;
using LabelService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<LabelContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("LabelContext")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(DomainMapping).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
await MigrateAsync();
app.Run();

async Task MigrateAsync()
{
  using var scope = app!.Services.CreateScope();
  var services = scope.ServiceProvider;

  var dbContext = services.GetRequiredService<LabelContext>();
  bool newDatabase = !dbContext.Database.GetService<IRelationalDatabaseCreator>().Exists();
  dbContext.Database.Migrate();
  var dbNotEmpty = await dbContext.Set<Device>().AnyAsync();
  if (newDatabase || !dbNotEmpty)
  {
    var device = new Device { Name = "Test", Type = 1 };
    var label = new Label { Device = device, Name = "L1", Address = "Anschrift Demo", TrackingId = "A1234" };
    dbContext.Labels.Add(label);
    await dbContext.SaveChangesAsync();
  }
}