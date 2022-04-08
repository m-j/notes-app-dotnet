namespace NotesAppDotnet.Model;

public record Note(int? Id, string Content, DateTime CreateDate, string Tag);