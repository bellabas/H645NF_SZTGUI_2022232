using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using H645NF_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace H645NF_HFT_2022231.WpfClient
{
    public class NoncrudWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }


        public RestCollection<GenreWithAverageBudget> GenreWithAverageBudgets { get; set; }
        public RestCollection<MoviesByGenre> MoviesByGenres { get; set; }
        public RestCollection<MoviesAverageRating> MoviesAverageRatings { get; set; }
        public RestCollection<NationalMovieRent> NationalMovieRents { get; set; }
        public RestCollection<RentalNameWithMovieTitleAndGenre> RentalNameWithMovieTitleAndGenres { get; set; }
        public RestCollection<RentedMovieTitleOfPerson> RentedMovieTitleOfPersons { get; set; }


        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public NoncrudWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                GenreWithAverageBudgets = new RestCollection<GenreWithAverageBudget>("http://localhost:31652/", "GenreNonCRUDMethods/GetGenreWithAverageBudget", "hub");
                MoviesByGenres = new RestCollection<MoviesByGenre>("http://localhost:31652/", "GenreNonCRUDMethods/GetMoviesByGenre", "hub");
                MoviesAverageRatings = new RestCollection<MoviesAverageRating>("http://localhost:31652/", "MovieNonCRUDMethods/GetMoviesAverageRating", "hub");
                NationalMovieRents = new RestCollection<NationalMovieRent>("http://localhost:31652/", "RentNonCRUDMethods/GetNationalMovieRent", "hub");
                RentalNameWithMovieTitleAndGenres = new RestCollection<RentalNameWithMovieTitleAndGenre>("http://localhost:31652/", "RentNonCRUDMethods/GetRentalNameWithMovieTitleAndGenre", "hub");
                RentedMovieTitleOfPersons = new RestCollection<RentedMovieTitleOfPerson>("http://localhost:31652/", $"RentNonCRUDMethods/GetRentedMovieTitlesOfPerson?name={name}", "hub");
            }
        }
    }
}
