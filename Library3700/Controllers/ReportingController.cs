using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library3700.Models.ViewModels;
using System.Data;
using Library3700.Models;
using Library3700.Models.Objects.Account;

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
            var activeAccount = (AccountAdapter)System.Web.HttpContext.Current.Session["activeAccount"];
            List<AccountItems> patronItems = GeneratePatronItemsList(activeAccount.AccountNumber);
            return View("PatronReport", patronItems);
        }

        public ActionResult GenerateLibrarianReport()
        {
            return View("LibrarianReport", AccountsList());
        }

        private static List<AccountItems> GeneratePatronItemsList(int accountID)
        {
            using (LibraryEntities db = new LibraryEntities())
            {
                try
                {
                    IEnumerable<ItemStatusLog> latestActions =
                        from x in db.ItemStatusLogs
                        group x by x.AccountId into g
                        select g.OrderByDescending(y => y.LogDateTime).FirstOrDefault();

                    List < ItemStatusLog > accountitemList = latestActions.Where(x => x.AccountId == accountID && (x.ItemStatusTypeId == 2 || x.ItemStatusTypeId == 4)).ToList();
                    List<AccountItems> accountItems = new List<AccountItems>();

                    if (accountitemList != null || accountitemList.Count != 0)
                    {
                        foreach (var i in accountitemList)
                        {
                            AccountItems accountitem = new AccountItems
                            {
                                item = i.Item,
                                accountID = i.AccountId,
                                account = i.Account,
                                itemTypeID = i.ItemStatusTypeId,
                                itemStatusText = i.ItemStatusType.ItemStatusName
                            };
                            accountItems.Add(accountitem);
                        }
                    }

                    return accountItems;
                }
                catch
                {
                    return null;
                }
            }
        }

        public static List<Accounts> AccountsList()
        {
            using (LibraryEntities db = new LibraryEntities())
            {
                List<Account> accounts = db.Accounts.Where(x => x.IsLibrarian == false).ToList();
                List<Accounts> accountList = new List<Accounts>();
                foreach (var account in accounts)
                {
                    Accounts acc = new Accounts
                    {
                        FirstName = account.FirstName,
                        LastName = account.LastName,
                        IsLibrarian = account.IsLibrarian,
                        AccountID = account.AccountId,
                        EmailAddress = db.Logins.Where(x => x.AccountId == account.AccountId).Select(x => x.Username).SingleOrDefault(),
                        AccountItems = GeneratePatronItemsList(account.AccountId)
                    };
                    accountList.Add(acc);
                }
                return accountList;
            }
        }
    }
}