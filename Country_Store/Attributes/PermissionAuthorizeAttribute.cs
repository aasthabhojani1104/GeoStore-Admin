using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace Country_Store.Attributes // Use your project namespace
{
    public class PermissionAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _requiredPermission;

        public PermissionAuthorizeAttribute(string requiredPermission)
        {
            _requiredPermission = requiredPermission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var session = context.HttpContext.Session;
            var permissionStr = session.GetString("Permissions");

            if (string.IsNullOrEmpty(permissionStr) ||
                !permissionStr.Split(',').Contains(_requiredPermission, StringComparer.OrdinalIgnoreCase))
            {
                // Redirect if user doesn't have permission
                context.Result = new RedirectToActionResult("AccessDenied", "Home", null);
            }
        }
    }
}
