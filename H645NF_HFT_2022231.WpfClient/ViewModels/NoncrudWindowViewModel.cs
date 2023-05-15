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
            set { SetProperty(ref name, value); (RentedMovieTitleOfPersonCommand as RelayCommand).NotifyCanExecuteChanged(); }
        }

        public ICommand RentedMovieTitleOfPersonCommand { get; set; }

        //collections
        public List<GenreWithAverageBudget> GenreWithAverageBudgets { get; set; }
        public List<MoviesByGenre> MoviesByGenres { get; set; }
        public List<MoviesAverageRating> MoviesAverageRatings { get; set; }
        public List<NationalMovieRent> NationalMovieRents { get; set; }
        public List<RentalNameWithMovieTitleAndGenre> RentalNameWithMovieTitleAndGenres { get; set; }
        public List<RentedMovieTitleOfPerson> RentedMovieTitleOfPersons { get; set; }

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
                var restservice = new RestService("http://localhost:31652/");

                GenreWithAverageBudgets = restservice.Get<GenreWithAverageBudget>("GenreNonCRUDMethods/GetGenreWithAverageBudget");
                MoviesByGenres = restservice.Get<MoviesByGenre>("GenreNonCRUDMethods/GetMoviesByGenre");
                MoviesAverageRatings = restservice.Get<MoviesAverageRating>("MovieNonCRUDMethods/GetMoviesAverageRating");
                NationalMovieRents = restservice.Get<NationalMovieRent>("RentNonCRUDMethods/GetNationalMovieRent");
                RentalNameWithMovieTitleAndGenres = restservice.Get<RentalNameWithMovieTitleAndGenre>("RentNonCRUDMethods/GetRentalNameWithMovieTitleAndGenre");

                RentedMovieTitleOfPersonCommand = new RelayCommand(() => { RentedMovieTitleOfPersons = restservice.Get<RentedMovieTitleOfPerson>($"RentNonCRUDMethods/GetRentedMovieTitlesOfPerson?name={Name}"); }, () => { return Name != ""; });
            }
        }
    }
}
