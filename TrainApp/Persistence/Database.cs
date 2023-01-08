#region dependencies
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq.Expressions;
using TrainApp.BusinessLogic;
#endregion

namespace TrainApp.Persistence
{
    /// <summary>
    /// This is the Database class - Persistence
    /// Author : Zin Lin Htun
    /// Date Modified : 18/11/22
    /// </summary>
    public class Database
    {
        #region methods
        /// <summary>
        /// GetLines : Get the id on the last line
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static int GetLines (string filename)
        {
            int max = 0;
            // get aboslute path
            string path =  AppContext.BaseDirectory.ToString() + filename;
            // open reader
            try
            {
                using (StreamReader reader = new StreamReader(@path))
                {
                    // For every line
                    while (!reader.EndOfStream)
                    {
                        // current line
                        string currentLine = reader.ReadLine()!;
                        if (currentLine != "")
                        {
                            // find the max id
                            int id = 0;
                            try
                            {
                                id = Convert.ToInt32(currentLine.Split(",")[0]);
                            }
                            catch
                            {
                                // Do nothing
                            }
                            if (id > max)
                                max = id;
                        }
                    }
                    reader.Close();
                }
            }
            catch
            {
                // DO NOTHING
            }

            // return max id
            return max;
        }

        /// <summary>
        /// GetDatabaseLineByLine - Get everyline from a database.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static List<string> GetDatabaseLineByLine(string filename)
        {
            // initiate lines
            List<string> lines = new List<string>();
            // abosolute path
            string path = @"" + AppContext.BaseDirectory.ToString() + filename;
            // open reader
            using (StreamReader reader = new StreamReader(@path))
            {
                // for every line
                while (!reader.EndOfStream)
                {
                    // current line
                    string currentLine = reader.ReadLine()!;
                    // if current line is empty don't count
                    if (currentLine == "")
                        continue;
                    else
                        lines.Add(currentLine); // else add to lines
                }
                reader.Close();
            }
            // return lines
            return lines;
        }

        /// <summary>
        /// GetTrains
        /// reading database files
        /// <param> string filename : database filename to read
        /// </summary>
        public static  ObservableCollection<Train> GetTrains()
        {
            // list
            ObservableCollection<Train> list = new ObservableCollection<Train>();
            // get abosolute path
            string path = @"" + AppContext.BaseDirectory.ToString()  +"trains.csv";

            try
            {
                // try open reader
                using (StreamReader reader = new StreamReader(@path))
                {
                    // for every line
                    while (!reader.EndOfStream)
                    {
                        // line is the currentline
                        string line;
                        line = reader.ReadLine()!;
                        if (line.Contains(","))
                        {
                            // split and get attributes for train
                            string[] attributes = line.Split(",");
                            string name = attributes[1];
                            string id = attributes[0];
                            string date = attributes[7];
                            string time = attributes[6];
                            string origin = attributes[4];
                            string destination = attributes[5];
                            int stdCapacity = Convert.ToInt32(attributes[2]);
                            int preCapacity = Convert.ToInt32(attributes[3]);
                            string[] stops = new string[2] { attributes[8], attributes[9] };

                            Train train = new Train(id, name, stdCapacity, preCapacity, origin, destination, time, date, stops);
                            // add new train to the list
                            list.Insert(0, train);
                        }
                    }
                    reader.Close();
                }
            }
            catch 
            { 
                // Do Nothing
            }
            // return list
            return list;

        }

        /// <summary>
        /// writeCSVDatabase
        /// writing database files
        /// <param> string filename : database filename to write
        /// </summary>
        public static void WriteCSVDatabase(string filename)
        {
            string path = AppContext.BaseDirectory.ToString();
            if (!File.Exists(@path  + @filename + ".csv"))
            {
                // Create a csv database file
                File.Create(@path+  @filename + ".csv");
            }
        }

