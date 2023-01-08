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
    /// First Class : Booking
    /// Author : Zin Lin Htun
    /// Date Modified : 18/11/22
    /// </summary>
    public class FirstClass : Booking
    {
        #region public

        public string Type { get; set; }
        public int Meals { get; set; }

        #endregion
        /// <summary>
        /// Firstclass
        /// </summary>
        /// <param name="trainId"></param>
        /// <param name="departure"></param>
        /// <param name="arrival"></param>
        /// <param name="tickets"></param>
        /// <param name="reserved"></param>
        public FirstClass(string trainId, string departure, string arrival, int tickets, List<int> reserved) :
            base(trainId, departure, arrival, tickets, reserved)
        {
            Type = "First Class";
            Meals = 0;
        }

        /// <summary>
        /// Firstclass explicit id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trainId"></param>
        /// <param name="departure"></param>
        /// <param name="arrival"></param>
        /// <param name="tickets"></param>
        /// <param name="reserved"></param>
        public FirstClass(string id,string trainId, string departure, string arrival, int tickets, List<int> reserved) : 
            base(id,trainId, departure, arrival, tickets, reserved)
        {
            Type = "First Class";
            Meals = 0;
        }
        #region methods
        /// <summary>
        /// Add itself to database
        /// </summary>
        public override void AddToDatabase ()
        {
            Database.AddBooking(this);
        }
        #endregion
    }
}
