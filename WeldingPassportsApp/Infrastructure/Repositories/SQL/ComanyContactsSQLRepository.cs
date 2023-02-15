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
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQL
{
    public class CompanyContactsSQLRepository : SaveChangesSQL, ICompanyContactsSQLRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDataProtector _protector;

        public CompanyContactsSQLRepository(AppDbContext context, IMapper mapper,
            IDataProtectionProvider dataProtectionProvider, IDataProtectionPurposeStrings dataProtectionPurposeStrings) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _protector = dataProtectionProvider
                .CreateProtector(dataProtectionPurposeStrings.IdRouteValue);
        }

        private IQueryable<CompanyContactIndexViewModel> GetCompanyContactsIndex()
        {
            IQueryable<CompanyContact> query = _context.CompanyContacts
                .Select(companyContact => companyContact);

            return query.ProjectTo<CompanyContactIndexViewModel>(_mapper.ConfigurationProvider);
        }

        public async Task<IPaginatedList<CompanyContactIndexViewModel>> GetCompanyContactsIndexPaginatedAsync(int pageSize, int pageIndex, string searchString, string sortOrder)
        {
            var contactsQuery = GetCompanyContactsIndex();

            contactsQuery = SearchContactIndex(contactsQuery, searchString);

            contactsQuery = SortContactIndex(contactsQuery, sortOrder);

            return await PaginatedList<CompanyContactIndexViewModel>.CreateAsync(contactsQuery.AsNoTracking(), pageIndex, pageSize);
        }

        public async Task<EntityEntry<CompanyContact>> PostCompanyContactCreateAsync(CompanyContact companyContact)
        {
            EntityEntry<CompanyContact> newCompanyContact = await _context.CompanyContacts.AddAsync(companyContact);
            newCompanyContact.State = EntityState.Added;
            return newCompanyContact;
        }

        public async Task<CompanyContactDetailsViewModel> GetCompanyContactDetailsAsync(string encryptedID)
        {
            int decryptedID = Convert.ToInt32(_protector.Unprotect(encryptedID));

            IQueryable<CompanyContact> query = _context.CompanyContacts
                .Where(contact => contact.ID == decryptedID);

            return await query.ProjectTo<CompanyContactDetailsViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public async Task<CompanyContactEditViewModel> GetCompanyContactEditAsync(string encryptedID)
        {
            int decryptedID = Convert.ToInt32(_protector.Unprotect(encryptedID));

            IQueryable<CompanyContact> query = _context.CompanyContacts
                .Where(companyContact => companyContact.ID == decryptedID);

            CompanyContactEditViewModel companyContactEditViewModel = await query.ProjectTo<CompanyContactEditViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();

            return companyContactEditViewModel;
        }

        public EntityEntry<CompanyContact> PostCompanyContactEdit(CompanyContact contactChanges)
        {
            EntityEntry<CompanyContact> companyContact = _context.Entry<CompanyContact>(contactChanges);
            companyContact.State = EntityState.Modified;
            return companyContact;
        }

        public async Task<int> DeleteCompanyContactByEncryptedIDAsync(string encryptedID, CancellationToken token)
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

            return await SaveChangesAsync(token);
        }

        public async Task<CompanyContact> GetCompanyContactById(int id)
        {
            return await _context.CompanyContacts.FindAsync(id);
        }

        public SelectList CompanyContactSelectList(string? encryptedTrainingCenterID, int? companyContactID)
        {
            var companyContactsIQ = _context.CompanyContacts.Where(x => true);

            if(encryptedTrainingCenterID != null)
            {
                int trainingCenterID = Convert.ToInt32(_protector.Unprotect(encryptedTrainingCenterID));
                companyContactsIQ = companyContactsIQ
                    .Where(companyContacts =>
                           companyContacts.Company.TrainingCenter.ID == trainingCenterID);   //of the same Company
                if(companyContactID != null)
                {
                    companyContactsIQ = companyContactsIQ
                        .Where(companyContacts =>
                            companyContacts.ListTrainingCenter == null ||                               //not listed in ListTrainingCenter
                            companyContacts.ListTrainingCenter.CompanyContactID == companyContactID);   //actual 
                }
                else
                {
                    companyContactsIQ = companyContactsIQ
                    .Where(companyContacts =>
                        companyContacts.ListTrainingCenter == null); //not listed in ListTrainingCenter
                };
            }

            var companyContacts = companyContactsIQ
                .OrderBy(companyContact => companyContact.Contact.LastName).ThenBy(companyContact => companyContact.Contact.FirstName)
                .Select(companyContact => new {
                    ID = companyContact.ID,
                    NameEmail = (companyContact.Contact.FirstName??"") + (((companyContact.Contact.FirstName != null) && (companyContact.Contact.LastName != null))?" ":"") + 
                                (companyContact.Contact.LastName??"") + (((companyContact.Contact.LastName != null) && (companyContact.Email != null)) ? " " : "") + 
                                (companyContact.Email??"")
                });

            return new SelectList(companyContacts, nameof(CompanyContact.ID), "NameEmail");
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
