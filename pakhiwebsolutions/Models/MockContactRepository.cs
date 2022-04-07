using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pakhiwebsolutions.Models
{
    public class MockContactRepository :IContactRepository
    {
        private List<Contact> _contactList;
        public MockContactRepository()
        {
            _contactList = new List<Contact>()
            {
                new Contact(){FullName="Yogendra Kumar",PhoneNo="9897505503",EmailId="yogendrakumar8219@gmail.com",CompanyName="pakhi web solutions",RequirementDescription="My firm reuirement websites for growing business",DocPath=""},
                new Contact(){FullName="Sanjay Kumar",PhoneNo="9897653676",EmailId="sanjaykumar@gmail.com",CompanyName="shiv computer",RequirementDescription="My firm reuirement websites for growing business",DocPath=""}
            };
        }

        public Contact Add(Contact contact)
        {
            _contactList.Add(contact);
            return contact;
        }

        public IEnumerable<Contact> GetAllContact()
        {
            return _contactList;
        }

        public Contact GetContact(int ContactId)
        {
            return this._contactList.FirstOrDefault(e => e.ContactId == ContactId);
        }

        public Contact Update(Contact contactChanges)
        {
            Contact contact = _contactList.FirstOrDefault(e => e.ContactId == contactChanges.ContactId);
            if (contact != null)
            {
                contact.FullName = contactChanges.FullName;
                contact.PhoneNo = contactChanges.PhoneNo;
                contact.EmailId = contactChanges.EmailId;
                contact.CompanyName = contact.CompanyName;
                contact.RequirementDescription = contact.RequirementDescription;
            }
            return contact;
        }

        public Contact Delete(int ContactId)
        {
            Contact contact = _contactList.FirstOrDefault(e => e.ContactId == ContactId);
            if (contact != null)
            {
                _contactList.Remove(contact);
            }
            return contact;
        }

    }
}
