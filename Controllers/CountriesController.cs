using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace WorldBankWebClientApplication.Controllers
{
    public class CountriesController : Controller
    {
        // GET: Countries
        public ActionResult List()
        {
            return View();
        }
        public async Task<ActionResult> List1()
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://api.worldbank.org/countries");
            var data = await response.Content.ReadAsStringAsync();
            XDocument doc = XDocument.Parse(data);
            XNamespace ns = "http://www.worldbank.org";
            var names = from n in doc.Descendants(ns + "country")
                        select n.Element(ns + "name").Value;
            return PartialView(names.ToList<string>());
        }
    }
}