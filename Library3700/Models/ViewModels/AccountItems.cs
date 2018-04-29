using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library3700.Models.ViewModels
{
    public class AccountItems
    {
        public Nullable<int> accountID { get; set; }
        public Nullable<int> itemID { get; set; }
        public Nullable<int> itemTypeID { get; set; }
        public string itemStatusText { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public Item item { get; set; }
        public Account account { get; set; }
    }
}