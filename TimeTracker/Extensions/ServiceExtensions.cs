using Contracts;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Repository;
using Microsoft.EntityFrameworkCore;
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
}