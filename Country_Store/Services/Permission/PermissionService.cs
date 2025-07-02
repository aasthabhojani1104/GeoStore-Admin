using Country_Store.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

namespace Country_Store.Services.Permission
{
    public class PermissionService : IPermissionService
    {
        private readonly string _connectionString;

        public PermissionService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MyConnection");
        }

        public List<UserModel> GetUsersByRole(string role)
        {
            var users = new List<UserModel>();

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SELECT UserId, Username FROM Users WHERE Role = @Role", conn);
            cmd.Parameters.AddWithValue("@Role", role);

            conn.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                users.Add(new UserModel
                {
                    UserId = (int)reader["UserId"],
                    Username = reader["Username"].ToString()
                });
            }

            return users;
        }


        public void SaveUserPermissions(int userId, List<string> permissions)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SaveUserPermissions", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@AdminAccess", permissions.Contains("Admin"));
                    cmd.Parameters.AddWithValue("@CountryAccess", permissions.Contains("Country"));
                    cmd.Parameters.AddWithValue("@StateAccess", permissions.Contains("State"));
                    cmd.Parameters.AddWithValue("@CityAccess", permissions.Contains("City"));
                    cmd.Parameters.AddWithValue("@StoreAccess", permissions.Contains("Store"));
                    cmd.Parameters.AddWithValue("@UserAccess", permissions.Contains("User"));

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public List<string> GetUserPermissions(int userId)
        {
            var permissions = new List<string>();
            var accessMap = new Dictionary<string, string>
        {
                { "CountryAccess", "Country" },
                { "StateAccess", "State" },
                { "CityAccess", "City" },
                { "StoreAccess", "Store" },
                { "UserAccess", "User" },
                { "AdminAccess", "Admin" }
        };

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM UserPermission WHERE UserId = @UserId", conn))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        foreach (var kvp in accessMap)
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal(kvp.Key)) && reader.GetBoolean(reader.GetOrdinal(kvp.Key)))
                            {
                                permissions.Add(kvp.Value);
                            }
                        }
                    }
                }
            }

            return permissions;
        }


        public bool HasPermission(int userId, string permissionKey)
        {
            string columnName = permissionKey + "Access";
            string query = $"SELECT 1 FROM UserPermission WHERE UserId = @UserId AND {columnName} = 1";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                conn.Open();

                return cmd.ExecuteScalar() != null;
            }
        }




    }
}
