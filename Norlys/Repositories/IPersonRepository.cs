using Norlys.Domain;

namespace Norlys.Repositories
{
    public interface IPersonRepository
    {
        void CreatePerson(Person person);
        void DeletePerson(int personID);
        Person GetPersonByID(int personID);
        void UpdatePerson(Person person);
    }
}