namespace CinemaApp.Web.ViewModels.Admin.CinemaManagement;

public class CinemaManagementIndexViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public bool IsDeleted { get; set; }
}
