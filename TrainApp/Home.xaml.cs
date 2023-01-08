#region dependencies
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO.Packaging;
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
    /// Interaction logic for Home.xaml
    /// Author : Zin Lin Htun
    /// </summary>
    public partial class Home : Page
    {
        #region private
        private string[] stops { get; set; }
        private string[] addStops { get; set; }

        private string previousDate;
        #endregion

        #region public 
       
        public ObservableCollection<Train> Trains { get; set; }
        #endregion

        #region constructor
        public Home()
        {
            InitializeComponent();
            stops = new string[] { "Newcastle","York" };
            addStops = new string[] {"","" };
            previousDate = "";
            Trains = new ObservableCollection<Train>();

        }
        #endregion

        /// <summary>
        /// This function deals with Deleting either newcastle or york (Exclusion)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Deleter(object sender, RoutedEventArgs e)
        {
            // get sender button
            Button button = (Button)sender;
            // If button's content us Neewcastle
            if ((string)button.Content == "Newcastle")
            {
                // stop 1 is nothing
                stops[0] = "";
                addStops[0] = "Newcastle";
                btnAddNewCastle.Visibility = Visibility.Visible;
            }
            else
            {
                // stop 2 is nothing
                stops[1] = "";
                addStops[1] = "York";
                btnAddYork.Visibility = Visibility.Visible;
            }

            // Collasping the button
            button.Visibility = Visibility.Collapsed;
            if (stops[0] == "" && stops[1] == "")
            {
                Cities.Visibility = Visibility.Collapsed;
            }
            // The border for adding more stop shall now be shown
            CitiesAdd.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Adding Stops - Either Newcastle and or York : Inclusion 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Adder(object sender, RoutedEventArgs e)
        {
            // get sender button
            Button button = (Button)sender;
            // If button's content us Neewcastle
            if ((string)button.Content == "Newcastle")
            {
                // add stop 1 as newcastle
                stops[0] = "Newcastle";
                addStops[0] = "";
                btnNewCastle.Visibility = Visibility.Visible;
            }
            else
            {
                // add stop 1 as york
                stops[1] = "York";
                addStops[1] = "";
                btnYork.Visibility = Visibility.Visible;
            }

            // deal with visibility
            button.Visibility = Visibility.Collapsed;
            if (addStops[0] == "" && addStops[1] == "")
            {
                CitiesAdd.Visibility = Visibility.Collapsed;
            }
            Cities.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Validate Date data
        /// </summary>
        /// _dateValidation is to be used only in this class
        /// <param name="date"></param>
        /// <returns>Boolean Success</returns>
        private bool _dateValidation(string date)
        {
            // ans is to be returned
            bool ans = true;
            // split
            string[] values = date.Split("/");
            if (values.Length < 3)
            {
                // date formate wrong
                ans = false;
            }
            else
            {
                int day = Convert.ToInt32(values[0]);
                int month = Convert.ToInt32(values[1]);
                int year = Convert.ToInt32(values[2]);
                // check date and also check for leap year, if date is not valid, then ans will be set to false
                if (day > 31 || (day > 28 && month ==2) || month > 12 || (day >29 && month == 2 && year%4 ==0 && year%400 != 0)  )
                {
                    ans = false;
                }

            }
            return ans;
        }

        /// <summary>
        /// Validate Time Data
        /// </summary>
        /// _timeValidation is to be used only in this class
        /// <param name="time"></param>
        /// <returns></returns>
        private bool _timeValidation(string time)
        {
            // answer
            bool ans = true;
            // split
            string[] values = time.Split(":");
            // Check format
            if (values.Length >1)
            {
                int hrs = Convert.ToInt32(values[0]);
                int mins = Convert.ToInt32(values[1]);
                // chekc mins and hrs
                if (hrs > 23 || mins > 59)
                    ans = false;
            }
            else 
            {
                // checking hrs only
                int hrs = Convert.ToInt32(values[0]);
                if (hrs > 24)
                    ans = false;

            }

            // returning ans
            return ans;
        }

        /// <summary>
        /// When Text Changed in txtOrigin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOrigin_TextChanged(object sender, TextChangedEventArgs e)
        {
            // get text
            string text = txtOrigin.Text; 
            // If starts with e or w set to waverly
            if (text.StartsWith("E") || text.StartsWith("e") || text.StartsWith("w") || text.StartsWith("W"))
            {
                txtOrigin.Text = "Edinburgh Waverley";
                txtDest.Text = "King's Cross";
            }
            // elif starts with l or k set ti kingcross
            else if (text.StartsWith("L") || text.StartsWith("l") || text.StartsWith("k") || text.StartsWith("K"))
            {
                txtOrigin.Text = "King's Cross";
                txtDest.Text = "Edinburgh Waverley";

            }
            // else show this
            else
            {
                if (text != "")
                    MessageBox.Show("Please enter a valid station the train goes to");
                txtOrigin.Text = "";

            }

        }

        /// <summary>
        /// When Text changes in txtDest
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDest_TextChanged(object sender, TextChangedEventArgs e)
        {
            // get text
            string text = txtDest.Text;
            // if starts with e or w set to waverly
            if (text.StartsWith("E") || text.StartsWith("e") || text.StartsWith("w") || text.StartsWith("W"))
            {
                txtDest.Text = "Edinburgh Waverley";
                txtOrigin.Text = "King's Cross";
            }
            // if starts with l or k king cross
            else if (text.StartsWith("L") || text.StartsWith("l") || text.StartsWith("k") || text.StartsWith("K"))
            {
                txtDest.Text = "King's Cross";
                txtOrigin.Text = "Edinburgh Waverley";
            }
            // else show this
            else
            {
                if (text != "")
                    MessageBox.Show("Please enter a valid station the train goes to");
                txtDest.Text = "";

            }

            

        }

        /// <summary>
        /// Adding a Train to the Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add(object sender, RoutedEventArgs e)
        {
            // Get Singleton Value
            IdMaker generateId = IdMaker.getInstance();
            
            // setting attributes
            string id = generateId.getId().ToString()!; // get id from it
            string date = txtDate.Text != "" ? txtDate.Text : "22/12/2022"; // if not exist 
            string time = txtTime.Text != "" ? txtTime.Text : "19:30"; // if not exist
            string name = txtName.Text != "" ? txtName.Text : "Service: " + date + " "+ time; 
            string origin = txtOrigin.Text != "" ? txtOrigin.Text : "Edinburgh Waverley";
            string destination = txtDest.Text != "" ? txtDest.Text : "King's Cross";
            string stdCapacity = txtStdCap.Text != "" ? txtStdCap.Text : "300";
            string preCapacity = txtPreCap.Text != "" ? txtPreCap.Text : "100";

            try
            {
                // check if one exist but not other in capacity
                if ((txtPreCap.Text != "" && txtStdCap.Text == ""))
                {
                    int cap = Convert.ToInt32(txtPreCap.Text);
                    if (cap <=400)
                        stdCapacity = (400 - cap).ToString();

                }
                // check if one exist but not other in capacity
                if ((txtPreCap.Text == "" && txtStdCap.Text != ""))
                {
                    int cap = Convert.ToInt32(txtStdCap.Text);
                    if (cap <= 400)
                        preCapacity = (400 - cap).ToString();

                }
            }
            catch
            {
                MessageBox.Show("There was an Error");
            }

            // Vaerifying station to get the right format
            if (txtOrigin.Text.Contains("Edinburgh") || txtOrigin.Text.Contains("Waverley"))
            {
                txtOrigin.Text = "Edinburgh Waverley";
            }

            if (txtDest.Text.Contains("Edinburgh") || txtDest.Text.Contains("Waverley"))
            {
                txtOrigin.Text = "Edinburgh Waverley";
            }

            if (txtOrigin.Text.Contains("London") || txtOrigin.Text.Contains("King's Cross"))
            {
                txtOrigin.Text = "King's Cross";
            }

            if (txtDest.Text.Contains("London") || txtDest.Text.Contains("King's Cross"))
            {
                txtOrigin.Text = "King's Cross";
            }

            int stdCapacityInt, preCapacityInt;
            try
            {
                // sometimes people may enter problematic things
                // thus try catch
                stdCapacityInt = Convert.ToInt32(stdCapacity); // get standard capacity
                preCapacityInt = Convert.ToInt32(preCapacity); // get premium capacity

               
                // verify if over populated
                if ((stdCapacityInt + preCapacityInt > 400) || !((destination == "Edinburgh Waverley" && origin == "King's Cross") || (origin == "Edinburgh Waverley" && destination == "King's Cross")) || !_dateValidation(date) || !_timeValidation(time))
                {
                    MessageBox.Show("Wrong Train Data, Cannot be Added");
                }
                else
                {
                    // if all checks succeed, train will be added
                    Train train = new Train(id, name.Replace(",", ""), stdCapacityInt, preCapacityInt, origin, destination, time.Replace(",",""), date.Replace(",", ""), stops);
                    // implementing the Database <interface like class>
                    train.AddToDatabase();
                }

                // Resetting
                txtName.Text = "";
                txtDate.Text = "";
                txtTime.Text = "";
                txtOrigin.Text = "";
                txtDest.Text = "";
                txtStdCap.Text = "";
                txtPreCap.Text = "";
            }

            catch
            {
                // error handling 
                MessageBox.Show("Wrong Data Provided, Train Cannot be Added");
                
            }

            #region reset
            // resetting the ui

            this.stops = new string[] { "Newcastle", "York" };
            btnAddNewCastle.Visibility = Visibility.Collapsed;
            btnAddYork.Visibility = Visibility.Collapsed;
            CitiesAdd.Visibility = Visibility.Collapsed;

            btnNewCastle.Visibility = Visibility.Visible;
            btnYork.Visibility = Visibility.Visible;
            Cities.Visibility = Visibility.Visible;

            //Update View Model
            Trains = Train.GetTrains();
            if (Trains.Count > 0)
            {
                stpIfNoBooking.Visibility = Visibility.Collapsed;
            }
            data.ItemsSource = Trains;
            #endregion
        }

        /// <summary>
        /// Date Formatter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Formatter(object sender, TextChangedEventArgs e)
        {
            
            // get textbox
            TextBox tb = (TextBox)sender; // tb stands for textbox
            if (tb.Text.EndsWith("/"))
            {
                // format
               tb.Text =   tb.Text.Substring(0, tb.Text.Length-1) +"";
            }
            if (tb.CaretIndex == tb.Text.Length)
            {   // if cursor (insertion point is at the end)
                string text = tb.Text; // get text
                int length = text.Length;
                if ((length == 3 && text[2] != '/') || (length == 6 && text[5] != '/'))
                {
                    // if format is to be set to date format, we put / to deal with this.
                    string last = "" + (text[length - 1]);
                    tb.Text = text.Substring(0, length - 1) + "/" + last;
                    
                }


            }

            // always put the insertion point at the end.
            int newLength = tb.Text.Length;
            tb.Select(newLength, newLength);
            previousDate = tb.Text;

        }

        /// <summary>
        /// Time Formatter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer(object sender, TextChangedEventArgs e)
        {
            // gets textbox sender
            TextBox tb = (TextBox)sender; // tb stands for textbox
            if (tb.Text.EndsWith(":"))
            {
                // if text ends with : get substring
                tb.Text = tb.Text.Substring(0, tb.Text.Length - 1) + "";
            }
            if (tb.CaretIndex == tb.Text.Length)
            {
                string text = tb.Text;
                int length = text.Length;
                if ((length == 3 && text[2] != ':'))
                {
                    // put : if it is the right format
                    string last = "" + (text[length - 1]);
                    tb.Text = text.Substring(0, length - 1) + ":" + last;
                    
                }
            }
            // always put the cursor back at the end
            int newLength = tb.Text.Length;
            tb.Select(newLength, newLength);

        }

        /// <summary>
        /// Page_Loaded runs while loading the page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Set Title
            Title.Text = "Choose Train to Book";
            // Implements Database to get all the trains
            Trains = Train.GetTrains();

            if (Trains.Count > 0)
            {
                stpIfNoBooking.Visibility = Visibility.Collapsed;
            }

            // set itemsource for the listview
            data.ItemsSource = Trains;

        }

        /// <summary>
        /// When KeyDown on txtDate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.D1 && e.Key != Key.D2 && e.Key != Key.D3 && e.Key != Key.D4 &&
                e.Key != Key.D5 && e.Key != Key.D6 && e.Key != Key.D7 && e.Key != Key.D8 &&
                e.Key != Key.D9 && e.Key != Key.D0 && e.Key != Key.Back && e.Key != Key.Tab)
            {
                e.Handled = true;
            }

            if ((txtDate.Text.Length != txtDate.CaretIndex && (e.Key == Key.Back || e.Key == Key.Delete))
                )
            {
                // handles everykey except typing and backspace to limit format casualties
                e.Handled = true;
            }
        }

        /// <summary>
        /// hen KeyDown on txtTime
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTime_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key != Key.D1 && e.Key != Key.D2 && e.Key != Key.D3 && e.Key != Key.D4 &&
                e.Key != Key.D5 && e.Key != Key.D6 && e.Key != Key.D7 && e.Key != Key.D8 &&
                e.Key != Key.D9 && e.Key != Key.D0 && e.Key != Key.Back && e.Key != Key.Tab)
            {
                e.Handled = true;
            }

            if ((txtTime.Text.Length != txtTime.CaretIndex && (e.Key == Key.Back || e.Key == Key.Delete))
                )
            {
                // handles everykey except typing and backspace to limit format casualties
                e.Handled = true;
            }
          

        }

        /// <summary>
        /// Open Train Page, where you can book and or view train manifest
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenTrain(object sender, RoutedEventArgs e)
        {
            // get datacontext since we bind this in xaml
            string id = (string)((Button)sender).DataContext;
            // navigation from frame in mainWindow
            this.NavigationService.Navigate(new OpenTrains(id));
        }

        /// <summary>
        /// Open up Window to Test 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenTesting_Click(object sender, RoutedEventArgs e)
        {
            // Go to testing page
            this.NavigationService.Navigate(new Test());

        }

        /// <summary>
        /// Allows only number
        /// </summary>
        private void keyDownConrtoller (object sender,  KeyEventArgs e)
        {
            if (e.Key != Key.D1 && e.Key != Key.D2 && e.Key != Key.D3 && e.Key != Key.D4 &&
                e.Key != Key.D5 && e.Key != Key.D6 && e.Key != Key.D7 && e.Key != Key.D8 &&
                e.Key != Key.D9 && e.Key != Key.D0 && e.Key != Key.Back && e.Key != Key.Tab)
            {
                e.Handled = true;
            }
        }
    }
}
