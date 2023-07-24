using Norlys.Domain;

namespace Norlys.Services {
    public class PersonValidator 
    {
        private readonly IOfficeLocationService _officeLocationService;

        public PersonValidator(IOfficeLocationService officeLocationService) 
        {
            _officeLocationService = officeLocationService;
        }
        public async Task<bool> Validate(Person person, CancellationToken cancellationToken) 
        {
            var officeLocation = await _officeLocationService.GetOfficeLocationByID(person.OfficeID, cancellationToken);
            if (officeLocation?.People?.Count >= officeLocation?.MaxOccupancy) 
            {
                return false;
            }
            if (person.BirthDate == default || person.BirthDate < DateTime.UtcNow.AddYears(-80) || person.BirthDate > DateTime.UtcNow) 
            {
                return false;
            }
            if (String.IsNullOrWhiteSpace(person.FirstName) || String.IsNullOrWhiteSpace(person.LastName) || person.LastName.Contains(" ")) 
            {
                return false;
            }
            return true;
        }
    }
}
