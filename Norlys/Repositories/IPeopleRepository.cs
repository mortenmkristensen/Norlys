using Norlys.Domain;

namespace Norlys.Repositories {
    public interface IPeopleRepository {
        Task CreatePerson(Person person);
        Task DeletePerson(int personID);
        Task<List<Person>> GetAllPeople();
        Task<Person> GetPersonByID(int personID);
        Task UpdatePerson(Person person);
    }
}