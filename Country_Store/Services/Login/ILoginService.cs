using Country_Store.Models;
using Microsoft.AspNetCore.Mvc;

namespace Country_Store.Services.User
{
    public interface ILoginService
    {
        UserModel ValidateUser(string username, string password);

      

    }

}
