using API.Data;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.BusinessLogic
{
    public class ContactLogic
    {
        private readonly AddressBookContext _context;

        public ContactLogic(AddressBookContext context)
        {
            _context = context;
        }

        public List<Contacts> GetContacts()
        {
            try
            {
                var contacts = _context.Contacts.Select(x => x).ToList();
                return contacts;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public Contacts GetContact(int id)
        {
            try
            {
                var contact = _context.Contacts.Find(id);
                return contact;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public bool AddContact(Contacts contact)
        {
            
            try
            {
                _context.Contacts.Add(contact);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public bool UpdateContact(Contacts contact)
        {
            try
            {
            _context.Contacts.Update(contact);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public bool RemoveContact(Contacts contact)
        {
            try
            {
                _context.Contacts.Remove(contact);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            
        }
    }
}
