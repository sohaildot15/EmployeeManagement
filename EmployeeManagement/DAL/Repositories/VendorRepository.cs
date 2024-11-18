using EmployeeManagement.DAL.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EmployeeManagement.DAL.Repositories
{
    public class VendorRepository
    {
        private readonly string _connectionString;

        public VendorRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Vendor> GetAllVendors()
        {
            List<Vendor> vendors = new List<Vendor>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Vendors", con);
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        vendors.Add(new Vendor
                        {
                            VendorCode = (int)reader["VendorCode"],
                            VendorName = reader["VendorName"].ToString()
                        });
                    }
                }
            }

            return vendors;
        }
    }
}
