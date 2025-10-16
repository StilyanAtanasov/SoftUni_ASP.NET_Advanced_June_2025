using CinemaApp.Web.ViewModels.Manager.CinemaConfiguration;

namespace CinemaApp.Services.Core.Manager.Contracts;

public interface ICinemaConfigurationService
{
    Task<ICollection<ConfigureCinemasViewModel>> GetAllCinemasReadonlyAsync();
}