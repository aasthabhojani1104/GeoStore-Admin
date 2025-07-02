using Microsoft.AspNetCore.Mvc;
using Country_Store.Models;
using Country_Store.Service;
using Country_Store.Services.Country;
using Country_State.Services.State;
using Country_Store.Attributes;

namespace Country_Store.Controllers
{
    [PermissionAuthorize("State")]
    public class StateController : BaseController
    {
        private readonly IStateService _stateService;
        private readonly ICountryService _countryService;

        public StateController(IStateService stateService, ICountryService countryService)
        {
            _stateService = stateService;
            _countryService = countryService;
        }

        public IActionResult List()
        {
            var states = _stateService.GetAll();
            return View(states);
        }
        
      

    }
}
