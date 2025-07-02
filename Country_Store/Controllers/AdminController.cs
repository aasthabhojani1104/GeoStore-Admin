using Country_Store.Models;
using Country_Store.Service;
using Country_Store.Services;
using Country_Store.Services.Admin;
using Country_Store.Services.City;
using Country_Store.Services.Country;
using Country_Store.Services.Permission;
using Country_Store.Services.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using Country_Store.Attributes;




namespace Country_Store.Controllers
{
    [Route("admin")]
   
    public class AdminController : BaseController
    {
        private readonly IAdminService _adminService;
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;

        public AdminController(IAdminService adminService, IUserService userService, IPermissionService permissionService)
        {
            _adminService = adminService;
            _userService = userService;
            _permissionService = permissionService;

        }
        [HttpGet]
        [Route("admin")]
        public IActionResult Index()
        {

            return View();
        }

        private readonly List<string> AdminPortalTabs = new List<string>
{
    "country", "state", "city", "store", "user", "permission"
};

        private bool HasPermission(string tab)
        {
            var permissions = HttpContext.Session.GetString("Permissions");
            if (string.IsNullOrEmpty(permissions)) return false;

            var list = permissions.Split(',');

            if (list.Contains("Admin") && AdminPortalTabs.Contains(tab.ToLower()))
                return true;

            return list.Contains(tab, StringComparer.OrdinalIgnoreCase);
        }

        [HttpGet("loadform/{tab}")]

        public IActionResult LoadTab(string tab)
        {
            try
            {
                if (string.IsNullOrEmpty(tab))
                    return Content("Tab name is null or empty");

                tab = tab.Trim().ToLower();

                if (!HasPermission(tab))
                    return Content("Access denied for: " + tab);


                switch (tab.ToLower())
                {
                    case "country":
                        return PartialView("Partials/_CountryForm", new CountryModel());

                    case "state":
                        ViewBag.CountryList = _adminService.GetAllCountries();
                        return PartialView("Partials/_StateForm", new StateModel());

                    case "city":
                        var states = _adminService.GetAllState();
                        var countries = _adminService.GetAllCountries();
                        ViewBag.CountryList = new SelectList(countries, "CountryId", "CountryName");
                        ViewBag.StateList = new SelectList(states, "StateId", "StateName");
                        return PartialView("Partials/_CityForm", new CityModel());

                    case "store":
                        countries = _adminService.GetAllCountries();
                        states = _adminService.GetAllState();
                        var cities = _adminService.GetAllCity();
                        ViewBag.CountryList = new SelectList(countries, "CountryId", "CountryName");
                        ViewBag.StateList = new SelectList(states, "StateId", "StateName");
                        ViewBag.CityList = new SelectList(cities, "CityId", "CityName");

                        return PartialView("Partials/_StoreForm", new StoreModel
                        {
                            OpeningTime = DateTime.Now.TimeOfDay,
                            ClosingTime = DateTime.Now.AddHours(9).TimeOfDay
                        });
                    case "user":
                        var newUser = new UserModel();
                        return PartialView("Partials/_UserForm", newUser);
                    



                    case "permission":
                        var model = new PermissionModel
                        {
                            UserList = _permissionService
                                .GetUsersByRole("User")
                                .Select(u => new SelectListItem
                                {
                                    Text = u.Username,
                                    Value = u.UserId.ToString()
                                }).ToList()
                        };
                        return PartialView("Partials/_PermissionForm", model);



                    default:
                        return Content("Invalid Tab: " + tab); 
                }

            }
            catch (Exception ex)
            {
                return Content("Error in LoadTab: " + ex.Message);
            }
        }

        [HttpGet("loadList/{tab}")]

        public IActionResult LoadList(string tab)
        {
            if (string.IsNullOrEmpty(tab))
                return Content("Tab name is null or empty");

            tab = tab.Trim().ToLower();

            if (!HasPermission(tab))
                return Content("Access denied for: " + tab);

            switch (tab)
            {
                case "country":
                    var countries = _adminService.GetAllCountries();
                    return PartialView("Partials/_CountryList", countries);

                case "state":
                    var states = _adminService.GetAllState();
                    return PartialView("Partials/_StateList", states);

                case "city":
                    var cities = _adminService.GetAllCity();
                    return PartialView("Partials/_CityList", cities);

                case "store":
                    var stores = _adminService.GetAllStores();
                    return PartialView("Partials/_StoreList", stores);
                case "user":
                    var usersWithPermissions = GetUsersWithPermissions(); // list
                    return PartialView("Partials/_UserList", usersWithPermissions); // ✅ Correct





                default:
                    return Content("Invalid Tab: " + tab); 
            }
        }

