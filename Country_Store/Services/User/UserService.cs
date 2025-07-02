using Country_Store.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Country_Store.Services.User
{
    public class UserService:IUserService
    {
        private readonly string _connectionString;

        public UserService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MyConnection");
        }
        public List<UserModel> GetAllUsers()
        {
            var users = new List<UserModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllUsers", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            users.Add(new UserModel
                            {
                                UserId = Convert.ToInt32(rdr["UserId"]),
                                Username = rdr["Username"].ToString(),
                                Password = rdr["Password"].ToString(), // Consider omitting in list
                                FullName = rdr["FullName"].ToString(),
                                Role = rdr["Role"].ToString(),
                                IsActive = Convert.ToBoolean(rdr["IsActive"])
                            });
                        }
                    }
                }
            }

            return users;
        }

        public UserModel GetUserByID(int id)
        {
            UserModel user = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_GetUserByID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = new UserModel
                    {
                        UserId = (int)reader["UserId"],
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        FullName = reader["FullName"].ToString(),
                        Role = reader["Role"].ToString(),
                        IsActive = (bool)reader["IsActive"]
                    };
                }

                reader.Close(); // 💡 Important: close before second command

                if (user != null)
                {
                    // Fetch permissions from UserPermissions table
                    using (SqlCommand permCmd = new SqlCommand("sp_GetUserPermission", conn))
                    {
                        permCmd.CommandType = CommandType.StoredProcedure;
                        permCmd.Parameters.AddWithValue("@UserId", id);

                        SqlDataReader permReader = permCmd.ExecuteReader();
                        if (permReader.Read())
                        {
                            user.AdminAccess = Convert.ToBoolean(permReader["AdminAccess"]);
                            user.CountryAccess = Convert.ToBoolean(permReader["CountryAccess"]);
                            user.StateAccess = Convert.ToBoolean(permReader["StateAccess"]);
                            user.CityAccess = Convert.ToBoolean(permReader["CityAccess"]);
                            user.StoreAccess = Convert.ToBoolean(permReader["StoreAccess"]);
                            user.UserAccess = Convert.ToBoolean(permReader["UserAccess"]);
                        }

                        permReader.Close();
                    }
                }
            }

            return user;
        }


        public void AddOrEditUser(UserModel model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_AddOrEditUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Define @UserId as InputOutput
                var userIdParam = new SqlParameter("@UserId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.InputOutput,
                    Value = model.UserId
                };
                cmd.Parameters.Add(userIdParam);

                cmd.Parameters.AddWithValue("@Username", model.Username);
                cmd.Parameters.AddWithValue("@Password", model.Password);
                cmd.Parameters.AddWithValue("@FullName", model.FullName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Role", model.Role ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@IsActive", model.IsActive);

                conn.Open();
                cmd.ExecuteNonQuery();

                // Retrieve the inserted UserId (for new users)
                model.UserId = Convert.ToInt32(userIdParam.Value);
            }
        }



        public void DeleteUser(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_DeleteUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
