using Country_Store.Models;
using Microsoft.AspNetCore.Mvc;

namespace Country_Store.Services.Permission
{
   
        public interface IPermissionService
        {
            List<UserModel> GetUsersByRole(string role);
            List<string> GetUserPermissions(int userId);
 
            void SaveUserPermissions(int userId, List<string> permissions);
            bool HasPermission(int userId, string permissionKey);
        }
   
}
