#region dependencies
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainApp.Persistence;
#endregion

namespace TrainApp.BusinessLogic
{
    /// <summary>
    /// This is a Booking class
    /// Author : Zin Lin Htun
    /// </summary>
    public class Booking
    {
        #region private attributes
        private string id;
        private string trainId;
        private string departure;
        private string arrival;
        private int tickets;
        private List<int> reserved;

        #endregion

        #region public attributes

       public string Id { get { return id; } set { id = value; } }
        public string TrainId
        { get { return trainId; } set { trainId = value; } }

        public string Departure
        { get { return departure; } set { departure = value; } }

        public string Arrival
        { get { return arrival; } set { arrival = value; } }

        public int Tickets
        { get { return tickets; } set { tickets = value; } }

        public List<int> Reserved
        { get { return reserved; } set { reserved = value; } }

        #endregion

        #region constructors

        

        /// <summary>
        /// Booking 
        /// </summary>
        /// <param name="trainId"></param>
        /// <param name="departure"></param>
        /// <param name="arrival"></param>
        /// <param name="tickets"></param>
        /// <param name="reserved"></param>
        public Booking (string trainId, string departure, string arrival, int tickets, List<int> reserved)
        {
            // set values
            this.id = (DateTime.Now.ToString() + DateTime.Now.Millisecond.ToString()).Replace(" ", "").Replace("/", "").Replace(":", "");
            this.trainId = trainId;
            this.tickets = tickets;
            this.departure = departure;
            this.arrival = arrival;
            this.reserved = reserved;
        }

        /// <summary>
        /// Booking with explicit id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trainId"></param>
        /// <param name="departure"></param>
        /// <param name="arrival"></param>
        /// <param name="tickets"></param>
        /// <param name="reserved"></param>

        public Booking(string id, string trainId, string departure, string arrival, int tickets, List<int> reserved)
        {
            // set values
            this.id = id;
            this.trainId = trainId;
            this.tickets = tickets;
            this.departure = departure;
            this.arrival = arrival;
            this.reserved = reserved;
        }

        #endregion

        #region methods
        /// <summary>
        /// Add itself to database
        /// </summary>
        public virtual void AddToDatabase()
        {
            Database.AddBooking(this);
        }

        /// <summary>
        /// Reset Booking Database
        /// </summary>
        public static void ResetDatabase()
        {
            Database.ResetBooking();
        }

        /// <summary>
        /// Build Database if not exist
        /// </summary>
        public static void ConnectDatabase()
        {
            Database.WriteCSVDatabase("bookings");
        }

        #endregion
    }
}
