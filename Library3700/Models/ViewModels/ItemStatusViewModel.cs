using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library3700.Models.ViewModels
{
    public class ItemStatusViewModel
    {
        public Item Item {get; set;}
        public Account Account { get; set; }
        public ItemStatusType ItemStatusType { get; set; }
        public ItemStatusLog ItemStatusLog { get; set; }
        public int ItemID { get; set; }
        public int AccountID { get; set; }
        public int itemStatusTypeID { get; set; }
        public IEnumerable<Item> ItemList { get; set; }
        public IEnumerable<Account> AccountList { get; set; }
        public IEnumerable<Item> MissingItemList { get; set; }
        public string ItemStatusText { get; set; }
    }
}