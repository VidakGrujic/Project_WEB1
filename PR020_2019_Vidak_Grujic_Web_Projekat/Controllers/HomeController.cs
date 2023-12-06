using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {        
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
