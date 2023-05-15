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
using System.Windows.Shapes;

namespace H645NF_HFT_2022231.WpfClient
{
    /// <summary>
    /// Interaction logic for NoncrudWindow.xaml
    /// </summary>
    public partial class NoncrudWindow : Window
    {
        public NoncrudWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // set the ItemsSource binding
            var binding = new Binding("GenreWithAverageBudgets");
            binding.Mode = BindingMode.OneWay;
            listbox_result.SetBinding(ListBox.ItemsSourceProperty, binding);

            // set datatemplate
            DataTemplate template = new DataTemplate { DataType = typeof(ListBox) };

            FrameworkElementFactory stackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));
            stackPanelFactory.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            FrameworkElementFactory element1 = new FrameworkElementFactory(typeof(TextBlock));
            element1.SetBinding(TextBlock.TextProperty, new Binding("Genre"));
            stackPanelFactory.AppendChild(element1);

            FrameworkElementFactory element2 = new FrameworkElementFactory(typeof(TextBlock));
            element2.SetValue(TextBlock.TextProperty, " - ");
            stackPanelFactory.AppendChild(element2);

            FrameworkElementFactory element3 = new FrameworkElementFactory(typeof(TextBlock));
            element3.SetBinding(TextBlock.TextProperty, new Binding("BudgetAverage"));
            stackPanelFactory.AppendChild(element3);

            template.VisualTree = stackPanelFactory;

            listbox_result.ItemTemplate = template;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // set the ItemsSource binding
            var binding = new Binding("MoviesByGenres");
            binding.Mode = BindingMode.OneWay;
            listbox_result.SetBinding(ListBox.ItemsSourceProperty, binding);

            // set datatemplate
            DataTemplate template = new DataTemplate { DataType = typeof(ListBox) };

            FrameworkElementFactory stackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));
            stackPanelFactory.SetValue(StackPanel.OrientationProperty, Orientation.Vertical);

            FrameworkElementFactory element1 = new FrameworkElementFactory(typeof(TextBlock));
            element1.SetBinding(TextBlock.TextProperty, new Binding("GenreName"));
            stackPanelFactory.AppendChild(element1);

            FrameworkElementFactory listboxFactory = new FrameworkElementFactory(typeof(ListBox));
            listboxFactory.SetValue(ListBox.ItemsSourceProperty, new Binding("MovieTitles"));
            var style = new Style(typeof(ListBoxItem));
            style.Setters.Add(new Setter(Control.PaddingProperty, new Thickness(30, 0, 0, 0)));
            style.Setters.Add(new Setter(FocusableProperty, false));
            style.Setters.Add(new Setter(IsHitTestVisibleProperty, false));
            listboxFactory.SetValue(ListBox.ItemContainerStyleProperty, style);
            stackPanelFactory.AppendChild(listboxFactory);

            template.VisualTree = stackPanelFactory;

            listbox_result.ItemTemplate = template;

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // set the ItemsSource binding
            var binding = new Binding("MoviesAverageRatings");
            binding.Mode = BindingMode.OneWay;
            listbox_result.SetBinding(ListBox.ItemsSourceProperty, binding);

            // set datatemplate
            DataTemplate template = new DataTemplate { DataType = typeof(ListBox) };

            FrameworkElementFactory stackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));
            stackPanelFactory.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            FrameworkElementFactory element1 = new FrameworkElementFactory(typeof(TextBlock));
            element1.SetBinding(TextBlock.TextProperty, new Binding("MovieTitle"));
            stackPanelFactory.AppendChild(element1);

            FrameworkElementFactory element2 = new FrameworkElementFactory(typeof(TextBlock));
            element2.SetValue(TextBlock.TextProperty, " - ");
            stackPanelFactory.AppendChild(element2);

            FrameworkElementFactory element3 = new FrameworkElementFactory(typeof(TextBlock));
            element3.SetBinding(TextBlock.TextProperty, new Binding("AverageRating"));
            stackPanelFactory.AppendChild(element3);

            template.VisualTree = stackPanelFactory;

            listbox_result.ItemTemplate = template;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            // set the ItemsSource binding
            var binding = new Binding("RentalNameWithMovieTitleAndGenres");
            binding.Mode = BindingMode.OneWay;
            listbox_result.SetBinding(ListBox.ItemsSourceProperty, binding);

            // set datatemplate
            DataTemplate template = new DataTemplate { DataType = typeof(ListBox) };

            FrameworkElementFactory stackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));
            stackPanelFactory.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            FrameworkElementFactory element1 = new FrameworkElementFactory(typeof(TextBlock));
            element1.SetBinding(TextBlock.TextProperty, new Binding("Name"));
            stackPanelFactory.AppendChild(element1);

            FrameworkElementFactory element2 = new FrameworkElementFactory(typeof(TextBlock));
            element2.SetValue(TextBlock.TextProperty, " - ");
            stackPanelFactory.AppendChild(element2);

            FrameworkElementFactory element3 = new FrameworkElementFactory(typeof(TextBlock));
            element3.SetBinding(TextBlock.TextProperty, new Binding("MovieName"));
            stackPanelFactory.AppendChild(element3);

            FrameworkElementFactory element4 = new FrameworkElementFactory(typeof(TextBlock));
            element4.SetValue(TextBlock.TextProperty, " - ");
            stackPanelFactory.AppendChild(element4);

            FrameworkElementFactory element5 = new FrameworkElementFactory(typeof(TextBlock));
            element5.SetBinding(TextBlock.TextProperty, new Binding("Genre"));
            stackPanelFactory.AppendChild(element5);

            template.VisualTree = stackPanelFactory;

            listbox_result.ItemTemplate = template;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            // set the ItemsSource binding
            var binding = new Binding("NationalMovieRents");
            binding.Mode = BindingMode.OneWay;
            listbox_result.SetBinding(ListBox.ItemsSourceProperty, binding);

            // set datatemplate
            DataTemplate template = new DataTemplate { DataType = typeof(ListBox) };

            FrameworkElementFactory stackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));
            stackPanelFactory.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            FrameworkElementFactory element1 = new FrameworkElementFactory(typeof(TextBlock));
            element1.SetBinding(TextBlock.TextProperty, new Binding("Name"));
            stackPanelFactory.AppendChild(element1);

            FrameworkElementFactory element2 = new FrameworkElementFactory(typeof(TextBlock));
            element2.SetValue(TextBlock.TextProperty, " - ");
            stackPanelFactory.AppendChild(element2);

            FrameworkElementFactory element3 = new FrameworkElementFactory(typeof(TextBlock));
            element3.SetBinding(TextBlock.TextProperty, new Binding("Title"));
            stackPanelFactory.AppendChild(element3);

            FrameworkElementFactory element4 = new FrameworkElementFactory(typeof(TextBlock));
            element4.SetValue(TextBlock.TextProperty, " - ");
            stackPanelFactory.AppendChild(element4);

            FrameworkElementFactory element5 = new FrameworkElementFactory(typeof(TextBlock));
            element5.SetBinding(TextBlock.TextProperty, new Binding("Country"));
            stackPanelFactory.AppendChild(element5);

            template.VisualTree = stackPanelFactory;

            listbox_result.ItemTemplate = template;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            (DataContext as NoncrudWindowViewModel).NotifyCommand.Execute(null);
            // set the ItemsSource binding
            var binding = new Binding("RentedMovieTitleOfPersons");
            binding.Mode = BindingMode.OneWay;
            listbox_result.SetBinding(ListBox.ItemsSourceProperty, binding);

            // set datatemplate
            DataTemplate template = new DataTemplate { DataType = typeof(ListBox) };

            FrameworkElementFactory element1 = new FrameworkElementFactory(typeof(TextBlock));
            element1.SetBinding(TextBlock.TextProperty, new Binding("MovieTitle"));

            template.VisualTree = element1;

            listbox_result.ItemTemplate = template;
        }
    }
}
