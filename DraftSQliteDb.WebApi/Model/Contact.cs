namespace DraftSQliteDb.WebApi.Model;

public class Contact
{
    public Address Address { get; set; } = null!;
    public string? Phone { get; set; }
}
