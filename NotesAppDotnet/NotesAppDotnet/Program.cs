using Microsoft.EntityFrameworkCore;
using NotesAppDotnet.Data;
using NotesAppDotnet.Model;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddOptions<Settings>()
    .Bind(builder.Configuration.GetSection(Settings.ConfigurationSectionName));

builder.Services.AddControllers();
builder.Services.AddDbContext<NotesContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddDbContextCheck<NotesContext>();

builder.Host.UseSystemd();

// https://opentelemetry.io/docs/instrumentation/net/automatic/
builder.Services.AddOpenTelemetryMetrics(otel =>
{
    // Add instrumentation - metrics providers
    // CPU / Memory  + GC activity aka Runtime Metrics
    otel.AddRuntimeMetrics();
    // HTTP Server call durations per return code
    otel.AddAspNetCoreInstrumentation();

    // Register Prometheus as OTEL exporter
    otel.AddPrometheusExporter();
});

// Configure OpenTelemetry tracer with automatic instrumentation and Jeager exporter
// Configure Jeager endpoit via ENV variables
// https://github.com/open-telemetry/opentelemetry-dotnet/blob/main/src/OpenTelemetry.Exporter.Jaeger/README.md
builder.Services.AddOpenTelemetryTracing(tracerProviderBuilder =>
{
    tracerProviderBuilder
    .AddSource("NotesApp")
    .SetResourceBuilder(
        ResourceBuilder.CreateDefault()
            .AddService(serviceName: "NotesApp"))
    .AddAspNetCoreInstrumentation()
    .AddEntityFrameworkCoreInstrumentation()
    .AddJaegerExporter();
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseOpenTelemetryPrometheusScrapingEndpoint();

app.MapHealthChecks("/health");

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<NotesContext>();
    db.Database.Migrate();
}

app.Run();