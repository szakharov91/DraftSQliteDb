namespace DraftSQliteDb.WebApi.Model;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public Contact Contact { get; set; } = null!;
}
