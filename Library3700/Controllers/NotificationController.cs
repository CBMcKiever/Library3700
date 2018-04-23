using Library3700.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library3700.Controllers
{
    public class NotificationController : Controller
    {
        // GET: Notification
        public ActionResult Index()
        {
            return View();
        }

        /* 
         * Class model methods
         */

        public Notification CreateNotification(string text) => new Notification(text);

        public void SendNotification(Notification notification)
        {
            // TODO: implement
        }
    }
}