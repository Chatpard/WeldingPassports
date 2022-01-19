using Application.Interfaces.Repositories.SQL;
using Infrastructure.Services.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQL
{
    public abstract class SaveChangesSQL
    {
        private readonly AppDbContext _context;

        public SaveChangesSQL(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
