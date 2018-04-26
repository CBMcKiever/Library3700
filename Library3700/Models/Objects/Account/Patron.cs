using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library3700.Models.Objects.Account
{
    public class Patron : AccountAdapter
    {
        public int PatronId { get => base.AccountNumber; set => base.AccountNumber = value; }

        public string PatronFirstName { get => base.FirstName; set => base.FirstName = value; }

        public string PatronLastName { get => base.LastName; set => base.LastName = value; }

        public string PatronEmailAddress { get => base.EmailAddress; set => base.EmailAddress = value; }
    }
}