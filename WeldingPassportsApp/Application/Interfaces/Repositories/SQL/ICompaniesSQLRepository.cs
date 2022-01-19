using Application.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.SQL
{
    public interface ICompaniesSQLRepository
    {
        Task<IPaginatedList<CompanyIndexViewModel>> GetCompaniesIndexPaginatedAsync(int pageSize, int pageIndex, string searchString, string sortOrder);

        Task<IEnumerable<Company>> GetAllCompaniesAsync();

        Task<Company> GetCompanyByNameAsync(string companyName);

        Task<EntityEntry<Company>> PostCompanyCreateAsync(Company company);

        Task<CompanyDetailsViewModel> GetCompanyDetailsAsync(string encryptedID);
            
        Task<CompanyEditViewModel> GetCompanyEditAsync(string encryptedID);

        EntityEntry<Company> PostCompanyContactEdit(Company companyChanges);

        Task<int> DeleteCompanyByEncryptedIDAsync(string encryptedID, CancellationToken token);

        SelectList CompanySelectList();

        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}
