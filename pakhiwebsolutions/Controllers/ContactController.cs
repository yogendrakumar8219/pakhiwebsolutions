using pakhiwebsolutions.Models;
using pakhiwebsolutions.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace pakhiwebsolutions.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly AppDbContext _context;

        public ContactController(IContactRepository contactRepository, IWebHostEnvironment hostingEnvironment, AppDbContext context)
        {
            _contactRepository = contactRepository;
            this.hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        [HttpPost]
        public JsonResult GetContactDetail(int ContactId)
        {
            Contact contact = _contactRepository.GetContact(ContactId);
            Contact newContact = new Contact
            {
                ContactId = contact.ContactId,
                FullName = contact.FullName,
                PhoneNo = contact.PhoneNo,
                EmailId = contact.EmailId,
                CompanyName = contact.CompanyName,
                RequirementDescription = contact.RequirementDescription
            };
            return Json(newContact);
            //FeesManagement.Models.Categerys.OBC
        }
        [HttpGet]
        public ViewResult Index()
        {
            var model = _contactRepository.GetAllContact();
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Create(ContactCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                Contact newContact = new Contact
                {
                    FullName = model.FullName,
                    PhoneNo = model.PhoneNo,
                    EmailId = model.EmailId,
                    CompanyName = model.CompanyName,
                    RequirementDescription = model.RequirementDescription,
                    DocPath = uniqueFileName
                };
                _contactRepository.Add(newContact);
                RedirectToAction("index");
            }
            return View();

        }

        private string ProcessUploadedFile(ContactCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.DocPath != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "files");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.DocPath.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.DocPath.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        [HttpGet]
        public ViewResult Edit(int ContactId)
        {
            Contact contact = _contactRepository.GetContact(ContactId);

            ContactEditViewModel contactEditViewModel = new ContactEditViewModel
            {
                FullName = contact.FullName,
                PhoneNo = contact.PhoneNo,
                EmailId = contact.EmailId,
                CompanyName = contact.CompanyName,
                RequirementDescription = contact.RequirementDescription,
                ExistingDocPath = contact.DocPath
            };
            return View(contactEditViewModel);
        }
        [HttpPost]
        public IActionResult Edit(ContactEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Contact contact = _contactRepository.GetContact(model.ContactId);
                contact.FullName = model.FullName;
                contact.PhoneNo = model.PhoneNo;
                contact.EmailId = model.EmailId;
                contact.CompanyName = model.CompanyName;
                contact.RequirementDescription = model.RequirementDescription;
                if (model.DocPath != null)
                {
                    if (model.ExistingDocPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "files", model.ExistingDocPath);
                        System.IO.File.Delete(filePath);
                    }
                    contact.DocPath = ProcessUploadedFile(model);
                }
                Contact updateContact = _contactRepository.Update(contact);
                return RedirectToAction("index");
            }
            return View(model);

        }
        [HttpPost]
        public IActionResult Delete(int ContactId)
        {
            Contact contact = _contactRepository.GetContact(ContactId);

            if (contact == null)
            {
                Response.StatusCode = 404;
                return View("ContactNotFound", ContactId.ToString());
            }
            else
            {
                try
                {
                    Contact deleteContact = _contactRepository.Delete(contact.ContactId);
                    if (contact.DocPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "files", contact.DocPath);
                        System.IO.File.Delete(filePath);
                    }

                    return RedirectToAction("index");
                }
                catch (Exception ex)
                {

                }

            }
            return View("");

        }

    }
}
