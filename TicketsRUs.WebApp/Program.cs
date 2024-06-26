using TicketsRUs.ClassLib.Data;
using TicketsRUs.ClassLib.Services;
using TicketsRUs.WebApp.Components;
using TicketsRUs.WebApp.Services;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using  Microsoft.AspNetCore.Builder;
using System.Diagnostics.Metrics;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllers();
builder.Services.AddSingleton<ITicketService, ApiTicketService>();
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddSingleton<ExampleHandler>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextFactory<PostgresContext>(options => options.UseNpgsql("Name=db"));


const string serviceName = "luris";

// builder.Logging.AddOpenTelemetry(logging => logging.AddOtlpExporter());
// builder.Services.AddLogging(builder => builder.AddConsole()); // may have a bug here

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService(serviceName))
    .WithTracing(tracing => tracing
    .AddAspNetCoreInstrumentation()
    .AddSource(DiagnosticsTrace.SourceName)
    .AddSource(DiagnosticsTrace.SourceName2)
    // .AddConsoleExporter()
    .AddOtlpExporter(o =>
    {
        o.Endpoint = new Uri("http://otel-collector:4317/");
    })
    )
    .WithMetrics(metrics =>
         metrics
        .AddAspNetCoreInstrumentation()
        .AddMeter(Meters.myServiceName)
        .AddMeter(Meters.histogramService)
        // .AddConsoleExporter()
        .AddPrometheusExporter()
        .AddOtlpExporter(o =>
        {
            o.Endpoint = new Uri("http://otel-collector:4317/");
        }
        )
    );

builder.Logging.AddOpenTelemetry(logs => 
    logs
        .AddConsoleExporter()
        .AddOtlpExporter(o =>
        {
            o.Endpoint = new Uri("http://otel-collector:4317/");
        }));

// builder.Logging.AddOpenTelemetry(logs => 
//     logs
//         .AddConsoleExporter()
//         .AddOtlpExporter(o =>
//         {
//             o.Endpoint = new Uri("http://otel-collector-service:4317/");
//         })); esto puede que se pero la verdad es que no tengo ni idea de que este funcionando tenes que ver el video

using ILoggerFactory factory = LoggerFactory.Create(builder =>
{
    builder.AddOpenTelemetry(logging =>
    {
        logging.AddOtlpExporter();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


var handler = app.Services.GetRequiredService<ExampleHandler>();
// app.MapGet("/log", ()=> {
//     Meters.LurisCount += 1
// })
// handler.HandleRequest());
app.MapGet("/log1", ()=>handler.HandleRequest());
app.MapGet("/log2", ()=>handler.HandleRequest());
app.MapGet("/log3", ()=>handler.HandleRequest());
app.MapGet("/log4", ()=>handler.HandleRequest());
app.MapGet("/log5", ()=>handler.HandleRequest());

app.MapGet("/healthCheck", () =>
{
    if (DateTime.Now.Second % 10 < 5)
    {
        return "healthy";
    }

    return "unhealthy";
});
app.UseOpenTelemetryPrometheusScrapingEndpoint();
// Swagger Components
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
//
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

public partial class Program { }
