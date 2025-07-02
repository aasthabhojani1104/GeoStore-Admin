using Country_Store.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Country_Store.Services
{
    public class StoreService : IStoreService
    {
        private readonly string _connectionString;

        public StoreService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MyConnection");
        }

        public List<StoreModel> GetAllStores()
        {
            var stores = new List<StoreModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("PL_StoreGetAll", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    stores.Add(new StoreModel
                    {
                        StoreId = Convert.ToInt32(rdr["StoreId"]),
                        StoreName = rdr["StoreName"].ToString(),
                        OpeningTime = (TimeSpan)rdr["OpeningTime"],
                        ClosingTime = (TimeSpan)rdr["ClosingTime"],
                        CountryName = rdr["CountryName"].ToString(),
                        StateName = rdr["StateName"].ToString(),
                        CityName = rdr["CityName"].ToString()
                    });
                }
            }

            return stores;
        }

        
        

        

       

        

        
    }
}
