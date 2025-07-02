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

     
    }
}
