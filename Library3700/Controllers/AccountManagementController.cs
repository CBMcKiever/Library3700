using Library3700.Models.Objects.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using Library3700.Models;
using System.Security.Cryptography;
using System.Text;

namespace Library3700.Controllers
{
    public class AccountManagementController : Controller
    {

        public class RegisterRequest
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string EmailAddress { get; set; }

            public bool IsLibrarian { get; set; }
        }

        /// <summary>
        /// Display the appropriate home page based on who is logged in.
        /// </summary>
        /// <returns></returns>
        public ActionResult Home()
        {
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            SetActiveAccount();

            if (User.IsInRole("librarian"))
            {
                return View("LibrarianHome");
            }
            else if (User.IsInRole("patron"))
            {
                return View("PatronHome");
            }
            else
            {
                return RedirectToAction("LogOut", "Login");
            }
        }
        
        /// <summary>
        /// Display add account page.
        /// </summary>
        /// <returns></returns>
        public ActionResult AddAccount()
        {
            return View();
        }

        /// <summary>
        /// Handle form request to register new user.
        /// </summary>
        /// <param name="request">Form data</param>
        /// <returns></returns>
        public JsonResult RegisterNewAccount(RegisterRequest request)
        {
            try
            {
                using (var db = new LibraryEntities())
                {
                    if (db.Logins.Where(x => x.Username == request.EmailAddress).Any())
                    {
                        throw new ApplicationException(
                            "A user with the email address " + request.EmailAddress + " already exists!");
                    }

                    var newAccount = new Account
                    {
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        IsLibrarian = request.IsLibrarian
                    };

                    string temporaryPassword = GenerateTemporaryPassword();
                    string temporaryPasswordHash = HashString(temporaryPassword);

                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            db.Accounts.Add(newAccount);
                            db.SaveChanges();

                            var newLogin = new Login
                            {
                                AccountId = newAccount.AccountId,
                                Username = request.EmailAddress,
                                PasswordHash = temporaryPasswordHash,
                                IsPasswordTemporary = true
                            };

                            db.Logins.Add(newLogin);
                            db.SaveChanges();
                            
                            transaction.Commit();
                        }
                        catch (Exception e)
                        {
                            transaction.Rollback();
                            throw e;
                        }
                    }

                    return Json(new
                    {
                        Success = true,
                        Message = "Created user with temporary password: " + temporaryPassword
                    });
                }
            }
            catch (Exception e)
            {
                return Json(new { Success = false, e.Message });
            }
        }

        /// <summary>
        /// Displays page to change user's temporary password.
        /// </summary>
        /// <returns></returns>
        public ActionResult SetPasswordConfirm()
        {
            SetActiveAccount();  // establish identity since we have not yet visited the homepage
            return View();
        }

        /// <summary>
        /// Handles form request for setting new password.
        /// </summary>
        /// <param name="passwordHash">Hash of new password to set</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdatePassword(string passwordHash)
        {
            using (var db = new LibraryEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var activeAccount = (AccountAdapter)System.Web.HttpContext.Current.Session["activeAccount"];

                        Login userLogin = db.Logins.Where(x => x.AccountId == activeAccount.AccountNumber).SingleOrDefault();
                        userLogin.PasswordHash = passwordHash;
                        userLogin.IsPasswordTemporary = false;
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
                    }
                }           
            }

            return RedirectToAction("Home");
        }

        /// <summary>
        /// Sets active account for the controller
        /// </summary>
        private void SetActiveAccount()
        {
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            if (User.IsInRole("librarian"))
            {
                System.Web.HttpContext.Current.Session["activeAccount"] = new Librarian
                {
                    LibrarianEmailAddress = claims.Where(x => x.Type == ClaimTypes.Email).Single().Value,
                    LibrarianFirstName = claims.Where(x => x.Type == ClaimTypes.GivenName).Single().Value,
                    LibrarianLastName = claims.Where(x => x.Type == ClaimTypes.Surname).Single().Value,
                    LibrarianId = Int32.Parse(claims.Where(x => x.Type == ClaimTypes.UserData).Single().Value)
                };
            }
            else if (User.IsInRole("patron"))
            {
                System.Web.HttpContext.Current.Session["activeAccount"] = new Patron
                {
                    PatronEmailAddress = claims.Where(x => x.Type == ClaimTypes.Email).Single().Value,
                    PatronFirstName = claims.Where(x => x.Type == ClaimTypes.GivenName).Single().Value,
                    PatronLastName = claims.Where(x => x.Type == ClaimTypes.Surname).Single().Value,
                    PatronId = Int32.Parse(claims.Where(x => x.Type == ClaimTypes.UserData).Single().Value)
                };
            }
        }

        /// <summary>
        /// Hash a string with SHA512
        /// </summary>
        /// <param name="source">String to hash</param>
        /// <returns>SHA512 hash of string</returns>
        private static string HashString(string source)
        {
            using (var sha512 = SHA512.Create())
            {
                byte[] bytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(source));

                StringBuilder sb = new StringBuilder();
                foreach (var b in bytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Generate a random 8 character alphanumeric password
        /// </summary>
        /// <returns>Password</returns>
        private static string GenerateTemporaryPassword()
        {
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new String(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}