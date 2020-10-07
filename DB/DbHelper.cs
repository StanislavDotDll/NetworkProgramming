using System.Collections.Generic;
using System.Linq;

namespace DB
{
    public class DbHelper
    {
        ContactContext context = new ContactContext();
        public void AddContact(Contact contact)
        {
            context.Contacts.Add(contact);
            context.SaveChanges();
        }

        public List<Contact> GetContacts()
        {
            return context.Contacts.ToList();
        }
    }
}
