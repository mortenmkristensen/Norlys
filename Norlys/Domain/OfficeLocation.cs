namespace Norlys.Domain
{
    public class OfficeLocation
    {
        public int? OfficeID { get; set; }
        public string LocationName { get; set; }
        public int MaxOccupancy { get; set; }
        public List<Person>? People { get; set; }

        public OfficeLocation()
        {

        }
        public OfficeLocation(int officeID, string locationName, int maxOccupancy, List<Person> people)
        {
            OfficeID = officeID;
            LocationName = locationName;
            MaxOccupancy = maxOccupancy;
            People = people;
        }
    }
}
