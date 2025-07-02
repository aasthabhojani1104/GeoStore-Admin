using System.ComponentModel.DataAnnotations;

namespace Country_Store.Models
{
    public class StateModel
    {
        public int StateId { get; set; }

        [Required(ErrorMessage = "State name is required")]
        public string StateName { get; set; }

        [Required(ErrorMessage = "Capital is required")]
        public string Capital { get; set; }

        [Required(ErrorMessage = "Language is required")]
        public string Language { get; set; }

        [Required(ErrorMessage = "Please select a country")]
        public int? CountryId { get; set; }

        public string? CountryName { get; set; }
    }
}
