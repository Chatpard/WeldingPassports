using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.SQL
{
    public interface ICompaniesRepository
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync();
        
        Task<Company> GetCompanyByNameAsync(string companyName);
        
        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}
