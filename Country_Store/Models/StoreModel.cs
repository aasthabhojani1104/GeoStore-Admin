using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Country_Store.Models
{
    public class StoreModel
    {
        public int StoreId { get; set; }

        [Required(ErrorMessage = "Store Name is required")]
        public string StoreName { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Opening Time is required")]
        public TimeSpan OpeningTime { get; set; }

        [Required(ErrorMessage = "Closing Time is required")]
        public TimeSpan ClosingTime { get; set; }

        [Required(ErrorMessage = "Country selection is required")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "State selection is required")]
        public int StateId { get; set; }

        [Required(ErrorMessage = "City selection is required")]
        public int CityId { get; set; }
        public string Address { get; set; }

        // Display-only fields
        public string? CountryName { get; set; }
        public string? StateName { get; set; }
        public string? CityName { get; set; }

        // Dropdowns for form binding
        public IEnumerable<SelectListItem>? CountryList { get; set; }
        public IEnumerable<SelectListItem>? StateList { get; set; }
        public IEnumerable<SelectListItem>? CityList { get; set; }
    }
}
