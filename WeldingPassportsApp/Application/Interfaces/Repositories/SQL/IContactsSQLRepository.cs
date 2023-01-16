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
    public interface IContactsSQLRepository
    {
        Task<IPaginatedList<CompanyContactIndexViewModel>> GetContactsIndexPaginatedAsync(int pageSize, int pageIndex, string searchString, string sortOrder);

        Task<EntityEntry<Contact>> PostContactCreateAsync(Contact contact);

        Task<CompanyContactDetailsViewModel> GetContactDetailsAsync(string encryptedID);

        Task<ContactEditViewModel> GetContactEditAsync(string encryptedID);

        EntityEntry<Contact> PostContactEdit(Contact contactChanges);

        Task<int> DeleteContactByEncryptedIDAsync(string encryptedID, CancellationToken token);

        SelectList ContactSelectList();

        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}
