using Norlys.Domain;
using Norlys.Repositories;

namespace Norlys.Services
{
    public class OfficeLocationService : IOfficeLocationService {
        private readonly IOfficeLocationRepository _officeLocationRepository;

        public OfficeLocationService(IOfficeLocationRepository officeLocationRepository) {
            this._officeLocationRepository = officeLocationRepository;
        }

        public async Task CreateOfficeLocation(OfficeLocation officeLocation) {
            await _officeLocationRepository.CreateOfficeLocation(officeLocation);
        }

        public async Task<OfficeLocation> GetOfficeLocationByID(int officeLocationID) {
            return await _officeLocationRepository.GetOfficeLocationWithPeople(officeLocationID);
        }

        public async Task UpdateOfficeLocation(OfficeLocation officeLocation) {
            await _officeLocationRepository.UpdateOfficeLocation(officeLocation);
        }

        public async Task DeleteOfficeLocation(int officeLocationID) {
            await _officeLocationRepository.DeleteOfficeLocation(officeLocationID);
        }

        public async Task<List<OfficeLocation>> GetAllOfficeLocations() {
            return await _officeLocationRepository.GetAllOfficeLocations();
        }
    }
}
