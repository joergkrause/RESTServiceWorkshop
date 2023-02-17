using LabelServiceClient;
using PublicApi.Security.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IClient>(sp => {
  var basicAuthHandler = new BasicAuthMessageHandler(builder.Configuration["BackendUsername"], builder.Configuration["BackendPassword"]);
  var httpClient = new HttpClient(basicAuthHandler);
  var client = new Client(builder.Configuration["BackendUrl"], httpClient);
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

app.UseAuthorization();

app.MapControllers();

app.Run();
