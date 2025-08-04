using Country_Store.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Country_Store.Services
{
    public interface IStoreService
    {
        List<StoreModel> GetAllStores();                  
                       
      PagedResult<StoreModel> GetPagedStores(int page, int pageSize,string searchTearm);       
    
    }
}
