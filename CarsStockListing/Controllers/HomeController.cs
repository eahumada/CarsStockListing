using CarsStockListing.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebGrease.Activities;

namespace CarsStockListing.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async Task<ActionResult> StockListing()
        {

            StockListingResult stockItems = null;

            try
            {
                var url = ConfigurationManager.AppSettings["CarSales.CarStockListing.Url"];

                stockItems = await GetStockItems(url);

            }
            catch (Exception ex)
            {
                ErrorHandling(ex);

                throw;
            }

            return View(stockItems);
        }

        private async Task<StockListingResult> GetStockItems(string url)
        {
            StockListingResult stockItems;
            var uri = new Uri(url);

            ViewBag.Message = "CarSales Stock Listing Details";

            using (WebClient client = new WebClient())
            {
                string result = (await client.DownloadStringTaskAsync(uri));

                stockItems = new JavaScriptSerializer().Deserialize<StockListingResult>(result);
            }

            return stockItems;
        }

        private void ErrorHandling(Exception ex) 
        {
            // Todo: Error Handling, logging and reporting (ej. Async error handling)
        }
    }
}