using EmployeeManagement.DAL.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EmployeeManagement.DAL.Repositories
{
    public class StateRepository
    {
        private readonly string _connectionString;

        public StateRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<State> GetAllStates()
        {
            List<State> states = new List<State>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM States", con);
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        states.Add(new State
                        {
                            StateID = (int)reader["StateID"],
                            StateName = reader["StateName"].ToString()
                        });
                    }
                }
            }

            return states;
        }

        public string GetStateNameById(int stateId)
        {
            string stateName = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT StateName FROM States WHERE StateID = @StateID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@StateID", stateId);

                con.Open();
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    stateName = result.ToString();
                }
            }

            return stateName;
        }
    }
}
