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
       
       
        /// <summary>
        ///         Index that will return a list of library books with the information to view
        /// </summary>
        public ActionResult Index()
        {
            using(LibraryEntities db = new LibraryEntities())
            {
                try
                {
                    List<Item> items = db.Items.Where(x => x.ItemId != 0).ToList();
                    CatalogItemViewModel catalogItemViewModel = new CatalogItemViewModel();
                    catalogItemViewModel.catalogItemList = items;

                    return View(catalogItemViewModel);
                }
                catch
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                }
               
            }
        }

        /// <summary>
        ///  Get create item view
        /// </summary>
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
            using (LibraryEntities db = new LibraryEntities())
            {
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
                    item.ItemType = db.ItemTypes.Where(x => x.ItemTypeId == catalogItem.ItemTypeID).FirstOrDefault();
                    if (ModelState.IsValid)
                    {
                        db.Items.Add(item);
                        db.SaveChanges();
                        return Json(new { success = "true" });
                    }
                }
                catch (DataException)
                {
                    return Json(new { sucess = "false" });
                }
                return View(item);
            }
        }
        

        [HttpGet]
        //finds an item by the item id and returns the item to the details view
        public ActionResult ItemDetails(int id)
        {
            using (LibraryEntities db = new LibraryEntities())
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
        }

        [HttpPost]
        public ActionResult EditItem(CatalogItemViewModel catalogItem)
        {
            using (LibraryEntities db = new LibraryEntities())
            {
                //create new item to save edit item too
                Item item = db.Items.Find(catalogItem.ItemID);

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
                    item.ItemType = db.ItemTypes.Where(x => x.ItemTypeId == catalogItem.ItemTypeID).FirstOrDefault();
                    if (ModelState.IsValid)
                    {
                        db.SaveChanges();
                        return Json(new { success = "true" });
                    }
                }
                catch (DataException)
                {
                    return Json(new { sucess = "false" });
                }
                return View(item);
            }
        }

      
       
        /// <summary>
        /// arhcives an item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteItem(int id)
        {
            using (LibraryEntities db = new LibraryEntities())
            {
                try
                {
                    Item item = db.Items.Find(id);
                    List<ItemStatusLog> itemStatusList = db.ItemStatusLogs.Where(x => x.ItemId == id).ToList();
                    foreach (var itemstatus in itemStatusList)
                    {
                        db.ItemStatusLogs.Remove(itemstatus);
                        db.SaveChanges();
                    }
                    db.Items.Remove(item);
                    db.SaveChanges();
                }
                catch
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return View("Index");
            }
        }

    }
}