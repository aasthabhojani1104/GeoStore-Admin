using Country_Store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Country_Store.Services.Admin
{
    public interface IAdminService
    {
        #region Country

        // In IAdminService.cs
        List<CountryModel> GetAllCountries();         
        List<SelectListItem> GetCountriesDropDown();           // for dropdowns
        CountryModel GetCountryById(int id);
        void AddOrUpdateCountry(CountryModel model);
        void DeleteCountry(int id);

        #endregion

        #region State
        //State

        List<StateModel> GetAllState();
        StateModel GetStateByID(int id);
        void AddOrEditState(StateModel state);
        void DeleteState(int id);
        #endregion 

        #region City
        //City
        List<CityModel> GetAllCity();
        CityModel GetIdByCity(int id);
        void AddOrUpdateCity(CityModel city);
        void DeleteCity(int id);
        #endregion

        #region Store

        // Store 
        List<StoreModel> GetAllStores();
        StoreModel GetStoreById(int id);
        StoreModel GetStore(int? id);
        void AddOrUpdateStore(StoreModel model);
        void DeleteStore(int id);

        List<CountryModel> GetAllCountry();
        List<SelectListItem> GetStatesByCountryId(int countryId);
        List<SelectListItem> GetCitiesByStateId(int stateId);
        bool HasPermission(int value, string tab);
        #endregion

    }

}
