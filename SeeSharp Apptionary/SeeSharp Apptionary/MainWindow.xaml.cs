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

namespace SeeSharp_Apptionary
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


        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            var searchedTitle = from reference in XDocument
                    .Load(
                        @"C:\Users\Yash\Documents\GitHub\SeeSharp_Apptionary\SeeSharp Apptionary\SeeSharp Apptionary\References.xml")
                    .Descendants("Reference")
                                where (string)reference.Element("Title") == searchBar.Text
                                select new
                                {
                                    Title = reference.Element("Title").Value,
                                    Definition = reference.Element("Definition").Value,
                                };
            if (searchedTitle.Count() == 0)
            {
                Title.Content = "No entries found for this chapter";
                DefinitionBlock.Text = String.Empty;
                definitionLabel.Content = String.Empty;
            }
            else
            {
                foreach (var item in searchedTitle)
                {
                    Title.Content = item.Title;
                    DefinitionBlock.Text = item.Definition;
                    definitionLabel.Content = "Definition";
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Subwindow subwindow = new Subwindow();
            subwindow.Show();
        }


    }
}
