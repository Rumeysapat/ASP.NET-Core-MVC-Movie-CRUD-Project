namespace DynamicData.Entity;

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ImageUrl { get; set; }

    // One-to-One Navigation
    public Person Person { get; set; }
}

public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }
    public string Biography { get; set; }
    public string? Imdb { get; set; }
    public string? HomePage { get; set; }
    public string? PlaceOfBirth { get; set; }
    // Navigation
    public User User { get; set; }
    // Foreign Key (bire bir ilişki burada)
    public int UserId { get; set; }

    // Navigation property: bir kişi birden fazla filmde rol alabilir
    public ICollection<Cast> Casts { get; set; } = new List<Cast>();

    // Navigation property: bir kişi birden fazla filmde ekip üyesi olabilir
    public ICollection<Crew> Crews { get; set; } = new List<Crew>();
}

public class Crew
{
    public int CrewId { get; set; }
    public Movie Movie { get; set; }
    public int MovieId { get; set; }
    public Person Person { get; set; }
    public int PersonId { get; set; }
    public string Job { get; set; }
}

public class Cast
{
    public int CastId { get; set; }
    public Movie Movie { get; set; }
    public int MovieId { get; set; }
    public Person Person { get; set; }
    public int PersonId { get; set; }
    public string Name { get; set; }
    public string Character { get; set; }

}