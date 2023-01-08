#region dependencies
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
using TrainApp.BusinessLogic;
using TrainApp.Persistence;
#endregion

namespace TrainApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Author : Zin Lin Htun
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Testing purposes
        /// </summary>
        private void Testing ()
        {
            //Testing Purposes
            //Booking b;
            //b = new Booking();
            //Database.AddBooking(b);
        }

        /// <summary>
        /// MainWindow Constructor
        /// </summary>
        public MainWindow()
        {
            // staet app
            InitializeComponent();
            // write databaes if not exist
            Train.ConnectDatabase();
            Booking.ConnectDatabase();
            // new instance of Singleton
            IdMaker idMaker = IdMaker.getInstance()!;
            // set source
            Container.Source = new Uri("home.xaml",UriKind.Relative);
        }

    }
}
