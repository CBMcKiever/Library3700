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
            return View();
        }

        private static List<AccountItems> GeneratePatronItemsList(int accountID)
        {
            using (LibraryEntities db = new LibraryEntities())
            {
                try
                {
                    List<ItemStatusLog> accountitemList = db.ItemStatusLogs.Where(x => x.AccountId == accountID && (x.ItemStatusTypeId == 2 || x.ItemStatusTypeId == 4)).ToList();
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
    }
}