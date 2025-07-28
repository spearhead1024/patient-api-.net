using PatientAdviceAPI.Interfaces;
using PatientAdviceAPI.Services;
using PatientAdviceAPI.Settings;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args);

builder.Services.AddOptions<OpenAISettings>()
    .Bind(builder.Configuration.GetSection(OpenAISettings.SectionName))
    .ValidateDataAnnotations();

builder.Services.AddHttpClient<ILLMService, OpenAIService>()
    .ConfigureHttpClient((sp, client) =>
    {
        var opts = sp.GetRequiredService<IOptions<OpenAISettings>>().Value;
        var baseUrl = opts.BaseUrl.TrimEnd('/') + "/";
        client.BaseAddress = new Uri(baseUrl);
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", opts.ApiKey);
    });

builder.Services.AddScoped<IPatientDataRepository, MockPatientDataRepository>();
builder.Services.AddScoped<IForecastingService, MockForecastingService>();
builder.Services.AddScoped<IAdviceService, AdviceService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo {
        Title = "Patient Advice API", Version = "v1",
        Description = "Generates health advice for patients using OpenAI’s LLM"
    });
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Patient Advice API v1");
        c.RoutePrefix = string.Empty;
    });
}
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }
