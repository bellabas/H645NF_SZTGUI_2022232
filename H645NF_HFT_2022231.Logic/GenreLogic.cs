using H645NF_HFT_2022231.Models;
using H645NF_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H645NF_HFT_2022231.Logic
{
    public class GenreLogic : IGenreLogic
    {
        //repo instance
        IRepository<Genre> repo;

        //CRUD methods
        public GenreLogic(IRepository<Genre> repo)
        {
            this.repo = repo;
        }

        public void Create(Genre item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Genre argument can not be null!");
            }
            else if (item.Value == null)
            {
                throw new ArgumentNullException("Genre Value property can not be null!");
            }
            else if (item.Value == "" || item.Value.Length < 3)
            {
                throw new ArgumentOutOfRangeException("Genre Value is out of range!");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("GenreId can not be negative!");
            }
            this.repo.Delete(id);
        }

        public Genre Read(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("GenreId can not be negative!");
            }
            var result = this.repo.Read(id);
            if (result == null)
            {
                throw new NullReferenceException("Genre with given Id is null!");
            }
            return result;
        }

        public IQueryable<Genre> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Genre item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Genre argument can not be null!");
            }
            var prop = item.GetType().GetProperty("Value");
            if (prop.GetValue(item) == null)
            {
                throw new ArgumentNullException($"Genre {prop.Name} property can not be null!");
            }
            else if (item.Value == "" || item.Value.Length < 3)
            {
                throw new ArgumentOutOfRangeException("Genre Value is out of range!");
            }
            this.repo.Update(item);
        }

        //NON CRUD methods
        public IEnumerable<GenreWithAverageBudget> GetGenreWithAverageBudget()
        {
            var result = from x in repo.ReadAll()
                         select new GenreWithAverageBudget()
                         {
                             Genre = x.Value,
                             BudgetAverage = x.Movies.Average(m => m.Budget)
                         };
            return result;
        }

        public IEnumerable<MoviesByGenre> GetMoviesByGenre()
        {
            var result = from g in repo.ReadAll()
                         select new MoviesByGenre()
                         {
                             GenreName = g.Value,
                             MovieTitles = g.Movies.Select(m => m.Title)
                         };
            return result;
        }

    }
}
