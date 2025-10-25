using System;

public interface IStationaryRepository
{
    List<Education> GetListOfStationaries();

    Task AddStationaryToDataBase(Education education);

    Task<Education> GetAStationaryByIdAsync(int educationId);

    Task EditAStationary(Education education);

    Task DeleteAStationary(Education education);
}
