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

        private System.Windows.Forms.NotifyIcon MyNotifyIcon;

        public MainWindow()
        {
            InitializeComponent();

            MyNotifyIcon = new System.Windows.Forms.NotifyIcon();
            MyNotifyIcon.Icon = new System.Drawing.Icon(@"..\..\cs.ico");
            MyNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(MyNotifyIcon_MouseDoubleClick);
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

        void MyNotifyIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.WindowState = WindowState.Normal;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                MyNotifyIcon.BalloonTipTitle = "Minimize Successfull";
                MyNotifyIcon.BalloonTipText = "Minized the app";
                MyNotifyIcon.ShowBalloonTip(400);
                MyNotifyIcon.Visible = true;


            }
            else if (this.WindowState == WindowState.Normal)
            {
                MyNotifyIcon.Visible = false;
                this.ShowInTaskbar = true;
            }
        }


    }
}
