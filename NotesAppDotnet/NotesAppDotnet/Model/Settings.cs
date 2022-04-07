namespace NotesAppDotnet.Model;

public class Settings
{
    public const string ConfigurationSectionName = "Settings";
    
    public string DataPath { get; set; } = null!;

    public IEnumerable<string> Tags { get; set; } = null!;
}