        /// <summary>
        /// Get Current Ticket Numbers
        /// </summary>
        /// <param name="trainId"></param>
        /// <returns></returns>
        public static int GetTicketNumber(string classes, string trainId, string departureStation, string arrivalStation)
        {
            // new int 0
            int answer = 0;
            // get absolute path
            string path = AppContext.BaseDirectory.ToString() + @"bookings.csv"; // Booking database
            // greater defines if train is travelling from edinburgh to london or vice versa
            bool greater;
            // try open up reader
            try
            {
                using (StreamReader reader = new StreamReader(@path))
                {
                    while (!reader.EndOfStream)
                    {
                        string temp = reader.ReadLine()!; //temp = temporary
                        if (temp.Contains(","))
                        {

                            string[] attributes = temp.Split(","); // Separate by commas
                            string seats = "";
                            for (int i = 7; i < attributes.Length; i++)
                            {
                                seats += attributes[i] == "" ? "" : attributes[i] + " ";
                            }

                            Cities departure = Helpers.GenerateCity(departureStation);
                            Cities arrival = Helpers.GenerateCity(arrivalStation);
                            Cities currentArrival = Helpers.GenerateCity(attributes[3]);
                            Cities currentDeaprture = Helpers.GenerateCity(attributes[2]);
                            greater = departure < arrival ? true : false;
                            if (greater)
                            {
                                // From Edinburgh to Lodon Train
                                if (attributes[1].Equals(trainId) && attributes[5] == classes && (departure < currentArrival &&
                                    !(arrival <= currentDeaprture)))
                                {
                                    answer += Convert.ToInt32(attributes[4]);
                                }
                            }
                            else
                            {
                                // From London to Edinburgh Train
                                if (attributes[1].Equals(trainId) && attributes[5] == classes && (departure < currentArrival &&
                                    !(arrival >= currentDeaprture)))
                                {
                                    answer += Convert.ToInt32(attributes[4]);

                                }
                            }


                            // At This Point Mate, we got all the taken values
                        }
                    }
                }
            }
            catch
            {
                // Do Nothing
            }

            // return statement
            return answer;
        }

        /// <summary>
        /// AddTrain
        /// <param> Train train
        /// This is to add Train to Database
        /// </summary>
        public static async void AddTrain(Train train)
        {
            // get absolute path
            string path = AppContext.BaseDirectory.ToString() + @"trains.csv";
          
            // open writer
            using (StreamWriter writetext = File.AppendText(@path))
            {
                // get cities
                Cities originCity = Helpers.GenerateCity(train.Origin);
                Cities destCity = Helpers.GenerateCity(train.Destination);
                // if orgin is Edinburgh
                if (originCity < destCity)

                    await writetext.WriteLineAsync( train.Id + "," + train.Name + "," + train.StdCap.ToString() + ","
                    + train.PreCap.ToString() + "," + train.Origin + "," + train.Destination + "," + train.Date + ","
                     + train.Time + "," + train.Stops[0] + "," + train.Stops[1] + "\r\n");

                else
                    await writetext.WriteLineAsync(train.Id + "," + train.Name + "," + train.StdCap.ToString() + ","
                    + train.PreCap.ToString() + "," + train.Origin + "," + train.Destination + "," + train.Date + ","
                     + train.Time + "," + train.Stops[1] + "," + train.Stops[0] + "\r\n");

                writetext.Close();

            }

           
        }

        /// <summary>
        /// Reset The Train Database
        /// </summary>
        public static void ResetTrain()
        {
            // get absolute path
            string path = AppContext.BaseDirectory.ToString() + "trains.csv";
            // open writer
            using (StreamWriter writetext = new StreamWriter(@path))
            {
                //write empty set
                writetext.WriteAsync("");
                writetext.Close();
            }
        }

        /// <summary>
        /// Reset the Train Database
        /// </summary>
        public static void ResetBooking()
        {
            // get absolute path
            string path = AppContext.BaseDirectory.ToString() + "bookings.csv";
            using (StreamWriter writetext = new StreamWriter(@path))
            {
                // write empty set 
                writetext.WriteAsync("");
                writetext.Close();

            }
        }

