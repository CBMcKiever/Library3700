using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library3700.Models.ViewModels
{
    public class CatalogItemViewModel
    {
        public int ItemID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public short PublicationYear { get; set; }
        public byte ItemTypeID { get; set; }

        public Item Item { get; set; }
        public ItemType ItemType { get; set; }
        public List<Item> catalogItemList { get; set; }
    }
}