using SpaceManagment.Common;
using SpaceManagment.Configuration;
using SpaceManagment.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();
Settings settings = config.GetRequiredSection("Settings").Get<Settings>();

builder.Services.AddControllers();

builder.AddLogger(settings);
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.Configure<Settings>(builder.Configuration.GetSection("Settings"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//ServiceCollectionExtensions
builder.AddIdentity(settings.IdentitySettings);
builder.AddDbContext(settings.ConnectionStrings);
builder.AddSwagger();
builder.AddAuthenticationJwt(settings.JwtSettings);
builder.AddDependency();
builder.AddAuthorization();

var app = builder.Build();

app.UseMiddleware<CustomExeptionHandlerMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();


