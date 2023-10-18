using System.Text;
using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Microsoft.OpenApi.Models;
using StudentHub.Core.DTOs;
using StudentHub.Core.Interfaces;
using StudentHub.Core.Services;
using StudentHub.Core.WorkModel;
using StudentHub.Middlewares;

namespace StudentHub;

/// <summary>
/// Startup class with configurations
/// </summary>
public class Startup
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configuration"></param>
    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Configures services
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IEmailService, EmailService>();

        services.AddSingleton<IDriveService, GoogleDriveService>();

        services.Configure<SmtpConfigModel>(_configuration.GetSection("SMTPConfig"));

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer("name=Data:ConnectionString"));

        services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireDigit = false;
                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        var jwtConfig = _configuration.GetSection("jwtConfig");
        var secretKey = jwtConfig["secret"];
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfig["validIssuer"],
                ValidAudience = jwtConfig["validAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
            };
        });

        services.AddCors();
        services.AddAuthorization();
        services.AddControllers().AddNewtonsoftJson(opt =>
        {
            opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            opt.SerializerSettings.Converters.Add(new StringEnumConverter());
        });
        services.AddSwaggerGen();
        services.AddSwaggerGen(swaggerGenOptions =>
        {
            swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Student Hub API",
                Version = "v1"
            });
            swaggerGenOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });
            swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
            swaggerGenOptions.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "StudentHub.xml"));
        });
    }

    /// <summary>
    /// Configures app
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        app.UseMiddleware<InitialMiddleware>();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapFallbackToFile("index.html");
        });
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
