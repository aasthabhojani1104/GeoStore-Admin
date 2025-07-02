using System;
using System.ComponentModel.DataAnnotations;

namespace Country_Store.Models
{
    public class CountryModel
    {
        public int CountryId { get; set; }


        [Required(ErrorMessage = "Country name is required")]
        public string CountryName { get; set; }



        [Required(ErrorMessage = "Country code is required")]
        public string CountryCode { get; set; }

        [Required(ErrorMessage = "Continent is required")]
        public string Continent { get; set; }

        [Required(ErrorMessage = "Created date is required")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
    }
}
