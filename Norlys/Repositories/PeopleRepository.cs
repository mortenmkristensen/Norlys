using Norlys.Domain;
using System.Data.SqlClient;

namespace Norlys.Repositories
{
    public class PeopleRepository : IPeopleRepository {
        private readonly string connectionString;

        public PeopleRepository(string connectionString) {
            this.connectionString = connectionString;
        }

        public async Task CreatePerson(Person person, CancellationToken cancellationToken) {
            string insertSql = "INSERT INTO People (FirstName, LastName, BirthDate, OfficeID) " +
                              "VALUES (@FirstName, @LastName, @BirthDate, @OfficeID)";

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                using (SqlCommand command = new SqlCommand(insertSql, connection)) {
                    command.Parameters.AddWithValue("@FirstName", person.FirstName);
                    command.Parameters.AddWithValue("@LastName", person.LastName);
                    command.Parameters.AddWithValue("@BirthDate", person.BirthDate);
                    command.Parameters.AddWithValue("@OfficeID", person.OfficeID);

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<Person> GetPersonByID(int personID, CancellationToken cancellationToken) {
            Person person = null;

            string selectSql = "SELECT PersonID, FirstName, LastName, BirthDate, OfficeID " +
                               "FROM People WHERE PersonID = @PersonID";

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                using (SqlCommand command = new SqlCommand(selectSql, connection)) {
                    command.Parameters.AddWithValue("@PersonID", personID);
                    connection.Open();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync()) {
                        if (reader.Read()) {
                            person = new Person {
                                PersonID = Convert.ToInt32(reader["PersonID"]),
                                FirstName = reader["FirstName"].ToString() ?? "",
                                LastName = reader["LastName"].ToString() ?? "",
                                BirthDate = Convert.ToDateTime(reader["BirthDate"]),
                                OfficeID = Convert.ToInt32(reader["OfficeID"])
                            };
                        }
                    }
                }
            }

            return person;
        }

        public async Task UpdatePerson(Person person, CancellationToken cancellationToken) {
            string updateSql = "UPDATE People SET FirstName = @FirstName, LastName = @LastName, " +
                               "BirthDate = @BirthDate, OfficeID = @OfficeID " +
                               "WHERE PersonID = @PersonID";

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                using (SqlCommand command = new SqlCommand(updateSql, connection)) {
                    command.Parameters.AddWithValue("@FirstName", person.FirstName);
                    command.Parameters.AddWithValue("@LastName", person.LastName);
                    command.Parameters.AddWithValue("@BirthDate", person.BirthDate);
                    command.Parameters.AddWithValue("@OfficeID", person.OfficeID);
                    command.Parameters.AddWithValue("@PersonID", person.PersonID);

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeletePerson(int personID, CancellationToken cancellationToken) {
            string deleteSql = "DELETE FROM People WHERE PersonID = @PersonID";

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                using (SqlCommand command = new SqlCommand(deleteSql, connection)) {
                    command.Parameters.AddWithValue("@PersonID", personID);

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task<List<Person>> GetAllPeople(CancellationToken cancellationToken) {
            List<Person> people = new List<Person>();

            string selectSql = "SELECT PersonID, FirstName, LastName, BirthDate, OfficeID FROM People";

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                using (SqlCommand command = new SqlCommand(selectSql, connection)) {
                    connection.Open();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync()) {
                        while (reader.Read()) {
                            Person person = new Person {
                                PersonID = Convert.ToInt32(reader["PersonID"]),
                                FirstName = reader["FirstName"].ToString() ?? "",
                                LastName = reader["LastName"].ToString() ?? "",
                                BirthDate = Convert.ToDateTime(reader["BirthDate"]),
                                OfficeID = Convert.ToInt32(reader["OfficeID"])
                            };

                            people.Add(person);
                        }
                    }
                }
            }

            return people;
        }
    }
}
