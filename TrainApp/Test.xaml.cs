#region dependencies
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
using System.Windows.Shapes;
using TrainApp.BusinessLogic;
using TrainApp.Persistence;
#endregion

namespace TrainApp
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// Author : Zin Lin Htun
    /// Date Modified : 18/11/22
    /// </summary>
    public partial class Test : Page
    {
        #region private
        private string filename { get; set; }
        #endregion

        #region constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public Test()
        {
            // set filename
            filename = "trains.csv";
            InitializeComponent();
            // set itemsource 
            data.ItemsSource = Helpers.GetDatabaseLineByLine(@filename); // get every line of database as a list


        }
        #endregion

        #region methods
        /// <summary>
        /// When Clicked btnSee
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSee_Click(object sender, RoutedEventArgs e)
        {
            // if file is trains then bookings and vice versa
            if (filename == "trains.csv")
            {
                filename = "bookings.csv";
                txtBtnFileName.Text = " See Trains";
                data.ItemsSource = Helpers.GetDatabaseLineByLine(@filename);
                txtFileName.Text = "bookings.csv";
                
            }
            else
            {
                filename = "trains.csv";
                txtBtnFileName.Text = " See Bookings";
                data.ItemsSource = Helpers.GetDatabaseLineByLine(@filename);
                txtFileName.Text = "trains.csv";
            }
        }

        /// <summary>
        /// Resetting All Databases
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            // Reset bookings by implementing Database <interface>
            Train.ResetDatabase ();
            // Reset trains by implementing Database <interface>
            Booking.ResetDatabase ();
            // update listview itemsource - at this point it is an empty set
            data.ItemsSource = Helpers.GetDatabaseLineByLine(@filename);

        }

        /// <summary>
        /// Navigate Back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            // Go Back
            this.NavigationService.GoBack();
        }
        #endregion
    }
}
