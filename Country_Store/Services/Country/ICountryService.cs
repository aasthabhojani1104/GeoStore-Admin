using Country_Store.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// ICountryService.cs
namespace Country_Store.Services.Country
{
    public interface ICountryService
    {
        
        List<CountryModel> GetAllCountries();
        
    }

}
