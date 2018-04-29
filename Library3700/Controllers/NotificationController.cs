﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library3700.Controllers
{
    public class NotificationController : Controller
    {
        public ActionResult SuccessItemCreation()
        {
            return Json(new { success = true, msg = "Item created successfully." });
        }
        
        public ActionResult FailureItemCreation()
        {
            return Json(new { success = false, msg = "Unable to create item. Please try again." });
        }

        public ActionResult EditItemSuccess()
        {
            return Json(new { success = true, msg = "Item edited successfully." });
        }

        public ActionResult EditItemFailure()
        {
            return Json(new { success = false, msg = "Unable to edit item. Please try again." });
        }

        public ActionResult DeleteItemSuccess()
        {
            return Json(new { success = true, msg = "Item deleted successfully." });
        }
        
        public ActionResult DeleteItemFailure()
        {
            return Json(new { success = false, msg = "Unable to delete item. Please try again." });
        }

        public ActionResult UpdateItemSuccess()
        {
            return Json(new { success = true, msg = "Item status updated successfully." });
        }

        public ActionResult UpdateItemFailure()
        {
            return Json(new { success = false, msg = "Unable to update item status. Please try again." });
        }

        public ActionResult CheckoutSuccess(DateTime dateDue)
        {

            return Json(new { success = true, msg = "Item Checked Out and is due back: " + dateDue });
        }

        public ActionResult CheckoutFailure()
        {
            return Json(new { success = false, msg = "Unable to checkout item. Please try again." });
        }

        //public class Notification
        //{
        //    private static int _count;

        //    private int _id;
        //    private string _text;

        //    static Notification()
        //    {
        //        _count = 0;
        //    }

        //    public Notification(string text)
        //    {
        //        _id = _count++;
        //        _text = text;
        //    }

        //    public int Id { get; }

        //    public string GetNotification => _text;
        //}

        //// GET: Notification
        //public ActionResult Index()
        //{
        //    return View();
        //}

        ///* 
        // * Class model methods
        // */

        //public static Notification CreateNotification(string text) => new Notification(text);

        //public static void SendNotification(Notification notification)
        //{
        //    // TODO: implement
        //}
    }
}