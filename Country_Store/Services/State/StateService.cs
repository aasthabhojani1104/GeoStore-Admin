using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Country_Store.Models;
using Microsoft.Extensions.Configuration;
using Country_State.Services.State;

namespace Country_Store.Service
{
    public class StateService : IStateService
    {
        private readonly string _connectionString;

        public StateService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MyConnection");
        }

        public List<StateModel> GetAll()
        {
            List<StateModel> list = new List<StateModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[SP_State_GetAll]", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new StateModel
                    {
                        StateId = (int)reader["StateId"],
                        StateName = reader["StateName"].ToString(),
                        Capital = reader["Capital"].ToString(),
                        Language = reader["Language"].ToString(),
                        CountryId = (int)reader["CountryId"],
                        CountryName = reader["CountryName"].ToString()
                    });
                }
            }

            return list;
        }

        public PagedResult<StateModel> GetPagedStates(int page, int pageSize, string searchTerm)
        {
            var result = new PagedResult<StateModel>();
            var items = new List<StateModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PL_StatePaged", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PageNumber", page);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);
                cmd.Parameters.AddWithValue("@SearchTerm", string.IsNullOrWhiteSpace(searchTerm) ? (object)DBNull.Value : searchTerm.Trim());

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    items.Add(new StateModel
                    {
                        StateId = Convert.ToInt32(reader["StateId"]),
                        StateName = reader["StateName"].ToString(),
                        Capital = reader["Capital"].ToString(),
                        Language = reader["Language"].ToString(),
                        CountryId = Convert.ToInt32(reader["CountryId"]),
                        CountryName = reader["CountryName"].ToString()
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

