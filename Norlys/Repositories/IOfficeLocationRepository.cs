using Norlys.Domain;

namespace Norlys.Repositories {
    public interface IOfficeLocationRepository {
        Task<int> CreateOfficeLocation(OfficeLocation officeLocation);
        Task DeleteOfficeLocation(int officeLocationID);
        Task<List<OfficeLocation>> GetAllOfficeLocations();
        Task<OfficeLocation> GetOfficeLocationWithPeople(int officeID);
        Task UpdateOfficeLocation(OfficeLocation officeLocation);
    }
}