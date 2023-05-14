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
    public class RentWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public RestCollection<Rent> Rents { get; set; }

        private Rent selectedRent;

        public Rent SelectedRent
        {
            get { return selectedRent; }
            set
            {
                if (value != null)
                {
                    selectedRent = new Rent()
                    {
                        RentId = value.RentId,
                        Name = value.Name,
                        Age = value.Age,
                        Gender = value.Gender,
                        Country = value.Country,
                        Rating = value.Rating,
                        Start = value.Start,
                        End = value.End,
                        MovieId = value.MovieId
                };
                    OnPropertyChanged();
                    (DeleteRentCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateRentCommand { get; set; }

        public ICommand DeleteRentCommand { get; set; }

        public ICommand UpdateRentCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public RentWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Rents = new RestCollection<Rent>("http://localhost:31652/", "rent", "hub");
                CreateRentCommand = new RelayCommand(() =>
                {
                    Rents.Add(new Rent()
                    {
                        Name = SelectedRent.Name,
                        Age = SelectedRent.Age,
                        Gender = SelectedRent.Gender,
                        Country = SelectedRent.Country,
                        Rating = SelectedRent.Rating,
                        Start = SelectedRent.Start,
                        End = SelectedRent.End,
                        MovieId = SelectedRent.MovieId
                    });
                });

                UpdateRentCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Rents.Update(SelectedRent);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }
                });

                DeleteRentCommand = new RelayCommand(() =>
                {
                    Rents.Delete(SelectedRent.RentId);
                },
                () =>
                {
                    return SelectedRent != null;
                });

                SelectedRent = new Rent();
            }
        }
    }
}
