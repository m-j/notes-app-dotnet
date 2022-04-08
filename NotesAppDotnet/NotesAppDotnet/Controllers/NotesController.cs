using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NotesAppDotnet.Data;
using NotesAppDotnet.Model;

namespace NotesAppDotnet.Controllers;

[ApiController]
[Route("[controller]")]
public class NotesController : ControllerBase
{
    private readonly IOptions<Settings> _settings;
    private readonly NotesContext _dbContext;

    public NotesController(IOptions<Settings> settings, NotesContext dbContext)
    {
        _settings = settings;
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IEnumerable<Note>> Get()
    {
        Console.WriteLine(_settings.Value.DataPath);

        var notes = await _dbContext.Notes.ToListAsync();

        return notes;
    }
}