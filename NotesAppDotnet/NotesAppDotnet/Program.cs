using Microsoft.EntityFrameworkCore;
using NotesAppDotnet.Data;
using NotesAppDotnet.Model;
using OpenTelemetry.Metrics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddOptions<Settings>()
    .Bind(builder.Configuration.GetSection(Settings.ConfigurationSectionName));

builder.Services.AddControllers();
builder.Services.AddDbContext<NotesContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseOpenTelemetryPrometheusScrapingEndpoint();
// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    // app.UseSwagger();
    // app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<NotesContext>();
    db.Database.Migrate();
}

app.Run();