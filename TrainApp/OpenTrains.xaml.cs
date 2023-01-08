#region dependencies
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrainApp.BusinessLogic;
using TrainApp.Persistence;
#endregion

namespace TrainApp
{
    /// <summary>
    /// Interaction logic for OpenTrain
    /// Author : Zin Lin Htun
    /// Design : OOP
    /// Matriculation : 40542237
    /// </summary>
    public partial class OpenTrains : Page
    {
        #region private
        private string trainId;
        private string classes;
        private bool meals;
        private List<int> seats;
        #endregion

        #region public
        public Train Train { get; set; }
        public List<string> Cities = new List<string> { "Edinburgh", "London", "York", "Newcastle" };
        public List<string> AddStart { get; set; }
        public List<string> AddEnd { get; set; }
        public bool ifDataExist { get; set; }
        
        public ObservableCollection<Booking> Bookings;
        public bool greater { get; set; }
        #endregion

        #region constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="trainId"></param>
        public OpenTrains(string trainId)
        {
            InitializeComponent();
            // setting trainId
            this.trainId = trainId;
            // initiating private and public components/attributes
            classes = "Standard Class";
            seats = new List<int>();
            meals = false;
            Train = Train.GetTrain(trainId);
            this.Bookings = Train.GetBookings(trainId);
            this.ifDataExist = Bookings.Count > 0 ? true : false;

            // Setting Up AddStart
            // This is to limit the stations for booking to start from
            AddStart = new List<string>();
            AddStart.Add(Train.Origin);
            if (Train.Stops[0] != "")
                AddStart.Add(Train.Stops[0]);
            if (Train.Stops[1] != "")
                AddStart.Add(Train.Stops[1]);

            // Setting Up AddEnd
            // This is so the the train departure station is not end station on the ticket.
            AddEnd = new List<string>();
            if (Train.Stops[0] != "")
                AddEnd.Add(Train.Stops[0]);
            if (Train.Stops[1] != "")
                AddEnd.Add(Train.Stops[1]);
            AddEnd.Add(Train.Destination);

        }

        #endregion

        #region private method

        /// <summary>
        /// GoBack to the previous page via the usage of a Frame in the MainWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void GoBack(object sender, RoutedEventArgs e)
        {
            #region animation
            DoubleAnimation d = new DoubleAnimation();
            d.From = 1.0;
            d.To = 0.0;

            d.Duration = new Duration(TimeSpan.FromSeconds(0.3));

            this.BeginAnimation(OpacityProperty, d);
            await Task.Delay(300);
            #endregion
            // toInfintieBolsh();

            try
            {
                this.NavigationService.GoBack(); // Go Back
            }

            catch { }
        }

        /// <summary>
        /// Called when Page is Loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Assign Values
            string departureStation = Train.Origin;
            string arrivalStation = Train.Destination;
            string stdCap = Train.StdCap.ToString();
            string preCap = Train.PreCap.ToString();
            Cities depart = Helpers.GenerateCity(departureStation);
            Cities arrival = Helpers.GenerateCity(arrivalStation);

            // greater if departure is less than arrival
            greater = depart < arrival ? true : false;
            txtStdCap.Text += (" " + stdCap);
            txtPreCap.Text += (" " + preCap);
            Console.WriteLine(greater);

            // setting up UI (Binding)
            data.ItemsSource = Bookings;
            txtTrainName.Text = Train.Name;
            txtTrainOrigin.Text = Train.Origin;
            txtTrainDest.Text = Train.Destination;
            txtTime.Text = Train.Time;
            txtDate.Text = Train.Date;

            uIOrigin.Content = Train.Origin;
            uIDestination.Content = Train.Destination;
            uIStop1.Content = Train.Stops[0];
            uIStop2.Content = Train.Stops[1];

            // Setting Up Dynamic UI 
            if (Train.Stops[0] == "")
            {
                uIStop1.Height = 0;
                uIStop1.Visibility = Visibility.Collapsed;
                bdMiddle.Visibility = Visibility.Collapsed;
            }
            if (Train.Stops[1] == "")
            {
                uIStop2.Height = 0;
                bdMiddle.Visibility = Visibility.Collapsed;
                uIStop2.Visibility = Visibility.Collapsed;
            }

            if (Train.Stops[0] == "" && Train.Stops[1] == "")
            {
                bdLowest.Height = 0;
                bdLowest.Visibility = Visibility.Collapsed;

            }

