using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using static WebApplication4.Models.Location;

namespace WebApplication4.Models
{
    public class InfoModel
    {
        private static InfoModel s_instace = null;
        private bool toCreate;
        private Queue<string> locations;

        public static InfoModel Instance
        {
            get
            {
                if (s_instace == null)
                {
                    s_instace = new InfoModel();
                    s_instace.toCreate = true;
                    s_instace.locations = new Queue<string>();
                }
                return s_instace;
            }
        }


        public const string SCENARIO_FILE = "~/App_Data/{0}.txt";

        public void ReadData(string fileName)
        {
            string path = HttpContext.Current.Server.MapPath(String.Format(SCENARIO_FILE, fileName));
            if (File.Exists(path))
            {
                string[] lines = System.IO.File.ReadAllLines(path);        // reading all the lines of the file
                
                for(int i=0; i < lines.Length; i++)
                {
                    locations.Enqueue(lines[i]);
                }
            }
        }

        public void SaveLocations(FlightSample flightSample, string fileName, bool toCreateFile)
        {
            string location = LocationsToString(flightSample);
            string path = HttpContext.Current.Server.MapPath(String.Format(SCENARIO_FILE, fileName));
            if (toCreateFile)
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
            {
                    file.WriteLine(location);
            }

        }

        public string LocationsToString(FlightSample flightSample)
        {
                string location = flightSample.location.longtitude.ToString() + "," +
                    flightSample.location.latitude.ToString() + "," +
                    flightSample.altitude.ToString() + "," +
                    flightSample.direction.ToString() + "," +
                    flightSample.velocity.ToString();
            return location;
        }
        public string GetLocation()
        {
            return locations.Dequeue();
        }
        public int GetNumOfLocations()
        {
            return locations.Count();
        }
    }
}