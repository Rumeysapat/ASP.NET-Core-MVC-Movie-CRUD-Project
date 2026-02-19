using DynamicData.Entity;

public class GenreCreateViewModel
{
    public string Name { get; set; }

    public List<Movie> Movies { get; set; } = new();

    public List<int> SelectedMovieIds { get; set; } = new();
}
