using Norlys.Repositories;
using Norlys.Domain;

namespace Norlys.Services
{
    public class PeopleService : IPeopleService {
        private readonly IPeopleRepository _peopleRepository;

        public PeopleService(IPeopleRepository peopleRepository) {
            _peopleRepository = peopleRepository;
        }

        public async Task CreatePerson(Person person) {
            await _peopleRepository.CreatePerson(person);
        }

        public async Task<Person> GetPersonByID(int personID) {
            return await _peopleRepository.GetPersonByID(personID);
        }

        public async Task UpdatePerson(Person person) {
            await _peopleRepository.UpdatePerson(person);
        }

        public async Task DeletePerson(int personID) {
            await _peopleRepository.DeletePerson(personID);
        }

        public async Task<List<Person>> GetAllPeople() {
            return await _peopleRepository.GetAllPeople();
        }
    }
}
