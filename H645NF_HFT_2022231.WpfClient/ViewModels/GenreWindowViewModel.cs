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
    public class GenreWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }


        public RestCollection<Genre> Genres { get; set; }

        private Genre selectedGenre;

        public Genre SelectedGenre
        {
            get { return selectedGenre; }
            set
            {
                if (value != null)
                {
                    selectedGenre = new Genre()
                    {
                        GenreId = value.GenreId,
                        Value = value.Value,
                    };
                    OnPropertyChanged();
                    (UpdateGenreCommand as RelayCommand).NotifyCanExecuteChanged();
                    (DeleteGenreCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateGenreCommand { get; set; }

        public ICommand DeleteGenreCommand { get; set; }

        public ICommand UpdateGenreCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }


        public GenreWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Genres = new RestCollection<Genre>("http://localhost:31652/", "genre", "hub");

                CreateGenreCommand = new RelayCommand(() =>
                {
                    Genres.Add(new Genre()
                    {
                        Value = SelectedGenre.Value
                    });
                });

                UpdateGenreCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Genres.Update(SelectedGenre);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                },
                () =>
                {
                    if (SelectedGenre != null && SelectedGenre.Value != null)
                    {
                        return true;
                    }
                    return false;
                });

                DeleteGenreCommand = new RelayCommand(() =>
                {
                    Genres.Delete(SelectedGenre.GenreId);
                },
                () =>
                {
                    if (SelectedGenre != null && SelectedGenre.Value != null)
                    {
                        return true;
                    }
                    return false;
                });
                SelectedGenre = new Genre();
            }

        }
    }
}
