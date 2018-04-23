using Library3700.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Library3700.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// A simple wrapper for login requests
        /// </summary>
        public class LoginRequest
        {
            public string Username { get; set; }
            public string PasswordHash { get; set; }
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Handle request from login form, providing a cookie to an authenticated user or denying an invalid login.
        /// </summary>
        /// <param name="loginRequest">Wrapper for URL parameters (Username, PasswordHash).</param>
        /// <returns>Action based on result of authentication</returns>
        public HttpStatusCodeResult Login(LoginRequest loginRequest)  // returning status code for testing purposes
        // public ActionResult Login(LoginRequest loginRequest)
        {
            // TODO: redirect to proper page

            string username = loginRequest.Username;
            string passwordHash = loginRequest.PasswordHash;

            try
            {
                if (String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(passwordHash))
                {
                    // error: invalid parameter
                    throw new ApplicationException("Value username and/or passwordHash is invalid!");
                }

                using (var db = new LibraryEntities())
                {
                    var targetLogin = db.Logins.Where(x => x.Username == username).FirstOrDefault();
                    if (targetLogin == null)
                    {
                        // error: user not found
                        throw new ApplicationException("User " + username + " not found!");
                    }

                    bool isCorrectPassword = targetLogin.PasswordHash == passwordHash;
                    if (!isCorrectPassword)
                    {
                        // error: incorrect password
                        throw new ApplicationException("Incorrect password!");
                    }

                    HttpCookie loginCookie = CreateCookie(targetLogin);
                    Response.Cookies.Add(loginCookie);

                    //return View();
                    return new HttpStatusCodeResult(200);
                }
            }
            catch (Exception e)
            {
                // handle login error
                return new HttpStatusCodeResult(500);
            }
        }

        private HttpCookie CreateCookie(Login login)
        {
            string cookieValue = String.Format("{0};{1};{2}", login.AccountId, login.LoginId, login.Username);
            string cookieHash;

            using (var sha512 = SHA512.Create())
            {
                byte[] cookieBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(cookieValue));
                var sb = new StringBuilder();
                foreach (byte b in cookieBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                cookieHash = sb.ToString();
            }

            var cookie = new HttpCookie("auth", login.Username + ":" + cookieHash)
            {
                Expires = DateTime.Now.AddDays(1)
            };

            return cookie;
        }
    }
}