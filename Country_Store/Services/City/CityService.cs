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
        public PagedResult<CityModel> GetPagedCities(int page, int pageSize, string searchTerm = null)
        {
            var result = new PagedResult<CityModel>();
            var items = new List<CityModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PL_CityPaged", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PageNumber", page);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);
                cmd.Parameters.AddWithValue("@SearchTerm", string.IsNullOrEmpty(searchTerm) ? (object)DBNull.Value : searchTerm);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    items.Add(new CityModel
                    {
                        CityId = Convert.ToInt32(reader["CityId"]),
                        CityName = reader["CityName"].ToString(),
                        PinCode = reader["PinCode"].ToString(),
                        Population = Convert.ToInt32(reader["Population"]),
                        StateId = Convert.ToInt32(reader["StateId"]),
                        StateName = reader["StateName"].ToString()
                    });
                }

                int totalCount = 0;
                if (reader.NextResult() && reader.Read())
                {
                    totalCount = Convert.ToInt32(reader["TotalCount"]);
                }

                result.Items = items;
                result.CurrentPage = page;
                result.PageSize = pageSize;
                result.TotalItems = totalCount;
            }

            return result;
        }


    }
}
