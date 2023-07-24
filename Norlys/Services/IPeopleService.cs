using Norlys.Domain;

namespace Norlys.Services {
    public interface IPeopleService {
        Task CreatePerson(Person person);
        Task DeletePerson(int personID);
        Task<List<Person>> GetAllPeople();
        Task<Person> GetPersonByID(int personID);
        Task UpdatePerson(Person person);
    }
}