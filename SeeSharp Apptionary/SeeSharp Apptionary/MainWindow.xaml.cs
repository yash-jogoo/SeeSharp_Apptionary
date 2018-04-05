using System;
using System.Collections.Generic;
using System.IO;
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
            queryXML(searchBar.Text);
        }

        private void queryXML(string x)
        {
            var searchedTitle = from reference in XDocument
                    .Load(
                        @"../../References.xml")
                    .Descendants("Reference")
                where (string) reference.Element("Title") == x
                select new
                {
                    Title = reference.Element("Title").Value,
                    Definition = reference.Element("Definition").Value,
                    ImagePath = reference.Element("ImagePath").Value,
                    SeeAlsos = reference.Element("SeeAlsos")
                                        .Elements("SeeAlso")
                                        .Select(s => new SeeAlso {
                                            link = s.Value
                                        }),
                    RefencesLink = reference.Element("ReferenceLinks")
                                        .Elements("ReferenceLink")
                                        .Select(s => new ReferenceLink
                                        {
                                            referenceLink = s.Value
                                        })
                };
            if (searchedTitle.Count() == 0)
            {
                Title.Content = "No entries found for this chapter";
                DefinitionBlock.Text = String.Empty;
                definitionLabel.Content = String.Empty;
                imageLabel.Content = String.Empty;
                imageCode.Source = new BitmapImage(new Uri(@"", UriKind.RelativeOrAbsolute));
            }
            else
            {
                foreach (var item in searchedTitle)
                {
                    Title.Content = item.Title;
                    DefinitionBlock.Text = item.Definition;
                    definitionLabel.Content = "Definition";
                    imageLabel.Content = "Example";
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource =
                        new Uri(
                            @"C:\Users\hurna\Documents\GitHub\SeeSharp_Apptionary\SeeSharp Apptionary\SeeSharp Apptionary\assets\C#.PNG");
                    bitmap.EndInit();
                    imageCode.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath(item.ImagePath.Substring(8,item.ImagePath.Length-8)), UriKind.RelativeOrAbsolute));
                    Console.WriteLine(item.ImagePath.Substring(8, item.ImagePath.Length - 8));
                    Run seeAlsoRun = new Run("\nSee Also :");
                    seeAlsoRun.FontSize = 25;
                    seeAlsoRun.FontWeight = FontWeights.Bold;
                    DefinitionBlock.Inlines.Add(seeAlsoRun);
                    foreach (var s in item.SeeAlsos)
                    {
                        Run seeAlsosRun = new Run($"\n{s.link}");
                        Hyperlink hyperlink = new Hyperlink(seeAlsosRun);
                        hyperlink.Name = s.link;
                        hyperlink.Click += Button2_Click;
                        DefinitionBlock.Inlines.Add(hyperlink);
                    }
                    Run referencesRun = new Run("\nReferences :");
                    referencesRun.FontSize = 25;
                    referencesRun.FontWeight = FontWeights.Bold;
                    DefinitionBlock.Inlines.Add(referencesRun);
                    foreach (var s in item.RefencesLink)
                    {
                        Run newRun2 = new Run($"\n{s.referenceLink}");
                        DefinitionBlock.Inlines.Add(newRun2);
                    }

                }
             }
         }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            queryXML(((Hyperlink)sender).Name);
        }



        private void Button_Click(object sender, RoutedEventArgs e)
         {
             Subwindow subwindow = new Subwindow(searchBar.Text);
             subwindow.Show();
         }

         private void Button_Click_1(object sender, RoutedEventArgs e)
         {

             /* Run newRun = new Run("Click me \n");
             Hyperlink hyperlink = new Hyperlink(newRun);
             hyperlink.Click += Button1_Click;

             Run newRun3 = new Run("Click me3 ");
             Hyperlink hyperlink3 = new Hyperlink(newRun3);
             hyperlink3.Click += Button1_Click;


             DefinitionBlock.Inlines.Add(hyperlink);
             DefinitionBlock.Inlines.Add(hyperlink3); */

                        string newString = "a,b,c,v";
            var array = newString.Split(',');
            XDocument xmlDocument = XDocument.Load(@"../../References.xml");
            xmlDocument.Element("References").Add(
            new XElement("Reference",
                    new XElement("Title", "kelemrvpoafrbve"),
                    new XElement("Definition", "eornvpalerv"),
                    new XElement("ImagePath", "file:///C:/Users/hurna/Documents/GitHub/SeeSharp_Apptionary/SeeSharp Apptionary/SeeSharp Apptionary/bin/Debug/C#.png"),
                    new XElement("SeeAlsos",
                                 from item in array
                                 select new XElement("SeeAlso", item)
                                                    
                                )
                        )
            );



            xmlDocument.Save(@"../../References.xml");
            MessageBox.Show("Reference Added");
        }

       
        private void searchBar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                queryXML(searchBar.Text);
            }
        }
    }
}
