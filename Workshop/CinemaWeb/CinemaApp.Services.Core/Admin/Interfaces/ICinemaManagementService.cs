using CinemaApp.Core.Common.Utils;
using CinemaApp.Web.ViewModels.Admin.CinemaManagement;

namespace CinemaApp.Services.Core.Admin.Interfaces;

public interface ICinemaManagementService
{
    Task<ICollection<CinemaManagementIndexViewModel>> GetAllCinemasReadonlyAsync();

    Task<ServiceResult> AddCinemaAsync(AddCinemaFormModel model);

    Task<ServiceResult<EditCinemaFormModel>> GetCinemaForEditAsync(Guid cinemaId);

    Task<ServiceResult> EditCinemaAsync(EditCinemaFormModel model);

    Task<ServiceResult> ToggleDeleteAsync(Guid cinemaId);
}
