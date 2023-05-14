using H645NF_HFT_2022231.Models;
using H645NF_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H645NF_HFT_2022231.Logic
{
    public class MovieLogic : IMovieLogic
    {
        //repo instance
        IRepository<Movie> repo;

        public MovieLogic(IRepository<Movie> repo)
        {
            this.repo = repo;
        }

        //CRUD methods
        public void Create(Movie item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Movie argument can not be null!");
            }

            foreach (var prop in item.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null && prop.Name != "MovieId" && prop.GetValue(item) == null)
                {
                    throw new ArgumentException($"Movie {prop.Name} can not be null!");
                }
            }

            if (item.Title.Length < 2)
            {
                throw new ArgumentException("Movie Title can not be one character long!");
            }
            else if (item.Year < 1895)
            {
                throw new ArgumentOutOfRangeException("Movie Year is out of range!");
            }
            else if (item.Country == "")
            {
                throw new ArgumentException("Movie Country can not be zero character long!");
            }
            else if (item.Budget < 0)
            {
                throw new ArgumentOutOfRangeException("Movie Budget can not be negative!");
            }
            else if (item.GenreId < 0)
            {
                throw new ArgumentOutOfRangeException("Movie GenreId can not be lower than zero!");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("MovieId can not be negative!");
            }
            this.repo.Delete(id);
        }

        public Movie Read(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("MovieId can not be negative!");
            }
            var result = this.repo.Read(id);
            if (result == null)
            {
                throw new NullReferenceException("Movie with given Id is null!");
            }
            return result;
        }

        public IQueryable<Movie> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Movie item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Movie argument can not be null!");
            }
            foreach (var prop in item.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null && prop.Name != "MovieId" && prop.GetValue(item) == null)
                {
                    throw new ArgumentNullException($"Movie {prop.Name} property can not be null!");
                }
            }

            if (item.Title.Length < 2)
            {
                throw new ArgumentException("Movie Title can not be one character long!");
            }
            else if (item.Year < 1895)
            {
                throw new ArgumentOutOfRangeException("Movie Year is out of range!");
            }
            else if (item.Country == "")
            {
                throw new ArgumentException("Movie Country can not be zero character long!");
            }
            else if (item.Budget < 0)
            {
                throw new ArgumentOutOfRangeException("Movie Budget can not be negative!");
            }
            else if (item.GenreId < 0)
            {
                throw new ArgumentOutOfRangeException("Movie GenreId can not be lower than zero!");
            }

            this.repo.Update(item);
        }

        //NON CRUD methods
        public IEnumerable<MoviesAverageRating> GetMoviesAverageRating()
        {
            var result = from m in repo.ReadAll()
                         where m.Rents.Count() != 0
                         select new MoviesAverageRating()
                         {
                             MovieTitle = m.Title,
                             AverageRating = Math.Round(m.Rents.Average(r => r.Rating), 2)
                         };
            return result;
        }
    }
}
