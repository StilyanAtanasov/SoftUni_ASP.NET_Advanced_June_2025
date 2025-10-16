using CinemaApp.Core.Common.Utils;
using CinemaApp.Web.ViewModels.Manager.ProgramSetup;

namespace CinemaApp.Services.Core.Manager.Contracts;

public interface IProgramSetupService
{
    Task<ServiceResult<ProgramSetupUpdateViewModel>> GetAllMoviesReadonlyAsync(Guid cinemaId);

    Task<ServiceResult> SaveProgramChanges(ProgramSetupUpdateViewModel model);
}