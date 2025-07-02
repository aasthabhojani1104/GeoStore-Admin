using Microsoft.AspNetCore.Mvc;
using Country_Store.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Country_Store.Services.Admin;
namespace Country_Store.Services.User
{


    public class LoginService : ILoginService
    {
        private readonly string _connectionString;

        public LoginService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MyConnection");
        }

        public UserModel ValidateUser(string username, string password)
        {
            UserModel user = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("[ValidateUser]", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel
                        {
                            UserId = (int)reader["UserId"],
                            Username = reader["Username"].ToString(),
                            FullName = reader["FullName"].ToString(),
                            Password = reader["Password"].ToString(),
                            Role = reader["Role"].ToString(),
                            IsActive = Convert.ToBoolean(reader["IsActive"])
                        };
                    }
                }
            }

            return user;
        }

         
        }


    }



