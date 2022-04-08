using NotesAppDotnet.Data;
using NotesAppDotnet.Model;

var builder = WebApplication.CreateBuilder(args);



// builder.Host.ConfigureAppConfiguration((context, configurationBuilder) =>
// {
//     var configurationRoot = configurationBuilder.Build();
//     configurationRoot.Bind();
// }).Build();

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

// app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();