        /// <summary>
        /// Get a Single Train from the Database csv file
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Train GetTrain(string id)
        {
            // train
            Train train = new Train();
            // get absolute path
            string path = AppContext.BaseDirectory.ToString() + "trains.csv";
            string value = "";
            
            try
            {
                // open reader
                using (StreamReader reader = new StreamReader(@path))
                {
                    // for every line
                    while (!reader.EndOfStream)
                    {
                        // temporary var line thus temp.
                        // temp is the current line
                        string temp = reader.ReadLine()!;
                        // value
                        value = (temp.StartsWith(id)) ? temp! : "";
                        if (value.Length > 0)
                            break;
                    }
                    reader.Close();
                }
                // check if value contains commas.
                if (value.Contains(","))
                {
                    // get all attributes for a train with spliting
                    string[] attributes = value.Split(",");
                    string name = attributes[1];
                    string tid = attributes[0];
                    string date = attributes[7];
                    string time = attributes[6];
                    string origin = attributes[4];
                    string destination = attributes[5];
                    int stdCapacity = Convert.ToInt32(attributes[2]);
                    int preCapacity = Convert.ToInt32(attributes[3]);
                    string[] stops = new string[2] { attributes[8], attributes[9] };

                    // new train
                    train = new Train(tid, name, stdCapacity, preCapacity, origin, destination, time, date, stops);
                }
                else
                {
                    // Empty
                    train = new Train(); //Empty
                }
            }
            catch
            {
                // Do Nothing
            }
            // return train
            return train;
        }

        /// <summary>
        /// Get the seat Positions to add, this can be use against the number so that customer don't choose the seats already taken
        /// </summary>
        /// <param name="classes"></param>
        /// <param name="trainid"></param>
        /// <returns></returns>
        public static ObservableCollection<int> SeatPositions(string classes, string trainid, string departureStation, string arrivalStation)
        {
            // answer list
            ObservableCollection<int> answer = new ObservableCollection<int>();
            // greater if train goes from Edinburgh to London
            bool greater;

            // get absolute path
            string path = AppContext.BaseDirectory.ToString() + "bookings.csv";
            // values and linesNeeded
            List<string> values = new List<string>();
            List<string> linesNeeded = new List<string>();

            // open reader
            using (StreamReader reader = new StreamReader(@path))
            {
                // for every line
                while (!reader.EndOfStream)
                {
                    // temporary line from readline
                    string temp = reader.ReadLine()!; //temp = temporary
                    if (temp.Contains(","))
                    {

                        string[] lineAttributes = temp.Split(","); // Separate by commas
                        // add linesNeeded if the right train and the right class as customers of differecnt classes will be in different coaches
                        // Thus seating plans with same number can occur on different coaches.
                        if (lineAttributes[1] == trainid && lineAttributes[5] == classes)
                            linesNeeded.Add(temp);
                    }
                }
            }
            // foreach statement
            foreach (string line in linesNeeded)
            {
                // line split to get all necessary attributes
                string[] attributes = line.Split(",");
                string seats = "";
                
                // get cities
                Cities departure = Helpers.GenerateCity(departureStation);
                Cities arrival = Helpers.GenerateCity(arrivalStation);
                Cities currentArrival = Helpers.GenerateCity(attributes[3]);
                Cities currentDeaprture = Helpers.GenerateCity(attributes[2]);
                // set up greater here
                greater = departure < arrival ? true : false;
                if (greater)
                {
                    // From Edinburgh to Lodon Train
                    if ( (departure < currentArrival &&
                        !(arrival <= currentDeaprture)
                        ))
                    {
                        // add all seats
                        for (int i = 7; i < attributes.Length; i++)
                        {
                            seats += attributes[i] == "" ? "" : attributes[i] + " ";
                        }
                        foreach (string current in seats.Split(" "))
                        {
                            if (current != "" && !values.Contains(current))
                                values.Add(current);
                        }
                    }
                }
                else
                {
                    // From London to Edinburgh Train
                    if (  (departure > currentArrival
                        && !(arrival >= currentDeaprture)
                        )
                        )
                    {
                        // add all seats
                        for (int i = 7; i < attributes.Length; i++)
                        {
                            seats += attributes[i] == "" ? "" : attributes[i] + " ";
                        }
                        foreach (string current in seats.Split(" "))
                        {
                            if (current != "" && !values.Contains(current))
                                values.Add(current);
                        }


                    }
                }

                // for each of them make the seat numbers instead of strings
                foreach (string str in values)
                {
                    try
                    {
                        int current = Convert.ToInt32(str);
                        answer.Add(current);
                    }
                    catch
                    {
                        // DO NOTHING
                    }
                }
            }
            // return answer;
            return answer;
        }

