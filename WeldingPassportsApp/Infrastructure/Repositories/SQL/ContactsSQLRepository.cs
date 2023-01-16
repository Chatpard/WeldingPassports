using Application.Interfaces;
using Application.Interfaces.Repositories.SQL;
using Application.Security;
using Application.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models;
using Infrastructure.Services.Persistence;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQL
{
    public class ContactsSQLRepository : SaveChangesSQL, IContactsSQLRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDataProtector _protector;

        public ContactsSQLRepository(AppDbContext context, IMapper mapper,
            IDataProtectionProvider dataProtectionProvider, IDataProtectionPurposeStrings dataProtectionPurposeStrings) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _protector = dataProtectionProvider
                .CreateProtector(dataProtectionPurposeStrings.IdRouteValue);
        }

        private IQueryable<CompanyContactIndexViewModel> GetContactsIndex()
        {
            IQueryable<CompanyContact> query = _context.CompanyContacts
                .Select(companyContact => companyContact);

            return query.ProjectTo<CompanyContactIndexViewModel>(_mapper.ConfigurationProvider);
        }

        public async Task<IPaginatedList<CompanyContactIndexViewModel>> GetContactsIndexPaginatedAsync(int pageSize, int pageIndex, string searchString, string sortOrder)
        {
            var contactsQuery = GetContactsIndex();

            contactsQuery = SearchContactIndex(contactsQuery, searchString);

            contactsQuery = SortContactIndex(contactsQuery, sortOrder);

            return await PaginatedList<CompanyContactIndexViewModel>.CreateAsync(contactsQuery.AsNoTracking(), pageIndex, pageSize);
        }

        public async Task<EntityEntry<Contact>> PostContactCreateAsync(Contact contact)
        {
            EntityEntry<Contact> newContact = await _context.Contacts.AddAsync(contact);
            newContact.State = EntityState.Added;
            return newContact;
        }

        public async Task<CompanyContactDetailsViewModel> GetContactDetailsAsync(string encryptedID)
        {
            int decryptedID = Convert.ToInt32(_protector.Unprotect(encryptedID));

            IQueryable<Contact> query = _context.Contacts
                .Where(contact => contact.ID == decryptedID);

            return await query.ProjectTo<CompanyContactDetailsViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public async Task<ContactEditViewModel> GetContactEditAsync(string encryptedID)
        {
            if (!int.TryParse(encryptedID, out int decryptedID))
            {
                decryptedID = Convert.ToInt32(_protector.Unprotect(encryptedID));
            }

            IQueryable<Contact> query = _context.Contacts
                .Where(contact => contact.ID == decryptedID);

            return await query.ProjectTo<ContactEditViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public EntityEntry<Contact> PostContactEdit(Contact contactChanges)
        {
            EntityEntry<Contact> contact = _context.Entry<Contact>(contactChanges);
            contact.State = EntityState.Modified;
            return contact;
        }

        public async Task<int> DeleteContactByEncryptedIDAsync(string encryptedID, CancellationToken token)
        {
            int decryptedID = Convert.ToInt32(_protector.Unprotect(encryptedID));
            var companyContact = await _context.CompanyContacts.Where(companyContact => companyContact.ID == decryptedID).Include(companyContact => companyContact.Contact).AsNoTracking().SingleOrDefaultAsync();
            var contact = companyContact.Contact;
            var user = companyContact?.IdentityUser;

            if (user == null)
            {
                _context.Remove(new CompanyContact { ID = decryptedID });

                var count = await _context.CompanyContacts.Where(companyContact => companyContact.ContactID == contact.ID).SelectMany(companyContact => companyContact.Contact.CompanyContacts).CountAsync();

                if (count == 1)
                    _context.Contacts.Remove(new Contact { ID = contact.ID });
            }

            return await SaveAsync(token);
        }

        public SelectList ContactSelectList()
        {
            var contacts = _context.Contacts
                .OrderBy(contact => contact.LastName).ThenBy(contact => contact.FirstName)
                .Select(contact => new {
                    ID = contact.ID,
                    Name = contact.FirstName + " " + contact.LastName
                });

            return new SelectList(contacts, nameof(Contact.ID), "Name");
        }

        private static IQueryable<CompanyContactIndexViewModel> SortContactIndex(IQueryable<CompanyContactIndexViewModel> contactsQuery, string sortOrder)
        {
            switch (sortOrder)
            {
                case "FirstName_desc":
                    contactsQuery = contactsQuery.OrderByDescending(contact => contact.FirstName);
                    return contactsQuery;
                case "FirstName_asc":
                case null:
                case "":
                    contactsQuery = contactsQuery.OrderBy(contact => contact.FirstName);
                    return contactsQuery;
                case "LastName_desc":
                    contactsQuery = contactsQuery.OrderByDescending(contact => contact.LastName);
                    return contactsQuery;
                case "LastName_asc":
                    contactsQuery = contactsQuery.OrderBy(contact => contact.LastName);
                    return contactsQuery;
                case "Email_desc":
                    contactsQuery = contactsQuery.OrderByDescending(contact => contact.Email);
                    return contactsQuery;
                case "Email_asc":
                    contactsQuery = contactsQuery.OrderBy(contact => contact.Email);
                    return contactsQuery;
                default:
                    throw new InvalidOperationException("SortOrder nout found.");
            }
        }

        private static IQueryable<CompanyContactIndexViewModel> SearchContactIndex(IQueryable<CompanyContactIndexViewModel> contactsQuery, string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                contactsQuery = contactsQuery.Where(contact => contact.FirstName.ToLower().Contains(searchString.ToLower())
                    || contact.LastName.ToLower().Contains(searchString.ToLower())
                    || contact.Email.ToLower().Contains(searchString.ToLower()));
            }

            return contactsQuery;
        }

    }
}
