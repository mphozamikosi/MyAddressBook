using API.Models;
using System;
using System.Linq;

namespace API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AddressBookContext context)
        {
            context.Database.EnsureCreated();

            if (context.Contacts.Any())
            {
                return;   
            }

            var contact = new Contacts[]
            {
                new Contacts{FirstName="Mpho",Surname="Mikosi",Cel="0671764259", Tel="0671764259", UpdatedDate=DateTime.Now.ToString()},
            };
            foreach (Contacts s in contact)
            {
                context.Contacts.Add(s);
            }
            context.SaveChanges();
        }
    }
}