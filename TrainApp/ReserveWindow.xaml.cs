#region dependencies
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using TrainApp.Persistence;
#endregion

namespace TrainApp
{
    /// <summary>
    /// Interaction logic for ReserveWindow.xaml
    /// Author : Zin Lin Htun
    /// Date Modified : 18/11/22
    /// </summary>
    public partial class ReserveWindow : Window
    {
        #region private

        private List<int> values;
        private int limit;
        private ObservableCollection<int> taken;
        private ObservableCollection<string> takenString;
        private int tickets;
        private int count;
        private string classes;
        #endregion

        #region constructor
        /// <summary>
        /// ReserveWindow Class constructor
        /// </summary>
        /// <param name="values"></param>
        /// <param name="limit"></param>
        /// <param name="taken"></param>
        /// <param name="tickets"></param>
        public ReserveWindow(List<int> values, int limit, ObservableCollection<int> taken, int tickets, string classes)
        {
            // with this values will have reference to the list sent
            InitializeComponent();
            this.values = values;
            this.limit = limit;
            this.taken = taken;
            this.count = 0;
            this.takenString = new ObservableCollection<string>();
            this.tickets = tickets;
            this.values.Clear();
            this.classes = classes;
            // get all the seats that are taken
            foreach (int i in taken)
            {
                this.takenString.Add(i.ToString());
            }


        }
        #endregion

        #region private
        /// <summary>
        /// touch mouse down
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
            // Close window
            this.Close();
        }

        /// <summary>
        /// Window Loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // seats
            ObservableCollection<int> seats = new ObservableCollection<int>();
            
            // get limits 
            for (int i = 1; i <= limit; i++)
            {
                // add if not taken
                if (!taken.Contains(i))
                    seats.Add(i);
            }
            // set itemsource
            title.Text += " " +classes;
            reserveList.ItemsSource = seats;
        }

        /// <summary>
        /// Button_Click when button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // get sender button
            Button thisButton = (Button)sender; // get the sender
            TextBlock text = ((TextBlock)(((StackPanel)thisButton.Content).Children[1])); // get textblock
            
            if (!text.Text.Contains("✓") && values.Count <tickets)
            {
                // add the one to values
                values.Add(Convert.ToInt32(text.Text));
                text.Text += "✓";
                count++;
            }
            else
            {
                // removing the one from values
                text.Text = text.Text.Replace("✓", "");

                values.Remove(Convert.ToInt32(text.Text));
                Console.WriteLine("Hello");
                count--;
            }
            
        }

        /// <summary>
        /// add_Click when button named add is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void add_Click(object sender, RoutedEventArgs e)
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

        #endregion
    }
}
