using NotesAppDotnet.Data;
using NotesAppDotnet.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSystemd();

// Add services to the container.

builder.Services.AddOptions<Settings>()
    .Bind(builder.Configuration.GetSection(Settings.ConfigurationSectionName));

builder.Services.AddControllers();
builder.Services.AddDbContext<NotesContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<NotesContext>();
    dataContext.Database.Migrate();
}
// app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();
