using Application.Interfaces.Repositories.SQL;
using Domain.Models;
using Infrastructure.Services.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.DataProtection;
using System.Linq;
using Application.Security;
using System.Threading;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Application.SQLModels;

namespace Infrastructure.Repositories.SQL
{
    public class CompaniesSQLRepository : SaveChangesSQL, ICompaniesSQLRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDataProtector _protector;

        public CompaniesSQLRepository(AppDbContext context, IMapper mapper,
            IDataProtectionProvider dataProtectionProvider, IDataProtectionPurposeStrings dataProtectionPurposeStrings) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _protector = dataProtectionProvider
                .CreateProtector(dataProtectionPurposeStrings.IdRouteValue);
        }

        private IQueryable<CompanyIndexViewModel> GetCompaniesIndex()
        {
            return _context.Companies.ProjectTo<CompanyIndexViewModel>(_mapper.ConfigurationProvider);
        }

        public async Task<IPaginatedList<CompanyIndexViewModel>> GetCompaniesIndexPaginatedAsync(int pageSize, int pageIndex, string searchString, string sortOrder)
        {
            var companiesQuery = GetCompaniesIndex();

            companiesQuery = SearchCompanyIndex(companiesQuery, searchString);

            companiesQuery = SortCompanyIndex(companiesQuery, sortOrder);

            return await PaginatedList<CompanyIndexViewModel>.CreateAsync(companiesQuery.AsNoTracking(), pageIndex, pageSize);
        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company> GetCompanyByNameAsync(string companyName)
        {
            return await _context.Companies.SingleOrDefaultAsync(c => c.CompanyName == companyName);
        }

        public async Task<CompanyDetailsViewModel> GetCompanyDetailsAsync(string encryptedID)
        {
            int decryptedID = Convert.ToInt32(_protector.Unprotect(encryptedID));

            IQueryable<Company> query = _context.Companies
                .Where(company => company.ID == decryptedID);

            return await query.ProjectTo<CompanyDetailsViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public async Task<EntityEntry<Company>> PostCompanyCreateAsync(Company company)
        {
            EntityEntry<Company> newCompany = await _context.Companies.AddAsync(company);
            newCompany.State = EntityState.Added;
            return newCompany;
        }

        public async Task<CompanyEditViewModel> GetCompanyEditAsync(string encryptedID)
        {
            if (!int.TryParse(encryptedID, out int decryptedID))
            {
                decryptedID = Convert.ToInt32(_protector.Unprotect(encryptedID));
            }

            IQueryable<Company> query = _context.Companies
                .Where(company => company.ID == decryptedID);

            return await query.ProjectTo<CompanyEditViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public EntityEntry<Company> PostCompanyEdit(Company companyChanges)
        {
            EntityEntry<Company> company = _context.Entry(companyChanges);
            company.State = EntityState.Modified;
            return company;
        }

        public async Task<int> DeleteCompanyByEncryptedIDAsync(string encryptedID, CancellationToken token)
        {
            int decryptedID = Convert.ToInt32(_protector.Unprotect(encryptedID));
            _context.Companies.Remove(new Company { ID = decryptedID });
            return await SaveAsync(token);
        }

        public SelectList CompanySelectList()
        {
            var company = _context.Companies
                .OrderBy(company => company.CompanyName)
                .Select(contact => new CompanySelectListSQLModel
                {
                    ID = contact.ID,
                    Name = contact.CompanyName,
                });https://localhost:44315/Companies

            return new SelectList(company, nameof(CompanySelectListSQLModel.ID), nameof(CompanySelectListSQLModel.Name));
        }

        public SelectList CompanyNoTrainingCentersSelectList(int? companyID = null)
        {
            var company = _context.Companies.Where(company => true);
            if (companyID == null)
            {
                company = company.Where(company => company.TrainingCenter == null);
            }
            else
            {
                company = company.Where(company => company.TrainingCenter == null || company.ID == companyID);
            };
            company = company                
                .OrderBy(contact => contact.CompanyName)
                .Select(contact => contact);

            return new SelectList(company, nameof(Company.ID), nameof(Company.CompanyName));
        }

        private static IQueryable<CompanyIndexViewModel> SortCompanyIndex(IQueryable<CompanyIndexViewModel> companiesQuery, string sortOrder)
        {
            switch (sortOrder)
            {
                case "CompanyName_desc":
                    companiesQuery = companiesQuery.OrderByDescending(company => company.CompanyName);
                    return companiesQuery;
                case "CompanyName_asc":
                case null:
                case "":
                    companiesQuery = companiesQuery.OrderBy(company => company.CompanyName);
                    return companiesQuery;
                case "CompanyMainPhone_desc":
                    companiesQuery = companiesQuery.OrderByDescending(company => company.CompanyMainPhone);
                    return companiesQuery;
                case "CompanyMainPhone_asc":
                    companiesQuery = companiesQuery.OrderBy(company => company.CompanyMainPhone);
                    return companiesQuery;
                case "CompanyEmail_desc":
                    companiesQuery = companiesQuery.OrderByDescending(company => company.CompanyEmail);
                    return companiesQuery;
                case "CompanyEmail_asc":
                    companiesQuery = companiesQuery.OrderBy(company => company.CompanyEmail);
                    return companiesQuery;
                case "WebPage_desc":
                    companiesQuery = companiesQuery.OrderByDescending(company => company.WebPage);
                    return companiesQuery;
                case "WebPage_asc":
                    companiesQuery = companiesQuery.OrderBy(company => company.WebPage);
                    return companiesQuery;
                default:
                    throw new InvalidOperationException("SortOrder nout found.");
            }
        }

        private static IQueryable<CompanyIndexViewModel> SearchCompanyIndex(IQueryable<CompanyIndexViewModel> companiesQuery, string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                companiesQuery = companiesQuery.Where(company => company.CompanyName.ToLower().Contains(searchString.ToLower())
                    || company.CompanyEmail.ToLower().Contains(searchString.ToLower())
                    || company.WebPage.ToLower().Contains(searchString.ToLower()));
            }

            return companiesQuery;
        }
    }
}
