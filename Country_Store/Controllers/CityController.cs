using Country_State.Services.State;
using Country_Store.Models;
using Country_Store.Services.City;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Country_Store.Attributes;

namespace Country_Store.Controllers
{
    [PermissionAuthorize("City")]
    public class CityController : BaseController
    {
        private readonly ICityService _cityService;
        private readonly IStateService _stateService;

        public CityController(ICityService cityService, IStateService stateService)
        {
            _cityService = cityService;
            _stateService = stateService;
        }

        public IActionResult List(int page = 1, string search = null)
        {
            int pageSize = 10;
            var data = _cityService.GetPagedCities(page, pageSize, search);
            ViewBag.Search = search;
            return View(data);
        }



    }

}
