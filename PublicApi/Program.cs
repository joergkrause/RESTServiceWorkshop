using LabelServiceClient;
using Microsoft.AspNetCore.Identity;
using System.Net.Http;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IClient>(sp =>
{
  var client = new Client(builder.Configuration["BackendUrl"]);
  client.SetAuth(builder.Configuration["BackendUsername"], builder.Configuration["BackendPassword"]);
  return client;
});

//builder.Services.AddIdentity<UserModel, RoleModel>()
//  .AddDefaultTokenProviders()
//  .AddUserStore<CustomUserStore>()
//  .AddUserManager<CustomUserManager>()
//  .AddRoleStore<CustomRoleStore>()
//  .AddRoleManager<CustomRoleManager>()
//  ; // user/pw // clientsecret/clientid // apikey ==> Token (JWT)
//builder.Services.AddScoped<SignInManager<UserModel>, SignInManager<UserModel>>();

//builder.Services
//  .AddAuthentication(opt => opt.DefaultAuthenticateScheme = "Bearer")
//  .AddJwtBearer(opt =>
//  {

//  });

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
