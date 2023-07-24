using Norlys.Repositories;
using Norlys.Domain;

namespace Norlys.Services
{
    public class PeopleService : IPeopleService {
        private readonly IPeopleRepository _peopleRepository;

        public PeopleService(IPeopleRepository peopleRepository) {
            _peopleRepository = peopleRepository;
        }

        public async Task CreatePerson(Person person, CancellationToken cancellationToken) {
            await _peopleRepository.CreatePerson(person, cancellationToken);
        }

        public async Task<Person> GetPersonByID(int personID, CancellationToken cancellationToken) {
            return await _peopleRepository.GetPersonByID(personID, cancellationToken);
        }

        public async Task UpdatePerson(Person person, CancellationToken cancellationToken) {
            await _peopleRepository.UpdatePerson(person, cancellationToken);
        }

        public async Task DeletePerson(int personID, CancellationToken cancellationToken) {
            await _peopleRepository.DeletePerson(personID, cancellationToken);
        }

        public async Task<List<Person>> GetAllPeople(CancellationToken cancellationToken) {
            return await _peopleRepository.GetAllPeople(cancellationToken);
        }
    }
}
