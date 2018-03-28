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
        public Subwindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XDocument xmlDocument = XDocument.Load(@"C:\Users\hurna\Documents\GitHub\SeeSharp_Apptionary\SeeSharp Apptionary\SeeSharp Apptionary\References.xml");
                xmlDocument.Element("References").Add(
                new XElement("Reference",
                        new XElement("Title", titleBox.Text),
                        new XElement("Definition", definitionBox.Text)
                    )
                );

                xmlDocument.Save(@"C:\Users\hurna\Documents\GitHub\SeeSharp_Apptionary\SeeSharp Apptionary\SeeSharp Apptionary\References.xml");
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot access application's database");
                Close();
                throw;
            }
            
            
        }
    }
}
