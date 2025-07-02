using Microsoft.AspNetCore.Mvc;
using Country_Store.Models;
using System.Collections.Generic;

namespace Country_Store.Services.City
{
   
    public interface ICityService
    {
        List<CityModel> GetAll();
       
    }

}
