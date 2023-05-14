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
    [Route("[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        IMovieLogic logic;
        IHubContext<SignalRHub> hub;
        public MovieController(IMovieLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Movie> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Movie Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Movie value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("MovieCreated", value);
        }

        [HttpPut]
        public void Put([FromBody] Movie value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("MovieUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var movieToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("MovieDeleted", movieToDelete);
        }
    }
}
