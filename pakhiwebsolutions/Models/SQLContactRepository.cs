using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pakhiwebsolutions.Models
{
    public class SQLContactRepository : IContactRepository
    {
        private readonly AppDbContext context;
        public SQLContactRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Contact Add(Contact contact)
        {
            context.Contacts.Add(contact);
            context.SaveChanges();
            return contact;
        }
        public Contact Delete(int ContactId)
        {
            Contact contact = context.Contacts.Find(ContactId);
            if (contact != null)
            {
                context.Contacts.Remove(contact);
                context.SaveChanges();
            }
            return contact;
        }
        public IEnumerable<Contact> GetAllContact()
        {
            return context.Contacts;
        }
        public Contact GetContact(int ContactId)
        {
            return context.Contacts.Find(ContactId);
        }
        public Contact Update(Contact contactChanges)
        {
            //var contact = context.Contacts.Attach(contactChanges);
            //contact.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return contactChanges;
        }

    }
}
