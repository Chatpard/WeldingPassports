﻿using Application.Interfaces.Repositories.API;
using Microsoft.AspNetCore.Mvc.Rendering;
using Infrastructure.Services.Persistence;
using System.Linq;
using Domain.Models;

namespace Infrastructure.Repositories.API
{
    public class CompanyContactsAPIRepository : ICompanyContactsAPIRepository
    {
        private readonly AppDbContext _context;

        public CompanyContactsAPIRepository(AppDbContext context)
        {
            _context = context;
        }

        public SelectList GetCompanyContactsFromCompanySelectList(int? companyID = null)
        {
            var companyContactsQ = _context.CompanyContacts
                .Where(companyContacts => true);
            if(companyID != null)
            {
                companyContactsQ = companyContactsQ.Where(companyContact => companyContact.CompanyID == companyID);
            }
            var companyContacts = companyContactsQ
                .Select(companyContact => new {
                    ID = companyContact.ID,
                    NameEmail = (companyContact.Contact.FirstName ?? "") + (((companyContact.Contact.FirstName != null) && (companyContact.Contact.LastName != null)) ? " " : "") +
                                (companyContact.Contact.LastName ?? "") + (((companyContact.Contact.LastName != null) && (companyContact.Email != null)) ? " <" : "") +
                                (companyContact.Email ?? "") + ((companyContact.Email != null) ? ">" : "")
                });

            return new SelectList(companyContacts, nameof(CompanyContact.ID), "NameEmail");
        }
    }
}
