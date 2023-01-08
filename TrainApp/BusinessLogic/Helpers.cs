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
    /// Public Interface Helpers
    /// help app
    /// Author: Zin Lin Htun
    /// Date Modified : 18/11/22
    /// </summary>
    public interface Helpers
    {
        /// <summary>
        /// Get Every Line from a CSV database of the path provided
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List <string> GetDatabaseLineByLine (string @path)
        {
            return Database.GetDatabaseLineByLine(@path);
        }

        /// <summary>
        /// Generate City
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public static Cities GenerateCity (string cityName)
        {
            if (cityName == "Edinburgh" || cityName == "Waverley" || cityName == "Edinburgh Waverley")
            {
                return Cities.Edinburgh;
            }

            else if (cityName == "York")
            {
                return Cities.York;
            }

            else if (cityName == "Newcastle")
            {
                return Cities.Newcastle;
            }

            else
            {
                return Cities.London;
            }
        }
    }
}
