using System.ComponentModel.DataAnnotations;
using static CinemaApp.Data.Common.EntityConstraints.Cinema;

namespace CinemaApp.Web.ViewModels.Admin.CinemaManagement;

public class AddCinemaFormModel
{
    [Required]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(LocationMaxLength, MinimumLength = LocationMinLength)]
    public string Location { get; set; } = null!;
}

