using H645NF_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H645NF_HFT_2022231.Logic
{
    public interface IMovieLogic
    {
        //CRUD methods
        void Create(Movie item);
        void Delete(int id);
        Movie Read(int id);
        IQueryable<Movie> ReadAll();
        void Update(Movie item);

        //NON CRUD methods
        IEnumerable<MoviesAverageRating> GetMoviesAverageRating();
    }
}
