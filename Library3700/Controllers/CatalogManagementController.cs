using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Library3700.Models;
using Library3700.Models.ViewModels;

namespace Library3700.Controllers
{
    public class CatalogManagementController : Controller
    {
        LibraryEntities db = new LibraryEntities();
       
        //Index that will return a list of library books with the information to view
        public ActionResult Index()
        {
            List<Item> items = db.Items.Where(x => x.ItemId != 0).ToList();
            CatalogItemViewModel catalogItemViewModel = new CatalogItemViewModel();
            catalogItemViewModel.catalogItemList = items;

            return View(catalogItemViewModel);
        }

        //Get create item view
        [HttpGet] 
        public ActionResult CreateItem()
        {
            return View("CreateItem");
        }

        //post method to create an item
        [HttpPost]
        public ActionResult CreateItem(CatalogItemViewModel catalogItem)
        {
            //create new item
            Item item = new Item();

            try
            {
                //set item equal to view model object
                item.Author = catalogItem.Author;
                item.Title = catalogItem.Title;
                item.Genre = catalogItem.Genre;
                item.PublicationYear = catalogItem.PublicationYear;
                //needs to be changed but setting everything to book right now
                catalogItem.ItemTypeID = 1;
                item.ItemTypeId = catalogItem.ItemTypeID;
                item.ItemType = db.ItemTypes.Where(x => x.ItemTypeId == 1).FirstOrDefault();
                if (ModelState.IsValid)
                {
                    db.Items.Add(item);
                    db.SaveChanges();
                    return Json(new { success = "true"});
                }
            }
            catch (DataException )
            {
                return Json(new {sucess = "false" });
            }
            return View(item);
        }

        [HttpGet]
        //finds an item by the item id and returns the item to the details view
        public ActionResult ItemDetails(int id)
        {
            try
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CatalogItemViewModel catalogItemViewModel = new CatalogItemViewModel();
                catalogItemViewModel.Item = db.Items.Find(id);
                if (catalogItemViewModel == null)
                {
                    return HttpNotFound();
                }
                return View(catalogItemViewModel);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        public ActionResult EditItem(int id)
        {
            return View();
        }
        public ActionResult DeleteItem(int id)
        {
            return View();
        }

    }
}