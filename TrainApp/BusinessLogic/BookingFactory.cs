#region dependencies
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TrainApp.Persistence;
#endregion

namespace TrainApp.BusinessLogic
{
    /// <summary>
    /// Factory Pattern Class
    /// Author : Zin Lin Htun
    /// Date : 15/ 11/ 22
    /// </summary>
    public class BookingFactory
    {
        #region public
        public string Classes { get; set; }

        /// <summary>
        /// Public Book, book bookings, from iput type create different instances
        /// </summary>
        /// <param name="trainId"></param>
        /// <param name="txtOrigin"></param>
        /// <param name="txtDest"></param>
        /// <param name="txtTickets"></param>
        /// <param name="txtClass"></param>
        /// <param name="meals"></param>
        /// <param name="train"></param>
        /// <param name="seats"></param>
        public Booking Book (string trainId, TextBox txtOrigin, TextBox txtDest, TextBox txtTickets, bool meals, Train train, List<int> seats)
        {
            Booking book;
            if (Classes == "Standard Class")
            {
                // If standard class add a standard class to database
                int tic = Convert.ToInt32(txtTickets.Text == "" ? 1 : txtTickets.Text);
                tic = tic > 0 ? tic : 1; // at least one ticket

                // new instance
                book = new StandardClass(trainId, txtOrigin.Text, txtDest.Text,tic, seats);
               
            }
            else
            {
                // If first class add a first class to database
                int tic = Convert.ToInt32(txtTickets.Text == "" ? 1 : txtTickets.Text);
                tic = tic > 0 ? tic : 1; // at least one ticket

                //new instance
                book = new FirstClass(trainId, txtOrigin.Text, txtDest.Text, tic, seats);
                if (meals)
                {   // If meal reserve
                    ((FirstClass)book).Meals = Convert.ToInt32(txtTickets.Text == "" ? 1 : txtTickets.Text);
                }
               
            }
            return book;
        }
        #endregion

        #region constructor
        /// <summary>
        /// Constructor taking in classes
        /// </summary>
        /// <param name="classes"></param>
        public BookingFactory(string classes)
        {
            this.Classes = classes;
        }
        #endregion
    }
}
