namespace CinemaApp.Web.ViewModels.Admin.MovieManagement;

public class MovieManagementIndexViewModel
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Genre { get; set; } = null!;

    public string ReleaseDate { get; set; } = null!;

    public string Director { get; set; } = null!;

    public int Duration { get; set; }

    public bool IsDeleted { get; set; } = false;
}