using Norlys.Domain;

namespace Norlys.Services {
    public interface IOfficeLocationService {
        Task CreateOfficeLocation(OfficeLocation officeLocation, CancellationToken cancellationToken);
        Task DeleteOfficeLocation(int officeLocationID, CancellationToken cancellationToken);
        Task<List<OfficeLocation>> GetAllOfficeLocations(CancellationToken cancellationToken);
        Task<OfficeLocation> GetOfficeLocationByID(int officeLocationID, CancellationToken cancellationToken);
        Task UpdateOfficeLocation(OfficeLocation officeLocation, CancellationToken cancellationToken);
    }
}