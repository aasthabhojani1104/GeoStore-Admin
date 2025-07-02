using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Country_Store.Models
{
    public class CityModel
    {
        public int CityId { get; set; }

        [Required(ErrorMessage = "City name is required")]
        public string CityName { get; set; }

        [Required(ErrorMessage = "Pin code is required")]
        public string PinCode { get; set; }

        [Required(ErrorMessage = "Population is required")]
        public int Population { get; set; }

        [Required(ErrorMessage = "State selection is required")]
        public int StateId { get; set; }

        [Required(ErrorMessage = "Country selection is required")]
        public int CountryId { get; set; }

        public List<SelectListItem>? CountryList { get; set; }
        public List<SelectListItem>? StateList { get; set; }


        public string StateName { get; set; } 
    }

}
