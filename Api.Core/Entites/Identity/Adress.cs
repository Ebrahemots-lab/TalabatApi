namespace Api.Core.Entites.Identity;

public class Adress
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string City { get; set; }

    public string Country { get; set; }

    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
}
