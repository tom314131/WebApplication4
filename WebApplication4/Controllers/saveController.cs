using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
using static WebApplication4.Models.Location;

namespace WebApplication4.Controllers
{
    public class saveController : Controller
    {
        InfoModel infoModel;
        
        Random rnd;
        // GET: save
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Save(string ip, int port, int rate, int time, string name)
        {
            Session["rate"] = rate;
            Session["iterations"] = rate * time;
            ViewBag.fileName = name;
            return View();
        }

        public FlightSample CreateFlightSample()
        {
            rnd = new Random();
            FlightSample flightSample = new FlightSample((double)rnd.Next(1, 50), (double)rnd.Next(1, 50));
            flightSample.altitude = (float)rnd.Next(1,50);
            flightSample.direction = (float)rnd.Next(1, 50);
            flightSample.velocity = (float)rnd.Next(1, 50);
            return flightSample;
        }

        public void GetLocation(string fileName, bool toCreateFile)
        {
            FlightSample flightSample = CreateFlightSample();
            SaveLocation(flightSample, fileName, toCreateFile);

        }

        public void SaveLocation(FlightSample flightSample, string fileName, bool toCreateFile)
        {
            infoModel = InfoModel.Instance;
            infoModel.SaveLocations(flightSample,fileName, toCreateFile);
        }
    }
}