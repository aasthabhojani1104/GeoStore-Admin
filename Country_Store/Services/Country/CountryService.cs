using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Country_Store.Models;
using Country_Store.Services.Country;
using System.Collections.Generic;
using System;

namespace Country_Store.Services.Country
{
    public class CountryService : ICountryService
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public CountryService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MyConnection");
        }




        public List<CountryModel> GetAllCountries()
        {
            List<CountryModel> countries = new List<CountryModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[SP_Country_GetAll]", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    countries.Add(new CountryModel
                    {
                        CountryId = Convert.ToInt32(reader["CountryId"]),
                        CountryName = reader["CountryName"].ToString(),
                        CountryCode = reader["CountryCode"].ToString(),
                        Continent = reader["Continent"].ToString(),
                        CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                    });
                }
            }

            return countries;
        }


        public PagedResult<CountryModel> GetPagedCountries(int page, int pageSize, string searchTerm = null)
        {
            var result = new PagedResult<CountryModel>();
            var items = new List<CountryModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PL_CountryPaged", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PageNumber", page);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);
                cmd.Parameters.AddWithValue("@SearchTerm", (object)searchTerm ?? DBNull.Value);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    items.Add(new CountryModel
                    {
                        CountryId = Convert.ToInt32(reader["CountryId"]),
                        CountryName = reader["CountryName"].ToString(),
                        CountryCode = reader["CountryCode"].ToString(),
                        Continent = reader["Continent"].ToString(),
                        CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
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

