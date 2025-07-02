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



    }

}