        private List<UserModel> GetUsersWithPermissions()
        {
            var users = _userService.GetAllUsers();
            foreach (var user in users)
            {
                user.AssignedPermissions = _permissionService.GetUserPermissions(user.UserId);
            }
            return users;
        }

        #region Country

        [HttpGet]
        [Route("admin/country/get/{id?}")]
        public IActionResult GetCountryByID(int? id)
        {
           
            var country = _adminService.GetCountryById(id.Value);
            return PartialView("Partials/_CountryForm", country);
        }

        [HttpPost]
        [Route("admin/country/add")]
        public IActionResult AddOrUpdateCountry(CountryModel country)
        {
            _adminService.AddOrUpdateCountry(country); 
            TempData["Message"] = country.CountryId == 0 ? "Country added!" : "Country updated!";
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        [Route("admin/country/delete/{id}")]
        public IActionResult DeleteCountry(int id)
        {
            _adminService.DeleteCountry(id);
            TempData["Message"] = "Country deleted successfully.";
            return RedirectToAction("Index");
        }
        #endregion



        #region State
        [HttpGet]
        [Route("admin/state/get/{id?}")]


        public IActionResult GetStateByID(int? id)
        {
            var model = _adminService.GetStateByID(id.GetValueOrDefault()) ?? new StateModel();

            ViewBag.CountryList = _adminService.GetAllCountries();
            return PartialView("Partials/_StateForm", model);
        }



        [HttpPost]
        [Route("admin/state/add")]


        public IActionResult AddOrEditState(StateModel model)
        {
            _adminService.AddOrEditState(model);

            TempData["Message"] = model.StateId > 0 ? "State updated successfully!" : "State added successfully!";

            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        [Route("admin/state/delete/{id}")]

        public IActionResult DeleteState(int id)
        {
           
                _adminService.DeleteState(id);
                TempData["Message"] = "State deleted successfully.";
            
                return RedirectToAction("Index");
        }

        #endregion


        #region City

        [HttpGet]
        [Route("admin/city/get/{id?}")]


        public IActionResult GetIdByCity(int? id)
        {
            CityModel model = _adminService.GetIdByCity(id.Value);

            ViewBag.CountryList = new SelectList(
                _adminService.GetCountriesDropDown(),
                "Value",
                "Text",
                model.CountryId
            );

            ViewBag.StateList = new SelectList(
                _adminService.GetStatesByCountryId(model.CountryId),
                "Value",
                "Text",
                model.StateId
            );

            return PartialView("Partials/_CityForm", model);
        }

        [HttpPost]
        [Route("admin/city/add")]
        public IActionResult AddOrEditCity(CityModel model)
        {
            _adminService.AddOrUpdateCity(model);

            TempData["Message"] = model.CityId > 0 ? "City updated successfully!" : "City added successfully!";

            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        [Route("admin/city/delete/{id}")]

        public IActionResult DeleteCity(int id)
        {
           
                _adminService.DeleteCity(id);
                TempData["Message"] = "City deleted successfully.";
                    
                 return RedirectToAction("Index");
        }

        #endregion



        #region Store

        // GET: /Store/AddOrEdit/5 or /Store/AddOrEdit
        [HttpGet]
        [Route("admin/city/get/{id?}")]

        public IActionResult GetStoreById(int? id)
        {
            var model = _adminService.GetStoreById(id.Value);

            ViewBag.CountryList = new SelectList(_adminService.GetCountriesDropDown(), "Value", "Text", model.CountryId);
            ViewBag.StateList = new SelectList(_adminService.GetStatesByCountryId(model.CountryId), "Value", "Text", model.StateId);
            ViewBag.CityList = new SelectList(_adminService.GetCitiesByStateId(model.StateId), "Value", "Text", model.CityId);

            return PartialView("Partials/_StoreForm", model);
        }


        // POST: /Store/AddOrEdit
        [HttpPost]
        [Route("admin/city/add")]
        public IActionResult AddOrEditStore(StoreModel model)
        {
            bool isEdit = model.StoreId > 0;

         
            _adminService.AddOrUpdateStore(model);
            TempData["Message"] = isEdit ? "Store updated successfully!" : "Store added successfully!";

            
            return RedirectToAction("Index", "Admin");
        }



        // GET: /Store/Delete/5
        [HttpGet]
        [Route("admin/city/delete/{id}")]
        public IActionResult DeleteStore(int id)
        {
            _adminService.DeleteStore(id);
            TempData["Message"] = "Store deleted successfully!";
            return RedirectToAction("Index","Admin");
        }
        #endregion
  
    }
}
