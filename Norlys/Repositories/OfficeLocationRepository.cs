using Norlys.Domain;
using System;
using System.Data.SqlClient;

namespace Norlys.Repositories
{
    public class OfficeLocationRepository : IOfficeLocationRepository {
        private readonly string connectionString;

        public OfficeLocationRepository(string connectionString) {
            this.connectionString = connectionString;
        }
        public async Task<int> CreateOfficeLocation(OfficeLocation officeLocation) {
            int id = 0;
            string insertSql = "INSERT INTO OfficeLocations (LocationName, MaxOccupancy) " +
                                "VALUES (@LocationName, @MaxOccupancy)";

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                using (SqlCommand command = new SqlCommand(insertSql, connection)) {
                    command.Parameters.AddWithValue("@LocationName", officeLocation.LocationName);
                    command.Parameters.AddWithValue("@MaxOccupancy", officeLocation.MaxOccupancy);

                    connection.Open();
                    id = await command.ExecuteNonQueryAsync();
                }
            }
            return id;
        }
        public async Task<List<OfficeLocation>> GetAllOfficeLocations() {
            List<OfficeLocation> officeLocations = new List<OfficeLocation>();

            string selectSql = "SELECT OfficeID, LocationName, MaxOccupancy FROM OfficeLocations";

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                using (SqlCommand command = new SqlCommand(selectSql, connection)) {
                    connection.Open();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync()) {
                        while (reader.Read()) {
                            OfficeLocation officeLocation = new OfficeLocation {
                                OfficeID = Convert.ToInt32(reader["OfficeID"]),
                                LocationName = reader["LocationName"].ToString() ?? "",
                                MaxOccupancy = Convert.ToInt32(reader["MaxOccupancy"]),
                                People = await GetPeopleForOfficeLocation(Convert.ToInt32(reader["OfficeID"]))
                            };

                            officeLocations.Add(officeLocation);
                        }
                    }
                }
            }

            return officeLocations;
        }
        public async Task<OfficeLocation> GetOfficeLocationWithPeople(int officeID) {
            OfficeLocation officeLocation = null;

            string selectSql = "SELECT OfficeID, LocationName, MaxOccupancy FROM OfficeLocations WHERE OfficeID = @OfficeID";

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                using (SqlCommand command = new SqlCommand(selectSql, connection)) {
                    command.Parameters.AddWithValue("@OfficeID", officeID);
                    connection.Open();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync()) {
                        if (reader.Read()) {
                            officeLocation = new OfficeLocation {
                                OfficeID = Convert.ToInt32(reader["OfficeID"]),
                                LocationName = reader["LocationName"].ToString() ?? "",
                                MaxOccupancy = Convert.ToInt32(reader["MaxOccupancy"]),
                                People = await GetPeopleForOfficeLocation(officeID)
                            };
                        }
                    }
                }
            }

            return officeLocation;
        }

        public async Task UpdateOfficeLocation(OfficeLocation officeLocation) {
            string updateSql = "UPDATE OfficeLocations SET LocationName = @LocationName, MaxOccupancy = @MaxOccupancy " +
                               "WHERE OfficeID = @OfficeID";

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                using (SqlCommand command = new SqlCommand(updateSql, connection)) {
                    command.Parameters.AddWithValue("@LocationName", officeLocation.LocationName);
                    command.Parameters.AddWithValue("@MaxOccupancy", officeLocation.MaxOccupancy);
                    command.Parameters.AddWithValue("@OfficeID", officeLocation.OfficeID);

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task DeleteOfficeLocation(int officeLocationID) {
            string deleteSql = "DELETE FROM OfficeLocations WHERE OfficeID = @OfficeID";

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                using (SqlCommand command = new SqlCommand(deleteSql, connection)) {
                    command.Parameters.AddWithValue("@OfficeID", officeLocationID);

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        private async Task<List<Person>> GetPeopleForOfficeLocation(int officeID) {
            List<Person> people = new List<Person>();

            string selectSql = "SELECT PersonID, FirstName, LastName, BirthDate FROM People WHERE OfficeID = @OfficeID";

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                using (SqlCommand command = new SqlCommand(selectSql, connection)) {
                    command.Parameters.AddWithValue("@OfficeID", officeID);
                    connection.Open();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync()) {
                        while (reader.Read()) {
                            Person person = new Person {
                                PersonID = Convert.ToInt32(reader["PersonID"]),
                                FirstName = reader["FirstName"].ToString() ?? "",
                                LastName = reader["LastName"].ToString() ?? "",
                                BirthDate = Convert.ToDateTime(reader["BirthDate"]),
                                OfficeID = officeID
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
