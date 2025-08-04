using Country_Store.Models;
using Country_Store.Services.Country;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using Country_Store.Attributes;

namespace Country_Store.Controllers
{
    [PermissionAuthorize("Country")]
    public class CountryController : BaseController
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }


        public IActionResult List(int page = 1, string search = null)
        {
            int pageSize = 10; // or any default
            var result = _countryService.GetPagedCountries(page, pageSize, search);
            ViewBag.Search = search;
            ViewBag.CurrentPage=page;
            return View(result);
        }






    }
}
