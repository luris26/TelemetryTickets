using TicketsRUs.ClassLib.Data;
using TicketsRUs.ClassLib.Services;
using TicketsRUs.WebApp.Components;
using TicketsRUs.WebApp.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using System.Diagnostics.Metrics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllers();
builder.Services.AddSingleton<ITicketService, ApiTicketService>();
builder.Services.AddSingleton<IEmailService, EmailService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextFactory<PostgresContext>(options => options.UseNpgsql("Name=db"));


const string serviceName = "luris";

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService(serviceName))
    .WithTracing(tracing => tracing
    .AddAspNetCoreInstrumentation()
    .AddSource(DiagnosticsTrace.SourceName)
    .AddSource(DiagnosticsTrace.SourceName2)
    .AddConsoleExporter()
    .AddOtlpExporter(o =>
    {
        o.Endpoint = new Uri("http://otel-collector:4317/");
    })
    )
    .WithMetrics(metrics =>
         metrics
        .AddAspNetCoreInstrumentation()
        .AddMeter(Meters.myhomeworkmeter.Name)
        .AddConsoleExporter()
        .AddOtlpExporter(o =>
        {
            o.Endpoint = new Uri("http://otel-collector:4317/");
        }
        )
    );



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapGet("/healthCheck", () =>
{
    if (DateTime.Now.Second % 10 < 5)
    {
        return "healthy";
    }

    return "unhealthy";
});

// Swagger Components
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

public partial class Program { }
