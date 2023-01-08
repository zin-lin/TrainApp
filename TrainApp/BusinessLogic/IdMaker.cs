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
    /// SINGLETON
    /// Author : Zin Lin Htun
    /// Matric : 40542237
    /// Date Modified : 18/ 11/ 22
    /// </summary>
    public class IdMaker
    {
        #region private
        private static int id;
        
        private static IdMaker? instance = new IdMaker();
        #endregion

        #region methods
        /// <summary>
        /// Get Instance, increment everytime
        /// </summary>
        public static IdMaker getInstance ()
        {
            // automatically increment
            // this is to ensure when database reset happens id should be 1
            id = Database.GetLines("trains.csv") + 1;
            return instance!;
        }

        /// <summary>
        /// Get the Id
        /// </summary>
        /// <returns></returns>
        public int getId ()
        {
            return id;
        }
        #endregion


        /// <summary>
        /// private
        /// So that No one can basically use it
        /// </summary>
        private IdMaker ()
        {
            // do nothing basically just set up int
            id =  0; 
        }

        
    }
}
