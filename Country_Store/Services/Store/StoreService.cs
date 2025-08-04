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

        public PagedResult<StoreModel> GetPagedStores(int page, int pageSize, string search)
        {
            var result = new PagedResult<StoreModel>();
            var items = new List<StoreModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PL_StorePaged", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PageNumber", page);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);

                // ✅ Add the missing SearchTerm parameter
                cmd.Parameters.AddWithValue("@SearchTerm", string.IsNullOrEmpty(search) ? DBNull.Value : search);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                // First result: store list
                while (reader.Read())
                {
                    items.Add(new StoreModel
                    {
                        StoreId = Convert.ToInt32(reader["StoreId"]),
                        StoreName = reader["StoreName"].ToString(),
                        OpeningTime = TimeSpan.Parse(reader["OpeningTime"].ToString()),
                        ClosingTime = TimeSpan.Parse(reader["ClosingTime"].ToString()),
                        CountryName = reader["CountryName"].ToString(),
                        StateName = reader["StateName"].ToString(),
                        CityName = reader["CityName"].ToString(),
                        Address = reader["Address"].ToString() // ✅ Include if available
                    });
                }

                // Second result: total count
                if (reader.NextResult() && reader.Read())
                {
                    result.TotalItems = Convert.ToInt32(reader["TotalCount"]);
                }

                result.Items = items;
                result.CurrentPage = page;
                result.PageSize = pageSize;
            }

            return result;
        }










    }
}
