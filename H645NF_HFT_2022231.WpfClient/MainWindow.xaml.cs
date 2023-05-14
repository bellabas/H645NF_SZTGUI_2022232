using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace H645NF_HFT_2022231.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Genre(object sender, RoutedEventArgs e)
        {
            GenreWindow genreWindow = new GenreWindow();
            genreWindow.Show();
            Close();
        }

        private void Button_Click_Rent(object sender, RoutedEventArgs e)
        {
            RentWindow rentWindow = new RentWindow();
            rentWindow.Show();
            Close();
        }

        private void Button_Click_Movie(object sender, RoutedEventArgs e)
        {
            MovieWindow movieWindow = new MovieWindow();
            movieWindow.Show();
            Close();
        }

        private void Button_Click_NonCrud(object sender, RoutedEventArgs e)
        {
            NoncrudWindow noncrudWindow = new NoncrudWindow();
            noncrudWindow.Show();
            Close();
        }
    }
}
