using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Shared.DataTransferObjects;
using TimeTracker.Extensions;
using NLog;
using Service.DataShaping;
using TimeTracker.Presentation.ActionFilters;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureServiceManager();

builder.Services.AddControllers(config => {
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters();

builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.AddScoped<IDataShaper<ClockworkTaskDto>, DataShaper<ClockworkTaskDto>>();


builder.Services.AddControllers()
    .AddApplicationPart(typeof(TimeTracker.Presentation.AssemblyReference).Assembly);

builder.Services.AddControllers(options =>
{
    options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers()
    .AddNewtonsoftJson();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

if (app.Environment.IsProduction())
    app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions {
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();