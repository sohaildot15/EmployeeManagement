using EmployeeManagement.DAL.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EmployeeManagement.DAL.Repositories
{
    public class CityRepository
    {
        private readonly string _connectionString;

        public CityRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<City> GetCitiesByState(int stateId)
        {
            List<City> cities = new List<City>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Cities WHERE StateID = @StateID", con);
                cmd.Parameters.AddWithValue("@StateID", stateId);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cities.Add(new City
                        {
                            CityID = (int)reader["CityID"],
                            CityName = reader["CityName"].ToString(),
                            StateID = (int)reader["StateID"]
                        });
                    }
                }
            }

            return cities;
        }

        public string GetCityNameById(int cityId)
        {
            string cityName = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT CityName FROM Cities WHERE CityID = @CityID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CityID", cityId);

                con.Open();
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    cityName = result.ToString();
                }
            }

            return cityName;
        }

    }
}