            if (Bookings.Count == 0)
            {
                bdIfNoBooking.Visibility = Visibility.Visible;
            }
            else
            {
                bdIfNoBooking.Visibility = Visibility.Collapsed;
            }



        }

        /// <summary>
        /// Called When text is changed for txtOrgin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOrigin_TextChanged(object sender, TextChangedEventArgs e)
        {
            // This method limits which text is displayed
            string text = txtOrigin.Text;
            if (text.StartsWith("E") || text.StartsWith("e") || text.StartsWith("w") || text.StartsWith("W"))
            {

                // If e or w set to waverly
                txtOrigin.Text = "Edinburgh Waverley";
                // check if a departure station
                if (!AddStart.Contains(txtOrigin.Text) && txtOrigin.Text != "")
                {
                    MessageBox.Show("Please enter a valid departure station");
                    txtOrigin.Text = "";
                }
            }
            else if (text.StartsWith("L") || text.StartsWith("l") || text.StartsWith("k") || text.StartsWith("K"))
            {
                // If l or k set to king's cross
                txtOrigin.Text = "King's Cross";
                // check if a departure station
                if (!AddStart.Contains(txtOrigin.Text) && txtOrigin.Text != "")
                {
                    MessageBox.Show("Please enter a valid departure station");
                    txtOrigin.Text = "";

                }
            }
            else if (text.StartsWith("N") || text.StartsWith("n"))
            {
                // If n set Newcastle
                txtOrigin.Text = "Newcastle";
                // check if a departure station
                if (!AddStart.Contains(txtOrigin.Text) && txtOrigin.Text != "")
                {
                    MessageBox.Show("Please enter a valid departure station");
                    txtOrigin.Text = "";

                }
            }
            else if (text.StartsWith("Y") || text.StartsWith("y"))
            {
                // If y set York
                txtOrigin.Text = "York";
                // check if a departure station
                if (!AddStart.Contains(txtOrigin.Text) && txtOrigin.Text != "")
                {
                    MessageBox.Show("Please enter a valid departure station");
                    txtOrigin.Text = "";

                }
            }
            else
            {
                // default
                if (text != "")
                    MessageBox.Show("Please enter a valid station the train goes to");
                txtOrigin.Text = "";

            }



            #region resetting
            // Resetting Reservations
            seats = new List<int>();
            txtBtnReserveSeat.Text = "Reserve seats";

            meals = false;
            txtReserveMeal.Text = "Reserve Meal";
            #endregion
        }

        /// <summary>
        /// Called When text is changed for txtDest
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDest_TextChanged(object sender, TextChangedEventArgs e)
        {
            // If e or w set to waverly
            string text = txtDest.Text;
            if (text.StartsWith("E") || text.StartsWith("e") || text.StartsWith("w") || text.StartsWith("W"))
            {
                // If e or w set to waverly
                txtDest.Text = "Edinburgh Waverley";
                // check if a arrival station
                if (!AddEnd.Contains(txtDest.Text) && txtDest.Text != "")
                {
                    MessageBox.Show("Please enter a valid arrival station");
                    txtDest.Text = "";

                }
            }
            else if (text.StartsWith("L") || text.StartsWith("l") || text.StartsWith("k") || text.StartsWith("K") )
            {
                // If l or k set to king's cross
                txtDest.Text = "King's Cross";
                // check if a arrival station
                if (!AddEnd.Contains(txtDest.Text) && txtDest.Text != "")
                {
                    MessageBox.Show("Please enter a valid arrival station");
                    txtDest.Text = "";

                }
            }
            else if (text.StartsWith("N") || text.StartsWith("n"))
            {
                // If n set Newcastle
                txtDest.Text = "Newcastle";
                // check if a arrival station
                if (!AddEnd.Contains(txtDest.Text) && txtDest.Text != "")
                {
                    MessageBox.Show("Please enter a valid arrival station");
                    txtDest.Text = "";

                }
            }
            else if (text.StartsWith("Y") || text.StartsWith("y"))
            {
                // If y set York
                txtDest.Text = "York";
                // check if a arrival station
                if (!AddEnd.Contains(txtDest.Text) && txtDest.Text != "")
                {
                    MessageBox.Show("Please enter a valid arrival station");
                    txtDest.Text = "";

                }
            }
            else
            {
                // default
                if (text != "")
                    MessageBox.Show("Please enter a valid station the train goes to");
                txtDest.Text = "";

            }


            #region resetting
            // resetting reservations
            seats = new List<int>();
            txtBtnReserveSeat.Text = "Reserve seats";

            meals = false;
            txtReserveMeal.Text = "Reserve Meal";
            #endregion
        }
        /// <summary>
        /// Called when a key is hit from keyboard when txtTickets is in Focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// This method is to stop people from entering words, numbers only field
        private void txtTickets_KeyDown(object sender, KeyEventArgs e)
        {
            // if key press is not a number it is not going to be displayed
            if (e.Key != Key.D1 && e.Key != Key.D0 && e.Key != Key.D2 && e.Key != Key.D3 &&
                e.Key != Key.D4 && e.Key != Key.D5 && e.Key != Key.D6 && e.Key != Key.D7 && e.Key != Key.D8 && e.Key != Key.D9)
            {
                // event handled so that it will not be displayed 
                e.Handled = true;
            }
        }

