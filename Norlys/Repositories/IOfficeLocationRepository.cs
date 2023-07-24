using Norlys.Domain;

namespace Norlys.Repositories {
    public interface IOfficeLocationRepository {
        Task<int> CreateOfficeLocation(OfficeLocation officeLocation, CancellationToken cancellationToken);
        Task DeleteOfficeLocation(int officeLocationID, CancellationToken cancellationToken);
        Task<List<OfficeLocation>> GetAllOfficeLocations(CancellationToken cancellationToken);
        Task<OfficeLocation> GetOfficeLocationWithPeople(int officeID, CancellationToken cancellationToken);
        Task UpdateOfficeLocation(OfficeLocation officeLocation, CancellationToken cancellationToken);
    }
}