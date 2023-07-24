using Norlys.Domain;

namespace Norlys.Services {
    public interface IOfficeLocationService {
        Task CreateOfficeLocation(OfficeLocation officeLocation);
        Task DeleteOfficeLocation(int officeLocationID);
        Task<List<OfficeLocation>> GetAllOfficeLocations();
        Task<OfficeLocation> GetOfficeLocationByID(int officeLocationID);
        Task UpdateOfficeLocation(OfficeLocation officeLocation);
    }
}