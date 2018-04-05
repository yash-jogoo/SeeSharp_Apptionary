using Microsoft.Win32;
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
using System.Xml.Linq;

namespace SeeSharp_Apptionary
{
    /// <summary>
    /// Interaction logic for Subwindow.xaml
    /// </summary>
    public partial class Subwindow : Window
    {
        public Subwindow(string text)
        {
            InitializeComponent();
            titleBox.Text = text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] array = seeAlsoBox.Text.Split(',');
                string[] linkArray = referencesBox.Text.Split(','); 
                XDocument xmlDocument = XDocument.Load(@"../../References.xml");
                xmlDocument.Element("References").Add(
                new XElement("Reference",
                        new XElement("Title", titleBox.Text),
                        new XElement("Definition", definitionBox.Text),
                        new XElement("ImagePath",imageCode.Source),
                        new XElement("SeeAlsos",
                                     from item in array
                                     select new XElement("SeeAlso", item)
                                    ),
                        new XElement("ReferenceLinks",
                                     from item in linkArray
                                     select new XElement("ReferenceLink", item)
                                    )
                            )
                        
                            
                );

                

                xmlDocument.Save(@"../../References.xml");
                MessageBox.Show("Reference Added");
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot access application's database");
                Close();
                throw;
            }
            
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imageCode.Source = new BitmapImage(new Uri(op.FileName));
            }
        }
    }
}
