using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pakhiwebsolutions.ViewModels;

namespace pakhiwebsolutions.ViewModels
{
    public class ContactEditViewModel:ContactCreateViewModel
    {
        public string ExistingDocPath { get; set; }
    }
}
