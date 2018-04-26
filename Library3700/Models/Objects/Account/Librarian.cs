using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library3700.Models.Objects.Account
{
    public class Librarian : AccountAdapter
    {
        public int LibrarianId { get => base.AccountNumber; set => base.AccountNumber = value; }

        public string LibrarianFirstName { get => base.FirstName; set => base.FirstName = value; }

        public string LibrarianLastName { get => base.LastName; set => base.LastName = value; }

        public string LibrarianEmailAddress { get => base.EmailAddress; set => base.EmailAddress = value; }
    }
}