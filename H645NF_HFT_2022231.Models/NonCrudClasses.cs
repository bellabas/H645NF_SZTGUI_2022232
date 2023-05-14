using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H645NF_HFT_2022231.Models
{
    public class GenreWithAverageBudget
    {
        public GenreWithAverageBudget()
        {

        }

        public string Genre { get; set; }
        public double BudgetAverage { get; set; }
    }

    public class MoviesByGenre
    {
        public MoviesByGenre()
        {

        }

        public string GenreName { get; set; }
        public IEnumerable<string> MovieTitles { get; set; }
    }

    public class MoviesAverageRating
    {
        public MoviesAverageRating()
        {

        }

        public string MovieTitle { get; set; }
        public double AverageRating { get; set; }
    }

    public class NationalMovieRent
    {
        public NationalMovieRent()
        {

        }

        public string Name { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }
    }

    public class RentalNameWithMovieTitleAndGenre
    {
        public RentalNameWithMovieTitleAndGenre()
        {

        }

        public string Name { get; set; }
        public string MovieName { get; set; }
        public string Genre { get; set; }
    }

    public class RentedMovieTitleOfPerson
    {
        public RentedMovieTitleOfPerson()
        {

        }

        public string Name { get; set; }
        public string MovieTitle { get; set; }
    }

}
