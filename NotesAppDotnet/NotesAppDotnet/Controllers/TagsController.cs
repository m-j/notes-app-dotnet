using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NotesAppDotnet.Model;

namespace NotesAppDotnet.Controllers;

[ApiController]
[Route("[controller]")]
public class TagsController : ControllerBase
{
    private readonly IOptions<Settings> _settings;

    public TagsController(IOptions<Settings> settings)
    {
        _settings = settings;
    }

    [HttpGet]
    public IEnumerable<string> Get()
    {
        return _settings.Value.Tags;
    }
}