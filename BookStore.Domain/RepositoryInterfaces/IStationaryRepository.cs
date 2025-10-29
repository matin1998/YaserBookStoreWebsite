using BookStore.Domain.Entities;

namespace BookStore.Domain.RepositoryInterfaces;
public interface IStationaryRepository
{
    List<Stationary> GetListOfStationaries();

    Task AddStationaryToDataBase(Stationary stationary);

    Task<Stationary> GetAStationaryByIdAsync(int stationaryId);

    Task EditAStationary(Stationary stationary);

    Task DeleteAStationary(Stationary stationary);
}
