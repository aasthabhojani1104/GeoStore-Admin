using Microsoft.AspNetCore.Mvc.Rendering;

namespace Country_Store.Models
{
    public class PermissionModel
    {
      
        public int SelectedUserId { get; set; }

        public List<SelectListItem> UserList { get; set; }

 

        public bool AdminAccess { get; set; }

        public bool CountryAccess { get; set; }
        public bool StateAccess { get; set; }
        public bool CityAccess { get; set; }
        public bool StoreAccess { get; set; }
        public bool UserAccess { get; set; }

        public PermissionModel()
        {
            UserList = new List<SelectListItem>();
        }
    }
}
