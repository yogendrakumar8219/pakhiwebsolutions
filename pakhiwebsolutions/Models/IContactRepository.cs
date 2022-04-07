using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pakhiwebsolutions.Models
{
    public interface IContactRepository
    {
        Contact GetContact(int ContactId);
        IEnumerable<Contact> GetAllContact();
        Contact Add(Contact contact);
        Contact Update(Contact contact);
        Contact Delete(int ContactId);

    }
}
