using Country_Store.Models;
using Country_Store.Services.Permission;
using Country_Store.Services.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Country_Store.Attributes;

namespace Country_Store.Controllers
{
    [PermissionAuthorize("admin")]

    public class PermissionController : Controller
    {
        private readonly IPermissionService _permissionService;
        private readonly IUserService _userService;


        public PermissionController(IPermissionService permissionService, IUserService userService)
        {
            _permissionService = permissionService;
             _userService = userService;
        }
        // [GET] Load permission form for selected user (via AJAX)
        // - Loads user list (only "User" role)
        // - Pre-fills permissions if userId is provided
        [HttpGet]
        public IActionResult Manage(int? userId)
        {
            try
            {
                var model = new PermissionModel
                {
                    SelectedUserId = userId ?? 0,
                    UserList = _permissionService
                        .GetUsersByRole("User")
                        .ConvertAll(u => new SelectListItem
                        {
                            Text = u.Username,
                            Value = u.UserId.ToString(),
                            Selected = userId.HasValue && u.UserId == userId.Value
                        })
                };

                if (userId.HasValue)
                {
                    var permissions = _permissionService.GetUserPermissions(userId.Value);
                    model.AdminAccess = permissions.Contains("Admin");
                    model.CountryAccess = permissions.Contains("Country");
                    model.StateAccess = permissions.Contains("State");
                    model.CityAccess = permissions.Contains("City");
                    model.StoreAccess = permissions.Contains("Store");
                    model.UserAccess = permissions.Contains("User");
                }

                return PartialView("~/Views/Admin/Partials/_PermissionCheckbox.cshtml", model);

            }
            catch (Exception ex)
            {
                return Content("❌ ERROR in Manage: " + ex.Message);
            }
        }

        //  [GET] Get checkbox-only permission form for a user (used for dynamic AJAX reload)

        [HttpGet]
        public IActionResult GetPermissionForm(int userId)
        {
            try
            {
                var permissions = _permissionService.GetUserPermissions(userId); 
                permissions ??= new List<string>(); 
                var model = new PermissionModel
                {
                    SelectedUserId = userId,
                    AdminAccess = permissions.Contains("Admin"),
                    CountryAccess = permissions.Contains("Country"),
                    StateAccess = permissions.Contains("State"),
                    CityAccess = permissions.Contains("City"),
                    StoreAccess = permissions.Contains("Store"),
                    UserAccess = permissions.Contains("User")
                };

                return PartialView("~/Views/Admin/Partials/_PermissionCheckbox.cshtml", model);
            }
            catch (Exception ex)
            {
                return Content("❌ ERROR: " + ex.Message);
            }
        }


        //  [POST] Save selected permissions for a user
        // - Collects selected checkboxes into a list
        // - Saves via service
        [HttpPost]
        public IActionResult Manage(PermissionModel model)
        {
            var permissions = new List<string>();

            if (model.AdminAccess) permissions.Add("Admin");
            if (model.CountryAccess) permissions.Add("Country");
            if (model.StateAccess) permissions.Add("State");
            if (model.CityAccess) permissions.Add("City");
            if (model.StoreAccess) permissions.Add("Store");
            if (model.UserAccess) permissions.Add("User");

            _permissionService.SaveUserPermissions(model.SelectedUserId, permissions);

            TempData["Message"] = "✅ Permissions updated successfully for the selected user.";
            TempData["ResetTab"] = "Permission";

            return RedirectToAction("Index", "Admin", new { tab = "Permission" });
        }


        //  [GET] Load user + permission form
        // - Used when both dropdown and checkboxes need to be loaded
        [HttpGet]
        public IActionResult LoadPermissions(int userId)
        {
            var permissions = _permissionService.GetUserPermissions(userId);

            var model = new PermissionModel
            {
                SelectedUserId = userId,
                UserList = _userService.GetAllUsers()  
                    .Where(u => u.Role == "User" && u.IsActive) 
                    .Select(u => new SelectListItem
                    {
                        Text = u.Username,
                        Value = u.UserId.ToString()
                    }).ToList(),

                AdminAccess = permissions.Contains("Admin"),
                CountryAccess = permissions.Contains("Country"),
                StateAccess = permissions.Contains("State"),
                CityAccess = permissions.Contains("City"),
                StoreAccess = permissions.Contains("Store"),
                UserAccess = permissions.Contains("User")
            };

            return PartialView("~/Views/Admin/Partials/_PermissionForm.cshtml", model);
        }



    }
}
