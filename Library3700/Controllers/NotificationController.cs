using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library3700.Controllers
{
    public class NotificationController : Controller
    {
        public class Notification
        {
            private static int _count;

            private int _id;
            private string _text;

            static Notification()
            {
                _count = 0;
            }

            public Notification(string text)
            {
                _id = _count++;
                _text = text;
            }

            public int Id { get; }

            public string GetNotification => _text;
        }

        // GET: Notification
        public ActionResult Index()
        {
            return View();
        }

        /* 
         * Class model methods
         */

        public static Notification CreateNotification(string text) => new Notification(text);

        public static void SendNotification(Notification notification)
        {
            // TODO: implement
        }
    }
}