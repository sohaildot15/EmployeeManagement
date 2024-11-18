using EmployeeManagement.DAL.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EmployeeManagement.DAL.Repositories
{
    public class EmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Employees", con);
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            EmployeeCode = (int)reader["EmployeeCode"],
                            VendorCode = (int)reader["VendorCode"],
                            EmployeeName = reader["EmployeeName"].ToString(),
                            Email = reader["Email"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            IsActive = (bool)reader["IsActive"],
                            WorkMode = reader["WorkMode"].ToString(),
                            StateID = (int)reader["StateID"],
                            CityID = (int)reader["CityID"]
                        });
                    }
                }
            }

            return employees;
        }

        public void AddEmployee(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO Employees (VendorCode, EmployeeName, Email, Gender, IsActive, WorkMode, StateID, CityID) 
                         VALUES (@VendorCode, @EmployeeName, @Email, @Gender, @IsActive, @WorkMode, @StateID, @CityID)";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@VendorCode", employee.VendorCode);
                cmd.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@IsActive", employee.IsActive);
                cmd.Parameters.AddWithValue("@WorkMode", employee.WorkMode);
                cmd.Parameters.AddWithValue("@StateID", employee.StateID);
                cmd.Parameters.AddWithValue("@CityID", employee.CityID);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Employee GetEmployeeById(int employeeCode)
        {
            Employee employee = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Employees WHERE EmployeeCode = @EmployeeCode";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);

                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        employee = new Employee
                        {
                            EmployeeCode = (int)reader["EmployeeCode"],
                            VendorCode = (int)reader["VendorCode"],
                            EmployeeName = reader["EmployeeName"].ToString(),
                            Email = reader["Email"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            IsActive = (bool)reader["IsActive"],
                            WorkMode = reader["WorkMode"].ToString(),
                            StateID = (int)reader["StateID"],
                            CityID = (int)reader["CityID"]
                        };
                    }
                }
            }

            return employee;
        }

        public void UpdateEmployee(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE Employees 
                                 SET VendorCode = @VendorCode, EmployeeName = @EmployeeName, Email = @Email, Gender = @Gender,
                                     IsActive = @IsActive, WorkMode = @WorkMode, StateID = @StateID, CityID = @CityID
                                 WHERE EmployeeCode = @EmployeeCode";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@VendorCode", employee.VendorCode);
                cmd.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@IsActive", employee.IsActive);
                cmd.Parameters.AddWithValue("@WorkMode", employee.WorkMode);
                cmd.Parameters.AddWithValue("@StateID", employee.StateID);
                cmd.Parameters.AddWithValue("@CityID", employee.CityID);
                cmd.Parameters.AddWithValue("@EmployeeCode", employee.EmployeeCode);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteEmployee(int employeeCode)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Employees WHERE EmployeeCode = @EmployeeCode";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }


    }
}
