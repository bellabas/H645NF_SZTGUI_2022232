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
    public class GenreNonCRUDMethodsController : ControllerBase
    {
        IGenreLogic logic;
        IHubContext<SignalRHub> hub;
        public GenreNonCRUDMethodsController(IGenreLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<GenreWithAverageBudget> GetGenreWithAverageBudget()
        {
            return this.logic.GetGenreWithAverageBudget();
        }

        [HttpGet]
        public IEnumerable<MoviesByGenre> GetMoviesByGenre()
        {
            return this.logic.GetMoviesByGenre();
        }

    }
}
