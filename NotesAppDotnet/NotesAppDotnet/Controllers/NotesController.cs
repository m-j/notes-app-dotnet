using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NotesAppDotnet.Model;

namespace NotesAppDotnet.Controllers;

[ApiController]
[Route("[controller]")]
public class NotesController : ControllerBase
{
    private readonly IOptions<Settings> _settings;

    public NotesController(IOptions<Settings> settings)
    {
        _settings = settings;
    }

    [HttpGet]
    public IEnumerable<Note> Get()
    {
        Console.WriteLine(_settings.Value.DataPath);
        
        return new[]
        {
            new Note(1, "Test 1", DateTime.Now),
            new Note(2, "Test 2", DateTime.Now)
        };
    }
}