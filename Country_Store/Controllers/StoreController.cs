using Country_Store.Models;
using Country_Store.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Country_Store.Attributes;

namespace Country_Store.Controllers
{
    [PermissionAuthorize("Store")]
    public class StoreController : BaseController
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        // GET: /Store/List
        public IActionResult List(int page = 1,string search="")
        {
            int pageSize = 10;
            var result = _storeService.GetPagedStores(page, pageSize,search);
            ViewBag.Search = search;
            return View(result);
        }

       
    }
}
