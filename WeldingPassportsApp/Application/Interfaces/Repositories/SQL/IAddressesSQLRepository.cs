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
    public interface IAddressesSQLRepository
    {
        Task<EntityEntry<Address>> PostAddressCreateAsync(Address address);

        SelectList AddressSelectList();

        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}
