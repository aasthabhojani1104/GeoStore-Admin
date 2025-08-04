using Country_Store.Models;

namespace Country_Store.Services.User
{
    public interface IUserService
    {
        UserModel GetUserByID(int id);
        void AddOrEditUser(UserModel model);
        void DeleteUser(int id);
        List<UserModel> GetAllUsers();
        PagedResult<UserModel> GetPagedUsers(int page, int pageSize, string searchTerm);


    }

}
