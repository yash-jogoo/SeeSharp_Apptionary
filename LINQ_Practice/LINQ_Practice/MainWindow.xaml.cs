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
using System.Xml.Linq;

namespace LINQ_Practice
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /* Movie[] moviesCollection = new Movie[]
            {
                new Movie {Name = "The Dictator", Genre = "Comedy", Rating = 9},
                new Movie {Name = "21 Jumpstreet", Genre = "Comedy", Rating = 9},
                new Movie {Name = "Spiderman", Genre = "Superhero", Rating = 8},
                new Movie {Name = "Batman", Genre = "Superhero", Rating = 10},
                new Movie {Name = "Harry Potter", Genre = "Fantasy", Rating = 9}
            };

            var comedyMovies = moviesCollection.Where(m => m.Genre == "Comedy");

            foreach (var item in comedyMovies)
            {
                Console.WriteLine(item.Name);
            } */
            var searchedMovie = from movie in XDocument.Load(@"C:\Users\hurna\Documents\Visual Studio 2017\Projects\LINQ_Practice\LINQ_Practice\Movies.xml").Descendants("Movie")
                                where (String)movie.Element("Name") == searchBar.Text
                                select new {
                                    Name = movie.Element("Name").Value,
                                    Genre = movie.Element("Genre").Value,
                                    Rating = movie.Element("Rating").Value,
                                };

            if(searchedMovie.Count() == 0)
            {
                nameLabel.Content = "No entries found for this movie";
                genreLabel.Content = "";
                ratingLabel.Content = "";
            }
            else
            {
                foreach (var item in searchedMovie)
                {
                    nameLabel.Content = $"Movie Name: {item.Name}";
                    genreLabel.Content = $"Movie Genre: {item.Genre}";
                    ratingLabel.Content = $"Movie Name: {item.Rating}";
                }
            }

            

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            XDocument xmlDocument = new XDocument(
                new XDeclaration("1.0","utf-8","yes"),
                new XElement("Movies",
                    new XElement("Movie",
                        new XElement("Name","The Dictator"),
                        new XElement("Genre", "Comedy"),
                        new XElement("Rating", "9")
                    ),
                    new XElement("Movie",
                        new XElement("Name", "Jumanji"),
                        new XElement("Genre", "Action"),
                        new XElement("Rating", "5")
                    ),
                    new XElement("Movie",
                        new XElement("Name", "21 Jumpstreet"),
                        new XElement("Genre", "Comedy"),
                        new XElement("Rating", "9")
                    )
                )
            );

            xmlDocument.Save(@"C:\Users\hurna\Documents\Visual Studio 2017\Projects\LINQ_Practice\LINQ_Practice\Movies.xml");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            XDocument xmlDocument = XDocument.Load(@"C:\Users\hurna\Documents\Visual Studio 2017\Projects\LINQ_Practice\LINQ_Practice\Movies.xml");
            xmlDocument.Element("Movies").Add(
                new XElement("Movie",
                        new XElement("Name", movieName.Text),
                        new XElement("Genre", movieGenre.Text),
                        new XElement("Rating", movieRating.Text)
                    )
            );

            xmlDocument.Save(@"C:\Users\hurna\Documents\Visual Studio 2017\Projects\LINQ_Practice\LINQ_Practice\Movies.xml");
        }
    }

    class Movie
    {
        public String Name;
        public String Genre;
        public int Rating;
    }
}