        /// <summary>
        /// Validate the credentials to add into Booking
        /// </summary>
        /// <returns></returns>
        // This method is to validate the date
        private bool validate()
        {
           
            string origin = txtOrigin.Text;
            string dest = txtDest.Text;
            int tickets = 0;
            try
            {
                tickets = txtTickets.Text != "" ? Convert.ToInt32(txtTickets.Text) : 1; // ticket number
            }
            catch
            {
                tickets = 1;
            }
            int Precap = Train.PreCap;
            int Stdcap = Train.StdCap;

            // Getting Cities value
            Cities arrival = Helpers.GenerateCity(txtDest.Text);
            Cities depart = Helpers.GenerateCity(txtOrigin.Text);

           

            if (!(AddStart.Contains(origin) && AddEnd.Contains(dest)) )
            {
                // Handle if Origin or Destination is wrong
                MessageBox.Show("Invalid Origin and or Destination");
                return false;
            }

            if (
                ((Train.PreCap - Train.GetTicketNumber(classes, trainId, origin, dest) < tickets) && this.classes == "First Class")
                || ((Train.StdCap - Train.GetTicketNumber(classes, trainId, origin, dest) < tickets) && this.classes == "Standard Class")
                 )
            {
                // Handle if ticket number is wrong or not acceptable.
                MessageBox.Show("Not Enough Space on the train");
                return false;

            }

            if ( (this.classes == "Standard Class" && tickets > Stdcap || (tickets > Precap && this.classes == "First Class")
                ))
            {
                // Handle Limit 
                MessageBox.Show("Maximum Limit Exceeded");
                return false;

            }

            if (((greater && arrival < depart) || (!greater && depart < arrival)))
            {
                // If Departure to Origin is wrong
                MessageBox.Show("Train Cannot Go Backwards");
                return false;

            }

            if (arrival == depart)
            {
                // Departure should not be Arrival
                MessageBox.Show("Don't pay for going nowhere ;)");
                return false;
            }

            return true;
        }

      
        /// <summary>
        /// Add Booking into the Booking database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBooking(object sender, RoutedEventArgs e)
        {
            if (validate())
            {
                // If Validation Succeeds
                BookingFactory factory = new BookingFactory(txtClass.Text);
                
                Booking booking = factory.Book(trainId, txtOrigin, txtDest, txtTickets, meals, Train ,seats);
                // Adding to database
                Database.AddBooking(booking);
                // Show Ticket/Booking Manifest
                TicketWindow ticketWindow = new TicketWindow(booking, Train);
                ticketWindow.ShowDialog();

                //If succeed Reload DataContext()

                this.Bookings = Train.GetBookings(trainId);
                data.ItemsSource = Bookings;

                if (Bookings.Count > 0)
                {
                    // set Default UI to invisible if we have Count
                    bdIfNoBooking.Visibility = Visibility.Collapsed;
                }

                //((OpenTrainViewModel)this.DataContext).Reload(this, trainId);

            }
            #region resetting
            //Resetting
            txtOrigin.Text = "";
            txtDest.Text = "";
            txtTickets.Text = "";

            this.seats = new List<int>();
            txtBtnReserveSeat.Text = "Reserve Seats";
            txtReserveMeal.Text = "Reserve Meals";
            this.meals = false;
            #endregion
        }

