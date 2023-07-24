using Norlys.Domain;
using Norlys.Repositories;

namespace Norlys.Services
{
    public class OfficeLocationService : IOfficeLocationService {
        private readonly IOfficeLocationRepository _officeLocationRepository;

        public OfficeLocationService(IOfficeLocationRepository officeLocationRepository) {
            this._officeLocationRepository = officeLocationRepository;
        }

        public async Task CreateOfficeLocation(OfficeLocation officeLocation, CancellationToken cancellationToken) {
            await _officeLocationRepository.CreateOfficeLocation(officeLocation, cancellationToken);
        }

        public async Task<OfficeLocation> GetOfficeLocationByID(int officeLocationID, CancellationToken cancellationToken) {
            return await _officeLocationRepository.GetOfficeLocationWithPeople(officeLocationID, cancellationToken);
        }

        public async Task UpdateOfficeLocation(OfficeLocation officeLocation, CancellationToken cancellationToken) {
            await _officeLocationRepository.UpdateOfficeLocation(officeLocation, cancellationToken);
        }

        public async Task DeleteOfficeLocation(int officeLocationID, CancellationToken cancellationToken) {
            await _officeLocationRepository.DeleteOfficeLocation(officeLocationID, cancellationToken);
        }

        public async Task<List<OfficeLocation>> GetAllOfficeLocations(CancellationToken cancellationToken) {
            return await _officeLocationRepository.GetAllOfficeLocations(cancellationToken);
        }
    }
}
