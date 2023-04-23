using System.Text;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Contracts;
using Entities.Models;
using LoggerService;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Service;
using Service.Contracts;

namespace TimeTracker.Extensions;

public static class ServiceExtensions {
    public static void ConfigureCors(this IServiceCollection services) {
        services.AddCors(options => {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-Pagination"));
        });
    }

    public static void ConfigureIISIntegration(this IServiceCollection services) {
        services.Configure<IISOptions>(_ => {});
    }

    public static void ConfigureLoggerService(this IServiceCollection services) {
        services.AddSingleton<ILoggerManager, LoggerManager>();
    }

    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<RepositoryContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

    public static void ConfigureServiceManager(this IServiceCollection services) =>
        services.AddScoped<IServiceManager, ServiceManager>();

    public static void AddCustomMediaTypes(this IServiceCollection services) {
        services.Configure<MvcOptions>(config => {
            var newtonsoftJsonOutputFormatter = config.OutputFormatters
                .OfType<NewtonsoftJsonOutputFormatter>()?.FirstOrDefault();
            if (newtonsoftJsonOutputFormatter == null)
                return;

            newtonsoftJsonOutputFormatter.SupportedMediaTypes
                .Add("application/vnd.marvel.hateoas+json");
            newtonsoftJsonOutputFormatter.SupportedMediaTypes
                .Add("application/vnd.marvel.apiroot+json");

            var newtonsoftXmlOutputFormatter = config.OutputFormatters
                .OfType<XmlDataContractSerializerOutputFormatter>()?.FirstOrDefault();
            if (newtonsoftXmlOutputFormatter == null)
                return;

            newtonsoftXmlOutputFormatter.SupportedMediaTypes
                .Add("application/vnd.marvel.hateoas+xml");
        });
    }

    public static void ConfigureVersioning(this IServiceCollection services) {
        services.AddApiVersioning(options => {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
        });
    }

    public static void ConfigureResponseCaching(this IServiceCollection services) {
        services.AddResponseCaching();
    }

    public static void ConfigureHttpCacheHeaders(this IServiceCollection services) {
        services.AddHttpCacheHeaders(
            (expirationOpt) => {
                expirationOpt.MaxAge = 65;
                expirationOpt.CacheLocation = CacheLocation.Private;
            },
            (validationOpt) => {
                validationOpt.MustRevalidate = true;
            });
    }
    
    public static void ConfigureRateLimitingOptions(this IServiceCollection services) {
        var rateLimitRules = new List<RateLimitRule> {
            new() {
                Endpoint = "*",
                Limit = 30,
                Period = "1m"
            }
        };
        services.Configure<IpRateLimitOptions>(opt => {
            opt.GeneralRules = rateLimitRules;
        });
        services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
        services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
    }
    
    public static void ConfigureIdentity(this IServiceCollection services) {
        
        var builder = services.AddIdentity<User, IdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<RepositoryContext>()
            .AddDefaultTokenProviders();
    }

    public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration) {
        var jwtSettings = configuration.GetSection("JWTSettings");
        //var secretKey = Environment.GetEnvironmentVariable("SECRET");
        var secretKey = configuration.GetSection("SECRET").Value;

        services.AddAuthentication(opt => {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            )
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new
                        SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey ?? throw new InvalidOperationException("Secret key is null")))
                };
            });
    }
}