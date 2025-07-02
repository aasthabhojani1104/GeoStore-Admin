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

       
        public IActionResult List()
        {
            var countries = _countryService.GetAllCountries();
            return View(countries);
        }



     
    }
}
