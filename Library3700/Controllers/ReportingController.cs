using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Library3700.Controllers
{
    public class ReportingController : Controller
    {
        // GET: Reporting
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GeneratePatronReport()
        {
            return View();
        }

        public ActionResult GenerateLibrarianReport()
        {
            return View();
        }

         
    }
}