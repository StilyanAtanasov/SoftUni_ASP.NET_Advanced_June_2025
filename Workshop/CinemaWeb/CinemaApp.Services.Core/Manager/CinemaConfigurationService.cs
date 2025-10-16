using CinemaApp.Data.Repository.Contracts;
using CinemaApp.Services.Core.Manager.Contracts;
using CinemaApp.Web.ViewModels.Manager.CinemaConfiguration;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Services.Core.Manager;
public class CinemaConfigurationService : ICinemaConfigurationService
{
    private readonly ICinemaRepository _cinemaRepository;

    public CinemaConfigurationService(ICinemaRepository cinemaRepository)
        => _cinemaRepository = cinemaRepository;

    public async Task<ICollection<ConfigureCinemasViewModel>> GetAllCinemasReadonlyAsync()
        => await _cinemaRepository
            .GetAllReadonly()
            .Select(c => new ConfigureCinemasViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Location = c.Location,  
            })
            .ToArrayAsync();
}