using Norlys.Domain;
using System.Data.SqlClient;

namespace Norlys.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly string connectionString;

        public PersonRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void CreatePerson(Person person)
        {
            string insertSql = "INSERT INTO People (FirstName, LastName, BirthDate, OfficeID) " +
                              "VALUES (@FirstName, @LastName, @BirthDate, @OfficeID)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", person.FirstName);
                    command.Parameters.AddWithValue("@LastName", person.LastName);
                    command.Parameters.AddWithValue("@BirthDate", person.BirthDate);
                    command.Parameters.AddWithValue("@OfficeID", person.OfficeID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public Person GetPersonByID(int personID)
        {
            Person person = null;

            string selectSql = "SELECT PersonID, FirstName, LastName, BirthDate, OfficeID " +
                               "FROM People WHERE PersonID = @PersonID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectSql, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", personID);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            person = new Person {
                                PersonID = Convert.ToInt32(reader["PersonID"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                BirthDate = Convert.ToDateTime(reader["BirthDate"]),
                                OfficeID = Convert.ToInt32(reader["OfficeID"])
                            };
                        }
                    }
                }
            }

            return person;
        }

        public void UpdatePerson(Person person)
        {
            string updateSql = "UPDATE People SET FirstName = @FirstName, LastName = @LastName, " +
                               "BirthDate = @BirthDate, OfficeID = @OfficeID " +
                               "WHERE PersonID = @PersonID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(updateSql, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", person.FirstName);
                    command.Parameters.AddWithValue("@LastName", person.LastName);
                    command.Parameters.AddWithValue("@BirthDate", person.BirthDate);
                    command.Parameters.AddWithValue("@OfficeID", person.OfficeID);
                    command.Parameters.AddWithValue("@PersonID", person.PersonID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeletePerson(int personID)
        {
            string deleteSql = "DELETE FROM People WHERE PersonID = @PersonID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(deleteSql, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", personID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
