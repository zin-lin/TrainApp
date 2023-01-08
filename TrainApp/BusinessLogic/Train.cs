#region dependencies
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainApp.Persistence;
#endregion

namespace TrainApp.BusinessLogic
{
    /// <summary>
    /// This is the class Train
    /// Author : Zin Lin Htun
    /// Date Modified : 18/11/22
    /// </summary>
    public class Train
    {
        #region private attributes
        private string id;
        private string name;
        private int stdCap;
        private int preCap;
        private string origin;
        private string destination;
        private string time;
        private string date;
        private string[] stops;

        #endregion

        #region public attributes

        public string Id
        { get { return id; } set { id = value; } }

        public string Name
        { get { return name; } set { name = value; } }

        public int StdCap
        { get { return stdCap; } set { stdCap = value; } }

        public int PreCap
        { get { return preCap; } set { preCap = value; } }

        public string Origin
        { get { return origin; } set { origin = value; } }

        public string Destination
        { get { return destination; } set { destination = value; } }

        public string Time
        { get { return time; } set { time = value; } }

        public string Date
        { get { return date; } set { date = value; } }

        public string[] Stops
        { get { return stops; } set { stops = value; } }

        #endregion

        #region Construtors
        /// <summary>
        /// Train with auto assignments
        /// </summary>
        public Train()
        {
            id = DateTime.Now.ToString();
            this.name = "Service 183";
            this.stdCap = 300;
            this.preCap = 100;
            this.date = "01/01/2023";
            this.time = "18:00";
            this.stops = new string[2] { "New Castle", "York" };
            this.origin = "Edinburgh";
            this.destination = "London";
        }

        /// <summary>
        /// Train explicits
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="stdCap"></param>
        /// <param name="preCap"></param>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <param name="time"></param>
        /// <param name="date"></param>
        /// <param name="stops"></param>
        /// <param name="seats"></param>
        public Train(string id, string name, int stdCap, int preCap, string origin, string destination, string time, string date, string[] stops)
        {
            this.id = id;
            this.name = name;
            this.stdCap = stdCap;
            this.preCap = preCap;
            this.origin = origin;
            this.destination = destination;
            this.time = time;
            this.date = date;
            this.stops = stops;

        }


        #endregion

        #region methods

        /// <summary>
        /// Add this train
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void AddToDatabase()
        {
            Database.AddTrain(this);
        }

        /// <summary>
        /// Get a train
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Train GetTrain(string id)
        {
            return Database.GetTrain(id);
        }

        /// <summary>
        /// Get Booking
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ObservableCollection<Booking> GetBookings (string id)
        {
            return Database.GetBookings(id);
        }

        /// <summary>
        /// Get Trains
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Train> GetTrains()
        {
            return Database.GetTrains();
        }

        /// <summary>
        /// Get How many tickets
        /// </summary>
        /// <param name="classes"></param>
        /// <param name="trainId"></param>
        /// <param name="departureStation"></param>
        /// <param name="arrivalStation"></param>
        /// <returns></returns>
        public static int GetTicketNumber(string classes, string trainId, string departureStation, string arrivalStation) 
        {
            return Database.GetTicketNumber(classes, trainId, departureStation, arrivalStation);
        }

        /// <summary>
        /// Get taken Position
        /// </summary>
        /// <param name="classes"></param>
        /// <param name="trainid"></param>
        /// <param name="departureStation"></param>
        /// <param name="arrivalStation"></param>
        /// <returns></returns>
        public static ObservableCollection<int> SeatPositions(string classes, string trainid, string departureStation, string arrivalStation)
        {
            return Database.SeatPositions(classes, trainid, departureStation, arrivalStation);  
        }

        /// <summary>
        /// Reset Train Database
        /// </summary>
        public static void ResetDatabase ()
        {
            Database.ResetTrain();
        }

        /// <summary>
        /// Build Database if not exist
        /// </summary>
        public static void ConnectDatabase ()
        {
            Database.WriteCSVDatabase("trains");
        }

        #endregion

    }
}
