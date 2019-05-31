using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace WebApplication4.Models
{

    public class Location
    {
        public double longtitude { get; set; }
        public double latitude { get; set; }

        public Location(double lon, double lat)
        {
            this.longtitude = lon;
            this.latitude = lat;
        }

        public void ToXml(XmlWriter writer)
        {
            writer.WriteStartElement("Location");
            writer.WriteElementString("lon", this.longtitude.ToString());
            writer.WriteElementString("lat", this.latitude.ToString());
            writer.WriteEndElement();
        }

        public class FlightSample
        {
            public Location location { get; set; }
            public float altitude { get; set; }
            public float direction { get; set; }
            public float velocity { get; set; }
            public FlightSample(double longtitude, double latitude)
            {
                this.location = new Location(longtitude, latitude);
            }
        }

    }

  
}