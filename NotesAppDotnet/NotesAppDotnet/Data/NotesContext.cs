using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NotesAppDotnet.Model;

namespace NotesAppDotnet.Data;

public class NotesContext : DbContext
{
    private readonly string _dbPath;
    public DbSet<Note> Notes { get; set; }

    public NotesContext(IOptions<Settings> settings)
    {
        _dbPath = Path.Join(settings.Value.DataPath, "notes.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={_dbPath}");
    }
}