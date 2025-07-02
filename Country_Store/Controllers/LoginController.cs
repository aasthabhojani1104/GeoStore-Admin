using Country_Store.Models;
using Country_Store.Services.Permission;
using Country_Store.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public class LoginController : Controller
{
    private readonly ILoginService _userService;
    private readonly IPermissionService _permissionService;

    public LoginController(ILoginService userService, IPermissionService permissionService)
    {
        _userService = userService;
        _permissionService = permissionService;

    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Login(LoginModel model)
    {
        var user = _userService.ValidateUser(model.Username, model.Password);

        if (user != null)
        {
            if (!user.IsActive)
            {
                TempData["ErrorMessage"] = "🚫 Your account is inactive. Please contact the administrator.";
                return View("Login", model);
            }
            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("UserRole", user.Role);

            var permissions = _permissionService.GetUserPermissions(user.UserId);
            HttpContext.Session.SetString("Permissions", string.Join(",", permissions));

            if (permissions.Count == 0)
            {
                TempData["ErrorMessage"] = "🔒 \"You're logged in, but it seems all access points are locked. Talk to your administrator to unlock features.\".";
                return View("Login", model);
            }

            string firstPermission = permissions.FirstOrDefault();
            if (!string.IsNullOrEmpty(firstPermission))
            {
                return RedirectToAction("List", firstPermission); 
            }

            TempData["ErrorMessage"] = "⚠ Unexpected error. Please try again.";
            return View("Login", model);
        }

        TempData["ErrorMessage"] = "Invalid username or password";
        return View("Login", model);
    }






    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        Response.Cookies.Delete("Username");
        return RedirectToAction("Login");
    }

   
}
