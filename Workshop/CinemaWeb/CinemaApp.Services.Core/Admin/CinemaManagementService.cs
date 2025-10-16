using CinemaApp.Core.Common.Utils;
using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using CinemaApp.Services.Core.Admin.Interfaces;
using CinemaApp.Web.ViewModels.Admin.CinemaManagement;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Services.Core.Admin;

public class CinemaManagementService : ICinemaManagementService
{
    private readonly ICinemaRepository _cinemaRepository;

    public CinemaManagementService(ICinemaRepository cinemaRepository)
        => _cinemaRepository = cinemaRepository;

    public async Task<ServiceResult> AddCinemaAsync(AddCinemaFormModel model)
    {
        Cinema cinema = new()
        {
            Name = model.Name,
            Location = model.Location
        };

        _cinemaRepository.Add(cinema);
        await _cinemaRepository.SaveChangesAsync();

        return ServiceResult.Ok();
    }

    public async Task<ServiceResult> EditCinemaAsync(EditCinemaFormModel model)
    {
        Cinema? cinema = await _cinemaRepository.FindByConditionsAsync(c => c.Id == model.Id);
        if (cinema is null) return ServiceResult.NotFound();

        cinema.Name = model.Name;
        cinema.Location = model.Location;

        await _cinemaRepository.SaveChangesAsync();

        return ServiceResult.Ok();
    }

    public async Task<ICollection<CinemaManagementIndexViewModel>> GetAllCinemasReadonlyAsync()
    => await _cinemaRepository
        .GetAllReadonly()
        .Select(c => new CinemaManagementIndexViewModel()
        {
            Id = c.Id,
            Name = c.Name,
            Location = c.Location,
            IsDeleted = c.IsDeleted
        })
        .ToArrayAsync();

    public async Task<ServiceResult<EditCinemaFormModel>> GetCinemaForEditAsync(Guid cinemaId)
    {
        Cinema? cinema = await _cinemaRepository.GetByIdAsync(cinemaId);
        if (cinema is null) return ServiceResult<EditCinemaFormModel>.NotFound();

        EditCinemaFormModel model = new()
        {
            Id = cinema.Id,
            Name = cinema.Name,
            Location = cinema.Location
        };

        return ServiceResult<EditCinemaFormModel>.Ok(model);
    }

    public async Task<ServiceResult> ToggleDeleteAsync(Guid cinemaId)
    {
        Cinema? cinema = await _cinemaRepository.GetByIdAsync(cinemaId);
        if (cinema is null) return ServiceResult.NotFound();

        cinema.IsDeleted = !cinema.IsDeleted;
        await _cinemaRepository.SaveChangesAsync();

        return ServiceResult.Ok();
    }
}