﻿using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Country_Store.Models;
using Country_Store.Services.Country;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System;

namespace Country_Store.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public AdminService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MyConnection");
        }

        #region Country
              
        public List<CountryModel> GetAllCountries()
        {
            var countries = new List<CountryModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[SP_Country_GetAll]", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    countries.Add(new CountryModel
                    {
                        CountryId = Convert.ToInt32(reader["CountryId"]),
                        CountryName = reader["CountryName"].ToString(),
                        CountryCode = reader["CountryCode"].ToString(),
                        Continent = reader["Continent"].ToString(),
                        CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                    });
                }
            }

            return countries;
        }

        // dropdowns 
       
        public List<SelectListItem> GetCountriesDropDown()
        {
            var list = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("[SP_Country_GetAll]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new SelectListItem
                    {
                        Value = dr["CountryId"].ToString(),
                        Text = dr["CountryName"].ToString()
                    });
                }
            }

            return list;
        }

        //  For Edit page
        public CountryModel GetCountryById(int id)
        {
            CountryModel country = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_Country_GetById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CountryId", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    country = new CountryModel
                    {
                        CountryId = Convert.ToInt32(reader["CountryId"]),
                        CountryName = reader["CountryName"].ToString(),
                        CountryCode = reader["CountryCode"].ToString(),
                        Continent = reader["Continent"].ToString(),
                        CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                    };
                }
            }

            return country;
        }

     
        public void AddOrUpdateCountry(CountryModel country)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[SP_AddOrUpdateCountry]", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CountryId", country.CountryId);
                cmd.Parameters.AddWithValue("@CountryName", country.CountryName);
                cmd.Parameters.AddWithValue("@CountryCode", country.CountryCode);
                cmd.Parameters.AddWithValue("@Continent", country.Continent);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

  
        public void DeleteCountry(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[SP_Country_Delete]", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CountryId", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        #endregion


        #region State
        public List<StateModel> GetAllState()
        {
            List<StateModel> list = new List<StateModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[SP_State_GetAll]", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new StateModel
                    {
                        StateId = (int)reader["StateId"],
                        StateName = reader["StateName"].ToString(),
                        Capital = reader["Capital"].ToString(),
                        Language = reader["Language"].ToString(),
                        CountryId = (int)reader["CountryId"],
                        CountryName = reader["CountryName"].ToString()
                    });
                }
            }

            return list;
        }


        public StateModel GetStateByID(int id)
        {
            StateModel state = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("SP_State_GetById", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StateId", id);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        state = new StateModel
                        {
                            StateId = Convert.ToInt32(dr["StateId"]),
                            StateName = dr["StateName"].ToString(),
                            Capital = dr["Capital"].ToString(),
                            Language = dr["Language"].ToString(),
                            CountryId = Convert.ToInt32(dr["CountryId"]),
                            CountryName = dr["CountryName"].ToString()
                        };
                    }
                }
            }

            return state;
        }



        public void AddOrEditState(StateModel state)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("[SP_AddOrUpdateState]", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@StateId", state.StateId);
                        cmd.Parameters.AddWithValue("@StateName", state.StateName);
                        cmd.Parameters.AddWithValue("@Capital", state.Capital);
                        cmd.Parameters.AddWithValue("@Language", state.Language);
                        cmd.Parameters.AddWithValue("@CountryId", state.CountryId);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while inserting/updating state: " + ex.Message);
            }
        }


        public void DeleteState(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("[SP_State_Delete]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StateId", id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) // Foreign key constraint violation
                {
                    throw new InvalidOperationException("Cannot delete this state because it has related cities.");
                }

                throw;
            }
        }

        #endregion

        #region City

        public List<CityModel> GetAllCity()
        {
            var list = new List<CityModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[SP_City_GetAll]", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new CityModel
                    {
                        CityId = (int)reader["CityId"],
                        CityName = reader["CityName"].ToString(),
                        PinCode = reader["PinCode"].ToString(),
                        Population = (int)reader["Population"],
                        StateId = (int)reader["StateId"],
                        StateName = reader["StateName"].ToString()
                    });
                }
            }

            return list;
        }
        public CityModel GetIdByCity(int id)
        {
            CityModel city = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[SP_City_GetById]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CityId", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    city = new CityModel
                    {
                        CityId = (int)reader["CityId"],
                        CityName = reader["CityName"].ToString(),
                        PinCode = reader["PinCode"].ToString(),
                        Population = (int)reader["Population"],
                        StateId = (int)reader["StateId"],
                        StateName = reader["StateName"].ToString(),
                        CountryId = (int)reader["CountryId"]
                    };
                }
            }

            return city;
        }

        public void AddOrUpdateCity(CityModel city)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[SP_AddOrUpdateCity]", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CityId", city.CityId);
                cmd.Parameters.AddWithValue("@CityName", city.CityName);
                cmd.Parameters.AddWithValue("@PinCode", city.PinCode);
                cmd.Parameters.AddWithValue("@Population", city.Population);
                cmd.Parameters.AddWithValue("@StateId", city.StateId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCity(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("[SP_City_Delete]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CityId", id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex) when (ex.Number == 547) // FK constraint violation
            {
                throw new InvalidOperationException("Cannot delete this city because it is referenced in other records.");
            }
        }

        #endregion

        #region Store

        public List<SelectListItem> GetCitiesByStateId(int stateId)
        {
            var cities = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("[PL_CityGetByStateId]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StateId", stateId);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    cities.Add(new SelectListItem
                    {
                        Value = rdr["CityId"].ToString(),
                        Text = rdr["CityName"].ToString()
                    });
                }
            }

            return cities;
        }

        public List<SelectListItem> GetStatesByCountryId(int countryId)
        {
            var states = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("[PL_StateGetByCountryId]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CountryId", countryId);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    states.Add(new SelectListItem
                    {
                        Value = rdr["StateId"].ToString(),
                        Text = rdr["StateName"].ToString()
                    });
                }
            }

            return states;
        }

        public List<CountryModel> GetAllCountry()
        {
            var list = new List<CountryModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("[SP_Country_GetAll]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new CountryModel
                    {
                        CountryId = Convert.ToInt32(dr["CountryId"]),
                        CountryName = dr["CountryName"].ToString(),
                        CountryCode = dr["CountryCode"].ToString(),
                        Continent = dr["Continent"].ToString(),
                        CreatedDate = Convert.ToDateTime(dr["CreatedDate"])
                    });
                }
            }

            return list;
        }

        public void AddOrUpdateStore(StoreModel model)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("PL_StoreAddOrUpdate", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StoreId", model.StoreId);
                cmd.Parameters.AddWithValue("@StoreName", model.StoreName);
                cmd.Parameters.AddWithValue("@OpeningTime", model.OpeningTime);
                cmd.Parameters.AddWithValue("@ClosingTime", model.ClosingTime);
                cmd.Parameters.AddWithValue("@CountryId", model.CountryId);
                cmd.Parameters.AddWithValue("@StateId", model.StateId);
                cmd.Parameters.AddWithValue("@CityId", model.CityId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteStore(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand("PL_StoreDelete", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StoreId", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex) when (ex.Number == 547) 
            {
                throw new InvalidOperationException("Cannot delete this store because it is referenced in other records.");
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the store.", ex);
            }
        }

        public StoreModel GetStoreById(int id)
        {
            var model = new StoreModel();

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("PL_StoreGetById", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StoreId", id);
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    model.StoreId = Convert.ToInt32(rdr["StoreId"]);
                    model.StoreName = rdr["StoreName"].ToString();
                    model.OpeningTime = (TimeSpan)rdr["OpeningTime"];
                    model.ClosingTime = (TimeSpan)rdr["ClosingTime"];
                    model.CountryId = Convert.ToInt32(rdr["CountryId"]);
                    model.StateId = Convert.ToInt32(rdr["StateId"]);
                    model.CityId = Convert.ToInt32(rdr["CityId"]);
                }
            }

            return model;
        }
        public StoreModel GetStore(int? id)
        {
            return id == null || id == 0 ? new StoreModel() : GetStoreById(id.Value);
        }

        public List<StoreModel> GetAllStores()
        {
            var stores = new List<StoreModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("PL_StoreGetAll", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    stores.Add(new StoreModel
                    {
                        StoreId = Convert.ToInt32(rdr["StoreId"]),
                        StoreName = rdr["StoreName"].ToString(),
                        Description = rdr["Description"].ToString(),
                      
                        OpeningTime = (TimeSpan)rdr["OpeningTime"],
                        ClosingTime = (TimeSpan)rdr["ClosingTime"],
                       
                        CountryName = rdr["CountryName"].ToString(),
                        StateName = rdr["StateName"].ToString(),
                        CityName = rdr["CityName"].ToString()
                    });
                }
            }

            return stores;
        }

        public bool HasPermission(int value, string tab)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}