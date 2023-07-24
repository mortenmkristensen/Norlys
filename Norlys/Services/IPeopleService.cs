using Norlys.Domain;

namespace Norlys.Services {
    public interface IPeopleService {
        Task CreatePerson(Person person, CancellationToken cancellationToken);
        Task DeletePerson(int personID, CancellationToken cancellationToken);
        Task<List<Person>> GetAllPeople(CancellationToken cancellationToken);
        Task<Person> GetPersonByID(int personID, CancellationToken cancellationToken);
        Task UpdatePerson(Person person, CancellationToken cancellationToken);
    }
}