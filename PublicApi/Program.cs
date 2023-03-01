using LabelServiceClient;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

HttpClient getClient(IServiceProvider ctx)
{
  var httpContextAccessor = ctx.GetRequiredService<IHttpContextAccessor>();
  var degHandler = new ApiAuthDelegatingHandler(httpContextAccessor, builder.Configuration);
  var httpClient = new HttpClient(degHandler);
  return httpClient;
}
// Add services to the container.
builder.Services.AddSingleton<IClient>(sp =>
{
  var client = new Client(builder.Configuration["BackendUrl"], getClient(sp));  
  return client;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
