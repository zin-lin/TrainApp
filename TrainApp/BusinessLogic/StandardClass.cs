using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainApp.Persistence;

// Author : Zin Lin Htun
// StandardClass : Booking

namespace TrainApp.BusinessLogic
{
    public class StandardClass : Booking
    {
        #region public
        public string Type { get; set; }
        #endregion

        /// <summary>
        /// StandardClass
        /// </summary>
        /// <param name="trainId"></param>
        /// <param name="departure"></param>
        /// <param name="arrival"></param>
        /// <param name="tickets"></param>
        /// <param name="reserved"></param>
        public StandardClass(string trainId, string departure, string arrival, int tickets, List<int> reserved) :
            base(trainId, departure, arrival, tickets, reserved)
        {
            this.Type = "Standard Class";
        }

        /// <summary>
        /// StandardClass explicit id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trainId"></param>
        /// <param name="departure"></param>
        /// <param name="arrival"></param>
        /// <param name="tickets"></param>
        /// <param name="reserved"></param>
        public StandardClass(string id,string trainId, string departure, string arrival, int tickets, List<int> reserved) :
         base(id ,trainId, departure, arrival, tickets, reserved)
        {
            this.Type = "Standard Class";
        }

        #region methods
        /// <summary>
        /// Add itself to database
        /// </summary>
        public override void AddToDatabase()
        {
            Database.AddBooking(this);
        }
        #endregion
    }
}
