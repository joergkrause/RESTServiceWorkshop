using LabelService.Controllers.Mappings;
using LabelService.Domain.Interface;
using LabelService.Domain.Models;
using LabelService.Infrastructure;
using LabelService.Infrastructure.Persistence;
using LabelService.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<LabelContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("LabelContext")));
builder.Services.AddScoped<ILabelRepository, LabelRepository>();
builder.Services.AddScoped<IUserContextService, UserContextService>();

builder.Services.AddAuthentication("Basic").AddScheme<BasicAuthOptions, BasicAuthHandler>("Basic", null);
builder.Services.AddAuthorization(opt =>
{
  opt.AddPolicy("InsertAdmin", policy =>
  {
    policy.RequireAuthenticatedUser();
    policy.RequireClaim("role", "admin");
    policy.RequireRole("Admin");
    policy.Build();
  });
  opt.AddPolicy("User", policy => policy.RequireClaim("role", "user"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
  opt.AddServer(new Microsoft.OpenApi.Models.OpenApiServer { Url = builder.Configuration["OpenApi:ServerUrl"], Description = "Demo Server Endkunde" });
  opt.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
  {
    Title = "Workshop Demo",
    Version = "v1",
    Contact = new Microsoft.OpenApi.Models.OpenApiContact
    {
      Name = "Joerg Krause",
      Email = "joerg@joergkrause.de"
    },
    License = new Microsoft.OpenApi.Models.OpenApiLicense
    {
      Name = "No License"
    },
    Description = "Nur zur Demo im Workshop"
  });
  //opt.IncludeXmlComments("");
  opt.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
  {
    Name = "Basic",
    Description = "Enter username and password",
    Type = SecuritySchemeType.Http,
    In = ParameterLocation.Header,
    Scheme = "basic"
  });

  opt.AddSecurityRequirement(new OpenApiSecurityRequirement
  {
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Basic" }
        },
        new string[]{}
    }
  });
});

builder.Services.AddAutoMapper(typeof(DomainMapping).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
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