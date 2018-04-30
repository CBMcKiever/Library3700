using Library3700.Models;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Library3700.Controllers
{
    

    [AllowAnonymous]
    public class LoginController : Controller
    {
        NotificationController notification = new NotificationController();

        /// <summary>
        /// A simple wrapper for login requests
        /// </summary>
        public class LoginRequest
        {
            public string Username { get; set; }
            public string PasswordHash { get; set; }
        }

        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            // don't show login page to user already logged in
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Home", "AccountManagement");
            }
            else
            {
                ViewBag.LoginError = TempData?["loginError"];
                return View();
            }
        }

        /// <summary>
        /// Handle login form request.
        /// </summary>
        /// <param name="request">Login request form form data</param>
        /// <returns>
        /// Returns redirect to home page if successful
        /// otherwise, returns to login page.
        /// </returns>
        [HttpPost]
        public ActionResult LogIn(LoginRequest request)
        {
            try
            {
                using (var db = new LibraryEntities())
                {
                    var targetLogin = db.Logins
                        .Where(x => x.Username == request.Username && x.PasswordHash == request.PasswordHash)
                        .SingleOrDefault();

                    if (targetLogin == null)
                    {
                        TempData["loginError"] = "The username or password is incorrect";
                        return RedirectToAction("Index");
                    }

                    var userAccount = targetLogin.Account;
                    var recentStatusLog = userAccount.AccountStatusLogs.OrderByDescending(x => x.LogDateTime).FirstOrDefault();
                    var currentStatus = recentStatusLog.AccountStatusType.StatusTypeName;
                    if (currentStatus == "terminated")
                    {
                        TempData["loginError"] = "The username or password is incorrect";
                        return RedirectToAction("Index");
                    }
                    else if (currentStatus == "suspended")
                    {
                        TempData["loginError"] = "The account with the given username has been suspended";
                        return RedirectToAction("Index");
                    }

                    var identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.GivenName, userAccount.FirstName),
                        new Claim(ClaimTypes.Surname, userAccount.LastName),
                        new Claim(ClaimTypes.Role, userAccount.IsLibrarian ? "librarian" : "patron"),
                        new Claim(ClaimTypes.Email, targetLogin.Username),
                        new Claim(ClaimTypes.UserData, userAccount.AccountId.ToString())
                    },
                    "ApplicationCookie");

                    IOwinContext ctx = Request.GetOwinContext();
                    IAuthenticationManager authMgr = ctx.Authentication;

                    authMgr.SignIn(identity);

                    // If password is temporary, user must set a new password on login
                    if (targetLogin.IsPasswordTemporary)
                    {
                        return RedirectToAction("SetPasswordConfirm", "AccountManagement");
                    }
                    else
                    {
                        return RedirectToAction("Home", "AccountManagement");
                    }
                }
            }
            catch (Exception e)
            {
                // TODO: show error page
                return View("Index");
            }
        }

        public ActionResult LogOut()
        {
            IOwinContext ctx = Request.GetOwinContext();
            IAuthenticationManager authMgr = ctx.Authentication;

            authMgr.SignOut("ApplicationCookie");

            System.Web.HttpContext.Current.Session.Remove("activeAccount");

            return RedirectToAction("Index", "Login");
        }

    }
}