using Country_Store.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Country_State.Services.State
{
    public interface IStateService
    {
        List<StateModel> GetAll();
        
    }
}
