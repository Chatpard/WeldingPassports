using Application.Interfaces;
using Application.Interfaces.Repositories.SQL;
using Application.Security;
using Domain.Models;
using Infrastructure.Services.Persistence;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQL
{
    public class RevokeSQLRepository: SaveChangesSQL, IRevokeSQLRepository
    {
        private readonly IDataProtector _protector;
        private readonly AppDbContext _context;

        public RevokeSQLRepository(AppDbContext context, IDataProtectionProvider dataProtectionProvider, IDataProtectionPurposeStrings dataProtectionPurposeStrings): base(context)
        {
            _protector=dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.IdRouteValue);
            _context=context;
        }

        public async Task<EntityEntry<Revoke>> DeleteByCertificateEncryptedID(string certificateEncryptedID)
        {
            int certificateDecryptedID = Convert.ToInt32(_protector.Unprotect(certificateEncryptedID));
            Revoke revoke = await _context.Revokes.Where(revoke => revoke.RegistrationID == certificateDecryptedID).SingleOrDefaultAsync();
            if(revoke == null) { return null; }

            EntityEntry<Revoke> revokeEntityEntry = _context.Entry(revoke);
            if (revokeEntityEntry == null) { return null; }

            revokeEntityEntry.State = EntityState.Deleted;
            return revokeEntityEntry;
        }
    }
}