        /// <summary>
        /// Change class from First to Standard or Vice Versa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnChangeClass(object sender, RoutedEventArgs e)
        {
            // if standard class set to first class vice versa
            this.classes = classes == "Standard Class" ? "First Class" : "Standard Class";
            txtClass.Text = (string)(txtClass.Text) == "Standard Class" ? "First Class" : "Standard Class";
            // change display to upgrade or downgrade
            ((Button)sender).Content = (string)((Button)sender).Content == "Upgrade Ticket" ? "Downgrade Ticket" : "Upgrade Ticket";
            btnReserveMeal.IsEnabled = btnReserveMeal.IsEnabled == false ? true : false;

            #region resetting
            // Resetting Reservations if you change class
            this.seats = new List<int>();
            txtBtnReserveSeat.Text = "Reserve Seats";

            this.meals = false;
            txtReserveMeal.Text = "Reserve Meals";
            #endregion
        }

        /// <summary>
        /// Click Reserve Meal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReserveMeal_Click(object sender, RoutedEventArgs e)
        {
            string text = txtTickets.Text;
            if (text == "")
            {
                // If tickets are not set yet return and end
                MessageBox.Show("No Tickets");
                return;
            }
            else
            {
                // If Yes Not un-reserve otherwise reserve
                if (!txtReserveMeal.Text.Contains("✓"))
                {    
                    meals = true;
                    txtReserveMeal.Text += "✓";
                }
                else
                {
                    meals = false;
                    txtReserveMeal.Text= txtReserveMeal.Text.Replace("✓", "");
                }
            }

        }

        /// <summary>
        /// Opens up a window to reserve seats if available
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void reserveSeat(object sender, RoutedEventArgs e)
        {
            // Get seat position of the same class (Coach-like in this assumption 1-n of both classes)
            ObservableCollection<int> taken = Train.SeatPositions(classes, trainId, txtOrigin.Text, txtDest.Text);
            
            if (txtOrigin.Text == "" || txtDest.Text == "" || txtTickets.Text == "" )
            {
                // if data is not perfect
                MessageBox.Show("Not Enough Data");
                return;
            }

            if (classes == "Standard Class")
            {
                // if standard class Open up Reservation Window
                try
                {
                    int tickets = Convert.ToInt32(txtTickets.Text);

                    if (txtDest.Text != txtOrigin.Text)
                    {
                        ReserveWindow rs = new ReserveWindow(this.seats, Train.StdCap, taken, tickets, classes);
                        rs.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Departure and Arrival cannot be same to reserve");
                        txtDest.Text = ""; // resetting destination
                        txtOrigin.Text = ""; // resetting origin
                    }
                }
                catch
                {
                    // Do Nothing
                }
            }

            else
            {
                // if first class Open up Reservation Window
                try
                {
                    int tickets = Convert.ToInt32(txtTickets.Text);
                    ReserveWindow rs = new ReserveWindow(this.seats, Train.PreCap, taken, tickets, classes);
                    rs.ShowDialog();
                }
                catch
                {
                    // Do Nothing
                }
            }
        }

        /// <summary>
        /// Called when text in txtTickets is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTickets_TextChanged(object sender, TextChangedEventArgs e)
        {
            // if the ticket number is changed seat reservation should be reset. This is only fair. 

            try 
            {
                int current = Convert.ToInt32(txtTickets.Text);
            }
            catch
            {
                txtTickets.Text = "";
            }

            txtBtnReserveSeat.Text = "Reserve seats";
            this.seats = new List<int>(); // Reseting
        }

        /// <summary>
        /// Opens a Booking manifest
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenManifest(object sender, RoutedEventArgs e)
        {
            // Get the current button to getcontext
            Button button = (Button)sender;
            Booking booking = (Booking) button.DataContext;
            
            // Open up a window to display ticket
            TicketWindow ticketWindow = new TicketWindow(booking, Train);
            ticketWindow.ShowDialog();
        }
        
        /// <summary>
        /// Called when txtDest key is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDest_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.E || e.Key == Key.K || e.Key == Key.N || e.Key == Key.Y || e.Key == Key.W || e.Key == Key.L)
            {
                // if these keys are pressed then fine
                txtDest.Text = "";
            }
            else if (e.Key == Key.Tab)
            {
                //Do Nothing to allow going to next Widget
            }
            else
            {
                // else handle
                e.Handled = true;
            }

        }

        /// <summary>
        /// txtOrigin Key pressed, then this will be fired
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOrigin_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.E || e.Key == Key.K || e.Key == Key.N || e.Key == Key.Y || e.Key == Key.W || e.Key == Key.L)
            {
                // if these keys are pressed, then fine
                txtOrigin.Text = "";
            }
            else if (e.Key == Key.Tab)
            {
                //Do Nothing to allow going to next Widget
            }
            else
            {
                // else handle

                e.Handled = true;
            }
        }
        #endregion
    }
}
