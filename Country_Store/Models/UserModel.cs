using System.ComponentModel.DataAnnotations;

namespace Country_Store.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }
        public List<string> AssignedPermissions { get; set; } = new List<string>();


        public bool IsActive { get; set; } = true;
        public bool AdminAccess { get; set; }
        public bool CountryAccess { get; set; }
        public bool StateAccess { get; set; }
        public bool CityAccess { get; set; }
        public bool StoreAccess { get; set; }
        public bool UserAccess { get; set; }

    }
}
