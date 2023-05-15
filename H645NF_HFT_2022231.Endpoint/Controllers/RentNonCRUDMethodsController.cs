using H645NF_HFT_2022231.Endpoint.Services;
using H645NF_HFT_2022231.Logic;
using H645NF_HFT_2022231.Models;
using H645NF_HFT_2022231.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace H645NF_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class RentNonCRUDMethodsController : ControllerBase
    {
        IRentLogic logic;
        IHubContext<SignalRHub> hub;
        public RentNonCRUDMethodsController(IRentLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<RentalNameWithMovieTitleAndGenre> GetRentalNameWithMovieTitleAndGenre()
        {
            return this.logic.GetRentalNameWithMovieTitleAndGenre();
        }

        [HttpGet]
        public IEnumerable<NationalMovieRent> GetNationalMovieRent()
        {
            return this.logic.GetNationalMovieRent();
        }

        [HttpGet]
        public IEnumerable<RentedMovieTitleOfPerson> GetRentedMovieTitlesOfPerson(string name)
        {
            return this.logic.GetRentedMovieTitlesOfPerson(name);
        }

    }
}
