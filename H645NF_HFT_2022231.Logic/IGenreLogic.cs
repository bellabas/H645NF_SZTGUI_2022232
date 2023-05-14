using H645NF_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H645NF_HFT_2022231.Logic
{
    public interface IGenreLogic
    {
        //CRUD methods
        void Create(Genre item);
        void Delete(int id);
        Genre Read(int id);
        IQueryable<Genre> ReadAll();
        void Update(Genre item);

        //NON CRUD methods
        IEnumerable<GenreWithAverageBudget> GetGenreWithAverageBudget();
        IEnumerable<MoviesByGenre> GetMoviesByGenre();
    }
}