        /// <summary>
        /// Get Bookings for a specific train
        /// </summary>
        /// <param name="trainid"></param>
        /// <returns></returns>
        public static ObservableCollection<Booking> GetBookings (string trainid)
        {
            ObservableCollection<Booking> list = new ObservableCollection<Booking>();
            string path =  AppContext.BaseDirectory.ToString() + "bookings.csv";

            try
            {
                // open up reader
                using (StreamReader reader = new StreamReader(@path))
                {
                    // for every line
                    while (!reader.EndOfStream)
                    {
                        string line;
                        line = reader.ReadLine()!; // readline
                        if (line.Contains(","))
                        {

                            string[] attributes = line.Split(",");
                            if (attributes[1] == trainid)
                            {
                                // get all attributes by Spliting the csv line value
                                string id = attributes[0];
                                string origin = attributes[2];
                                string destination = attributes[3];
                                int tickets = Convert.ToInt32(attributes[4]);
                                string classes = attributes[5];
                                int meals = Convert.ToInt32(attributes[6]);
                                #region Not Necessary 
                                List<int> reserves = new List<int>();

                                if (attributes[7] != "")
                                {
                                    for (int count = 7; count < attributes.Length; count++)
                                    {
                                        int current = Convert.ToInt32(attributes[count]);
                                        reserves.Add(current);
                                    }
                                }

                                #endregion

                                // if of type standard
                                if (classes == "Standard Class")
                                {
                                    // Factory like architecture
                                    // but not factory
                                    // this is to demonstrate simple factory, factory is demostrated on parts of the train database
                                    StandardClass booking = new StandardClass(id, trainid, origin, destination, tickets, reserves);
                                    list.Insert(0, booking);
                                }
                                // first or premium class
                                else if (classes == "First Class")
                                {
                                    FirstClass booking = new FirstClass(id, trainid, origin, destination, tickets, reserves);
                                    booking.Meals = meals;
                                    list.Insert(0, booking);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                // Do Nothing
            }
            // returning
            return list;
        }

        /// <summary>
        /// add new Booking into the Database
        /// </summary>
        /// <param name="booking"></param>
        public static async void AddBooking (Booking booking)
        {
            // get absolute path
            string path = AppContext.BaseDirectory.ToString() + @"bookings.csv";
            // seats
            string seats = "";
            // get reserved seats
            foreach (int i in booking.Reserved)
            {
                seats += i.ToString() + ",";
            }

            seats = seats != "" ? seats.Substring(0, seats.Length - 1) : seats; // Getting rid of the last ,
            // open up writer
            using (StreamWriter writetext = File.AppendText(@path))
            {
                // Simple factory 
                if (booking is FirstClass)
                {
                    await writetext.WriteLineAsync(booking.Id + "," + booking.TrainId + "," + booking.Departure + "," + booking.Arrival + ","
                    + booking.Tickets.ToString() + "," + "First Class" + "," + ((FirstClass)booking).Meals  + "," + seats + "\r\n");
                }
                else
                {
                    await writetext.WriteLineAsync(booking.Id +"," + booking.TrainId + "," + booking.Departure + "," + booking.Arrival + ","
                    + booking.Tickets.ToString() + "," + "Standard Class"+ ",0," + seats + "\r\n");
                }
                // closing stream writer
                writetext.Close();
            }
        }
        #endregion

    }
}


