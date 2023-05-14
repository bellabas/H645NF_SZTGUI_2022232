using H645NF_HFT_2022231.Models;
using H645NF_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H645NF_HFT_2022231.Logic
{
    public class RentLogic : IRentLogic
    {
        //repo instance
        IRepository<Rent> repo;

        //CRUD methods
        public RentLogic(IRepository<Rent> repo)
        {
            this.repo = repo;
        }

        public void Create(Rent item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Rent argument can not be null!");
            }

            foreach (var prop in item.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null && prop.Name != "RentId" && prop.Name != "Interval" && prop.GetValue(item) == null)
                {
                    throw new ArgumentException($"Rent {prop.Name} can not be null!");
                }
            }

            if (item.Name == "" || item.Name.Length < 3)
            {
                throw new ArgumentException("Rent Name can not be two character long!");
            }
            else if (item.Age < 16)
            {
                throw new ArgumentOutOfRangeException("Rent Age is out of range!");
            }
            else if (item.Gender.Length < 4)
            {
                throw new ArgumentException("Rent Gender can not be shorter than four character!");
            }
            else if (item.Country == "")
            {
                throw new ArgumentException("Rent Country can not be zero character long!");
            }
            else if (item.Rating < 1 || item.Rating > 10)
            {
                throw new ArgumentOutOfRangeException("Rent's Movie Rating can only be in the one to ten range!");
            }
            else if (item.MovieId < 0)
            {
                throw new ArgumentOutOfRangeException("Rent's MovieId can not be lower than one!");
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

        public Rent Read(int id)
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
            return this.repo.Read(id);
        }

        public IQueryable<Rent> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Rent item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Rent argument can not be null!");
            }
            foreach (var prop in item.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null && prop.Name != "RentId" && prop.Name != "Interval" && prop.GetValue(item) == null)
                {
                    throw new ArgumentNullException($"Rent {prop.Name} property can not be null!");
                }
            }

            if (item.Name == "" || item.Name.Length < 3)
            {
                throw new ArgumentException("Rent Name can not be two character long!");
            }
            else if (item.Age < 16)
            {
                throw new ArgumentOutOfRangeException("Rent Age is out of range!");
            }
            else if (item.Gender.Length < 4)
            {
                throw new ArgumentException("Rent Gender can not be shorter than four character!");
            }
            else if (item.Country == "")
            {
                throw new ArgumentException("Rent Country can not be zero character long!");
            }
            else if (item.Rating < 1)
            {
                throw new ArgumentOutOfRangeException("Rent's Movie Rating can not be lower than one!");
            }
            else if (item.MovieId < 0)
            {
                throw new ArgumentOutOfRangeException("Rent's MovieId can not be lower than one!");
            }

            this.repo.Update(item);
        }

        //NON CRUD methods
        public IEnumerable<RentalNameWithMovieTitleAndGenre> GetRentalNameWithMovieTitleAndGenre()
        {
            var result = from r in repo.ReadAll()
                         select new RentalNameWithMovieTitleAndGenre()
                         {
                             Name = r.Name,
                             MovieName = r.Movie.Title,
                             Genre = r.Movie.Genre.Value
                         };
            return result;
        }

        public IEnumerable<NationalMovieRent> GetNationalMovieRent()
        {
            var result = from r in repo.ReadAll()
                         where r.Country == r.Movie.Country
                         select new NationalMovieRent()
                         {
                             Name = r.Name,
                             Title = r.Movie.Title,
                             Country = r.Country
                         };
            return result;
        }

        public IEnumerable<RentedMovieTitleOfPerson> GetRentedMovieTitlesOfPerson(string name)
        {
            var result = from r in repo.ReadAll()
                         where r.Name == name
                         select new RentedMovieTitleOfPerson()
                         {
                             Name = r.Name,
                             MovieTitle = r.Movie.Title
                         };
            return result;
        }

    }
}
