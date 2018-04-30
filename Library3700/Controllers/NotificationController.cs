using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library3700.Controllers
{
    /// <summary>
    /// this controller holds all of the json notifications that are being sent from the controllers to the views
    /// </summary>
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

        public ActionResult AddAccountSuccess(string temporaryPassword)
        {
            return Json(new { success = true, msg = "Account has been created. Temporary Password for login is: " + temporaryPassword });
        }

        public ActionResult AddAccountFailure()
        {
            return Json(new { success = false, msg = "Unable to create account. Please try again." });
        }

        public ActionResult ReserveItemSuccess(DateTime holddate)
        {
            return Json(new { success = true, msg = "Item has been successfully reserved. Please pickup at the library. Item will be held for pickup until: " + holddate });
        }

        public ActionResult MissingItemSuccess()
        {
            return Json(new { success = true, msg = "Item has been reported missing successfully." });

        }

        public ActionResult ResetPasswordUserNotFound()
        {
            return Json(new { Success = false, Message = "An account with that username was not found!" });
        }

        public ActionResult ResetPasswordSuccess(string pass)
        {
            return Json(new { Success = true, Message = "User assigned new temporary password: " + pass });
        }

        public ActionResult UnknownError()
        {
            return Json(new { Success = false, Message = "An unexpected error has occurred!" });
        }

        public ActionResult UpdateAccountStatusSuccess()
        {
            return Json(new { Success = true, Message = "Account status updated" });
        }
    }
}