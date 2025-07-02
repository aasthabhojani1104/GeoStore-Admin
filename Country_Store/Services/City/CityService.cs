using System.Collections.Generic;
using System.Data;
using Country_Store.Models;
using Country_Store.Services.City;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
namespace Country_Store.Services.City
{
    public class CityService : ICityService
    {
        private readonly string _connectionString;

        public CityService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MyConnection");
        }

     

        public List<CityModel> GetAll()
        {
            var list = new List<CityModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[SP_City_GetAll]", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new CityModel
                    {
                        CityId = (int)reader["CityId"],
                        CityName = reader["CityName"].ToString(),
                        PinCode = reader["PinCode"].ToString(),
                        Population = (int)reader["Population"],
                        StateId = (int)reader["StateId"],
                        StateName = reader["StateName"].ToString()
                    });
                }
            }

            return list;
        }

       
    }
}
