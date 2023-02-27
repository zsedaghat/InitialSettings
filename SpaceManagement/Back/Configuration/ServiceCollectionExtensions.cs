using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using SpaceManagment.Common;
using SpaceManagment.Data;
using SpaceManagment.Infrastructure;
using SpaceManagment.Model;
using SpaceManagment.Service;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;


namespace SpaceManagment.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }

        public static void AddAuthenticationJwt(this WebApplicationBuilder builder, JwtSettings settings)
        {
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var Key = Encoding.UTF8.GetBytes(settings.Key);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = settings.Issuer,
                    ValidAudience = settings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Key),
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public static void AddIdentity(this WebApplicationBuilder builder, IdentitySettings settings)
        {
            builder.Services.AddIdentity<User, Role>(identityOptions =>
            {
                //Password Settings
                identityOptions.Password.RequireDigit = settings.PasswordRequireDigit;
                identityOptions.Password.RequiredLength = settings.PasswordRequiredLength;
                identityOptions.Password.RequireNonAlphanumeric = settings.PasswordRequireNonAlphanumic; //#@!
                identityOptions.Password.RequireUppercase = settings.PasswordRequireUppercase;
                identityOptions.Password.RequireLowercase = settings.PasswordRequireLowercase;

                // Default Lockout settings.
                identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                identityOptions.Lockout.MaxFailedAccessAttempts = 5;
                identityOptions.Lockout.AllowedForNewUsers = true;

                //UserName Settings
                //identityOptions.User.RequireUniqueEmail = settings.RequireUniqueEmail;
            })
                 .AddEntityFrameworkStores<SpaceManagmentDbContext>()
                 .AddDefaultTokenProviders();
        }

        public static void AddDbContext(this WebApplicationBuilder builder, ConnectionStrings settings)
        {

            builder.Services.AddDbContext<SpaceManagmentDbContext>(options =>
            {
                options
                    .UseSqlServer(settings.SqlServer);
                //Tips
                // .ConfigureWarnings(warning => warning.Throw(RelationalEventId.QueryClientEvaluationWarning));
            });
        }

        public static void AddDependency(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<DbContext, SpaceManagmentDbContext>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<IHostService, HostService>();
            builder.Services.AddScoped<ISupervisorService, SupervisorService>();
            builder.Services.AddScoped<ISpaceService, SpaceService>();
        }

        public static void AddAuthorization(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthorization(option =>
            {
                #region MyRegion
                ////claim
                //option.AddPolicy("testclaim", policy => policy.RequireClaim("User.GetUsers", true.ToString()));

                ////policy
                //option.AddPolicy("testPolicy", policy => policy.RequireAssertion(context =>
                //context.User.HasClaim("testclaim", true.ToString()) || context.User.IsInRole("Admin"))); 
                #endregion
                option.AddPolicy("Default", builder =>
                {
                    builder.AddRequirements(new PermissionRequirement("default"));
                });
                option.AddPolicy(AuthorazationAttributes.WithoutAuthorization, builder =>
                {
                    builder.AddRequirements(new PermissionRequirement(AuthorazationAttributes.WithoutAuthorization));
                });
            });
        }

        public static void AddLogger(this WebApplicationBuilder builder, Settings settings)
        {
            var columnOptions = new Serilog.Sinks.MSSqlServer.ColumnOptions
            {
                AdditionalColumns = new Collection<SqlColumn>
               {
                   new SqlColumn("UserName", SqlDbType.VarChar)
               }
            };
            var sinkOptions = new MSSqlServerSinkOptions()
            {
                TableName = "SeriLog",
                AutoCreateSqlTable = true
            };
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                          .Enrich.FromLogContext()
                          .WriteTo.MSSqlServer(
                settings.ConnectionStrings.SqlServer, sinkOptions: sinkOptions
                             , null, null, LogEventLevel.Debug, null, columnOptions: columnOptions, null, null
                  )
                          .WriteTo.Seq(settings.Seq.ServerUrl, LogEventLevel.Debug)
                          .CreateLogger();
            builder.Host.UseSerilog(Log.Logger);  
        }
    }
}
