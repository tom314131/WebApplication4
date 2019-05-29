using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class DisplayerController : Controller
    {
        // GET: Default

        SimulatorClient client = null;

        public string Index()
        {
            return "index";
        }

        public ActionResult Display(string first ="127.0.0.1", int second=5400, int rate = 0)
        {
            IPAddress address;
            if (IPAddress.TryParse(first, out address))
            {
                return DisplayOnline(first, second, rate);
            }
            else
            {
                return DisplaySavedMap(first, second);
            }
        }

        ActionResult DisplayOnline(string ip, int port, int rate)
        {
            if (client == null)
            {
                //client = new SimulatorClient(ip, port);
            }

            ViewBag.rate = rate;
            return View("DisplayOnline");
        }

        ActionResult DisplaySavedMap(string name, int rate)
        {
            ViewBag.rate = rate;
            return View();
        }

        [HttpPost]
        public string QueryData(string query)
        {
            double[] data = client.GetData(query.Split(','));

            return ToXml(new Location(data[0], data[1]));
        }

        string ToXml(Location location)
        {
            //Initiate XML stuff
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            XmlWriter writer = XmlWriter.Create(sb, settings);

            writer.WriteStartDocument();
            location.ToXml(writer);
            writer.WriteEndDocument();
            writer.Flush();

            return sb.ToString();
        }
    }
}