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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TrainApp.BusinessLogic;
#endregion

namespace TrainApp
{
    /// <summary>
    /// Interaction logic for TicketWindow.xaml
    /// Author : Zin Lin Htun
    /// Date Modified : 18/11/22
    /// </summary>
    

    public partial class TicketWindow : Window
    {

        #region public attributes
        public Booking Booking { get; set; }
        public Train Train { get; set; }
        #endregion

        #region constructor
        /// <summary>
        /// TicketWindow Constructor
        /// </summary>
        /// <param name="booking"></param>
        /// <param name="train"></param>
        public TicketWindow(Booking booking, Train train)
        {
            InitializeComponent();
            // set booking and train
            Booking = booking;
            Train = train;

        }
        #endregion

        /// <summary>
        /// MouseDown for toucher
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void toucher_MouseDown(object sender, MouseButtonEventArgs e)
        {
            #region animation
            DoubleAnimation d = new DoubleAnimation();
            d.From = 1.0;
            d.To = 0.0;
            d.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            this.BeginAnimation(OpacityProperty, d);
            await Task.Delay(200);
            #endregion
            // Close Window
            this.Close();
        }

        /// <summary>
        /// When Loading Up the Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// Assumption - Meal reservation price is not given in the specs thus, have decided to use 5 pounds
        /// otherwise it doesn't make much sense
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // set up variables
            int total = 0;
            int mealsCount;
            int ticketCount = 0;
            int reserveCount = 0;
            int classPrice = 0;
            int basePrice = 20;

            // get base price
            if ((Booking.Departure == "Edinburgh Waverley" && Booking.Arrival == "King's Cross") || (Booking.Departure == "King's Cross" && Booking.Arrival == "Edinburgh Waverley")) 
            {
                basePrice = 40;
            }
            // get train and booking attributes
            txtTrainName.Text = Train.Name;
            txtReference.Text = Booking.Id;
            txtOrigin.Text = Booking.Departure;
            txtDest.Text = Booking.Arrival;
            txtTickets.Text += Booking.Tickets.ToString();
            ticketCount = Booking.Tickets;
            string seats = "";
            // for all seats
            foreach (int seat in Booking.Reserved)
            {
                seats+= seat + ", ";
                reserveCount++;
            }
            // set up seats
            txtSeats.Text = seats == "" ? "None" : seats;
            // cut off , 
            txtSeats.Text = txtSeats.Text.Length > 2 && txtSeats.Text == seats? txtSeats.Text.Substring(0, txtSeats.Text.Length - 2) : txtSeats.Text;
            // Simple Factory
            if (Booking is StandardClass)
            {
                txtClass.Text += " Standard/Economy Class";
                txtMeals.Text = "Not Allowed";
                mealsCount = 0;
                classPrice = 0;
            }

            else
            {
                txtClass.Text += " Premium/First Class";
                txtMeals.Text = ((FirstClass)Booking).Meals.ToString();
                mealsCount = ((FirstClass)Booking).Meals;
                classPrice = 10;
            }
            // calculate total price
            total = (basePrice * ticketCount) + (classPrice * ticketCount) + (reserveCount * 5) + (mealsCount*5);
            // to string
            txtTotal.Text = total.ToString();

        }
    }
}
