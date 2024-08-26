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
using WpfApp1.Controllers;
using WpfApp1.Models;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MovieController controller;
        private Movie selectedMovie;

        public MainWindow()
        {
            InitializeComponent();

            controller = new MovieController();
            LoadMovies();
        }

        public void AddMovieButton_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleTextBox.Text;
            string director = DirectorTextBox.Text;
            if(int.TryParse(YearTextBox.Text, out int year))
            {
                string genre = GenreTextBox.Text;
                Movie movie = new Movie { Title = title, Director = director, Year = year, Genre = genre };

                controller.AddMovie(movie);
                LoadMovies();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Please enter a valid year.");
            }

          
        }
        public void DeleteMovieButton_Click(Object sender, RoutedEventArgs e)
        {
            if((sender as Button)?.Tag is Movie movie)
            {
                controller.RemoveMovie(movie);
                LoadMovies();
            }
        }

        public void SelectMovieButton_Click(object sender, RoutedEventArgs e)
        {
            if((sender as Button)?.Tag is Movie movie)
            {
                selectedMovie = movie;
                TitleTextBox.Text = movie.Title;
                DirectorTextBox.Text = movie.Director;
                YearTextBox.Text = movie.Year.ToString();
                GenreTextBox.Text = movie.Genre;
            }
        }

        public void UpdateMovieButton_Click(object sender, RoutedEventArgs e)
        {
            if(selectedMovie != null)
            {
                selectedMovie.Title = TitleTextBox.Text;
                selectedMovie.Director = DirectorTextBox.Text;
                if(int.TryParse(YearTextBox.Text, out int year))
                {
                    selectedMovie.Year = year;
                }
                selectedMovie.Genre = GenreTextBox.Text;

                controller.UpdateMovie(selectedMovie);
                LoadMovies();
                ClearFields();
                selectedMovie = null;
            }
            else
            {
                MessageBox.Show("Please select a movie to update");
            }
        }

        public void DeleteSelectedButton_Click(object sender, RoutedEventArgs e)
        {

            //var selectedMovies = new List<Movie>();

            //foreach(var item in MoviesListBox.Items)
            //{
            //    var container = (ListBoxItem)MoviesListBox.ItemContainerGenerator.ContainerFromItem(item);

            //    if(container != null)
            //    {
            //        var checkBox = (CheckBox)container.ContentTemplate.FindName("DeleteCheckBox", container);

            //        if(checkBox != null && checkBox.IsChecked == true)
            //        {
            //            selectedMovies.Add((Movie)item);
            //        }
            //    }
            //}

            //foreach(var movie in selectedMovies)
            //{
            //    controller.RemoveMovie(movie);
            //}

            //LoadMovies();

            var moviesToRemove = MoviesListBox.Items.Cast<Movie>()
                .Where(movie => movie.IsSelected)
                .ToList();

            foreach(var movie in moviesToRemove)
            {
                controller.RemoveMovie(movie);
            }

            LoadMovies();
        }

        private void LoadMovies()
        {
            MoviesListBox.ItemsSource = null;
            MoviesListBox.ItemsSource = controller.GetMovies();

            //MoviesListBox.ItemsSource = controller.GetMovies().Select(movie =>
            //{
            //    return new CheckBox
            //    {
            //        Content = movie.Title,
            //        IsChecked = false,
            //        Tag = movie
            //    };
            //}).ToList();

        }

        private void ClearFields()
        {
            TitleTextBox.Text = "";
            DirectorTextBox.Text = "";
            YearTextBox.Text = "";
            GenreTextBox.Text = "";
        }
    }
}
