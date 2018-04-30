using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library3700.Models.ViewModels
{
    public class Accounts
    {
        public Nullable<int> AccountID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsLibrarian { get; set; }
        public AccountStatusType AccountStatus { get; set; }
        public string EmailAddress { get; set; }
        public List<AccountItems> AccountItems { get; set; }
    }
}