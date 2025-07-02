using Country_Store.Models;
using Country_Store.Services.Permission;
using Country_Store.Services.User;
using Microsoft.AspNetCore.Mvc;
using Country_Store.Attributes;

namespace Country_Store.Controllers
{
    [PermissionAuthorize("admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;

        public UserController(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;

        }


        #region User
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();

            foreach (var user in users)
            {
                user.AssignedPermissions = _permissionService.GetUserPermissions(user.UserId);
            }

            return PartialView("Partials/_UserList", users);
        }

        [HttpGet]
        public IActionResult GetUserByID(int? id)
        {
            UserModel model;

            if (id.HasValue && id.Value > 0)
            {
                model = _userService.GetUserByID(id.Value);

                var permissions = _permissionService.GetUserPermissions(id.Value);
                model.AdminAccess = permissions.Contains("Admin");
                model.CountryAccess = permissions.Contains("Country");
                model.StateAccess = permissions.Contains("State");
                model.CityAccess = permissions.Contains("City");
                model.StoreAccess = permissions.Contains("Store");
                model.UserAccess = permissions.Contains("User");

                ViewBag.Title = "Edit User";
            }
            else
            {
                model = new UserModel();
                ViewBag.Title = "Add New User";

            }

            return PartialView("~/Views/Admin/Partials/_UserForm.cshtml", model);
        }



        [HttpPost]
        public IActionResult AddOrEditUser(UserModel model)
        {
            if (ModelState.IsValid)
            {
                _userService.AddOrEditUser(model);

                var permissions = new List<string>();
                if (model.AdminAccess) permissions.Add("Admin");
                if (model.CountryAccess) permissions.Add("Country");
                if (model.StateAccess) permissions.Add("State");
                if (model.CityAccess) permissions.Add("City");
                if (model.StoreAccess) permissions.Add("Store");
                if (model.UserAccess) permissions.Add("User");

                _permissionService.SaveUserPermissions(model.UserId, permissions);

                TempData["Message"] = model.UserId > 0
                    ? "✅ User updated with permissions!"
                    : "✅ User created with permissions!";

                return RedirectToAction("Index", "Admin", new { tab = "User" }); 
            }

            return PartialView("~/Views/Admin/Partials/_UserForm.cshtml", model);
        }

        public IActionResult DeleteUser(int id)
        {
            _userService.DeleteUser(id);
            TempData["Message"] = "User deleted successfully!";
            return RedirectToAction("Index","Admin");
        }

        [HttpGet]
        public IActionResult LoadTab(string tab)
        {
            if (tab == "User")
            {
                return PartialView("~/Views/Admin/Partials/_UserForm.cshtml", new UserModel());
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult LoadList(string tab)
        {
            if (tab == "User")
            {
                var users = _userService.GetAllUsers();
                return PartialView("~/Views/Admin/Partials/_UserList.cshtml", users);
            }

            return NotFound();
        }


        #endregion
    }
}
