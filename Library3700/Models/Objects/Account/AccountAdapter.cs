using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library3700.Models.Objects.Account
{
    /// <summary>
    /// Account adapter class
    /// </summary>
    public abstract class AccountAdapter
    {
        public int AccountNumber { get; set; }

        public byte AccountStatus { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

    }
}