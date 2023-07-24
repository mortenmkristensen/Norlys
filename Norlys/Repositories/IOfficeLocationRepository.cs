using Norlys.Domain;

namespace Norlys.Repositories
{
    public interface IOfficeLocationRepository
    {
        int CreateOfficeLocation(OfficeLocation officeLocation);
        List<OfficeLocation> GetAllOfficeLocations();
        OfficeLocation GetOfficeLocationWithPeople(int officeID);
        void UpdateOfficeLocation(OfficeLocation officeLocation);
    }
}