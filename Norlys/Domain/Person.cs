namespace Norlys.Domain
{
    public class Person
    {
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int OfficeID { get; set; }

        public Person()
        {

        }
        public Person(int personID, string firstName, string lastName, DateTime birthDate, int officeID)
        {
            PersonID = personID;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            OfficeID = officeID;
        }
    }
}
