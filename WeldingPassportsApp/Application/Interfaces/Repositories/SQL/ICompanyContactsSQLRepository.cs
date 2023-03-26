using Application.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.SQL
{
    public interface ICompanyContactsSQLRepository
    {
        Task<IPaginatedList<CompanyContactIndexViewModel>> GetCompanyContactsIndexPaginatedAsync(int pageSize, int pageIndex, string searchString, string sortOrder);

        Task<EntityEntry<CompanyContact>> PostCompanyContactCreateAsync(CompanyContact companyContact);

        Task<CompanyContactDetailsViewModel> GetCompanyContactDetailsAsync(string encryptedID);

        Task<CompanyContactEditViewModel> GetCompanyContactEditAsync(string encryptedID);

        Task<EntityEntry<CompanyContact>> PostCompanyContactEdit(CompanyContact contactChanges, AppRole appRole);

        Task<int> DeleteCompanyContactByEncryptedIDAsync(string encryptedID, CancellationToken token);

        Task<CompanyContact> GetCompanyContactById(int id);

        SelectList CompanyContactSelectList(string? encryptedCompanyID, int? companyContactID);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
