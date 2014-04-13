using System;
using System.Web.Mvc;

namespace HomeRental.Controllers
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

        public ActionResult s(DateTime checkin, DateTime checkout, int guests, string id = null)
        {
            ViewBag.Message = checkin.ToString() + checkout.ToString() + guests.ToString() + id;

            return View();
        }
    }
}