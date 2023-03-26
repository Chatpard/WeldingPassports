using Application.Interfaces.Repositories.SQL;
using Application.Security;
using AutoMapper;
using Domain.Models;
using Infrastructure.Services.Persistence;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
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
    public class UserToApproveSQLRepository : SaveChangesSQL, IUserToApproveRepository  
    {
        private readonly AppDbContext _context;
        private readonly ICompaniesSQLRepository _companiesSQLRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IDataProtector _protector;

        public UserToApproveSQLRepository(AppDbContext context, ICompaniesSQLRepository companiesSQLRepository,
            UserManager<AppUser> userManager, IMapper mapper,
            IDataProtectionProvider dataProtectionProvider,
            IDataProtectionPurposeStrings dataProtectionPurposeStrings) : base(context)
        {
            _context = context;
            _companiesSQLRepository = companiesSQLRepository;
            _userManager = userManager;
            _mapper = mapper;
            _protector = dataProtectionProvider
                .CreateProtector(dataProtectionPurposeStrings.IdRouteValue);
        }

        public async Task<IEnumerable<UserToApprove>> GetAllUsersToApproveAsync()
        {
            return await _context.UsersToApprove.ToListAsync();
        }

        public async Task<IEnumerable<UserToApproveIndex>> GetUsersToApproveIndexAsync()
        {
            return (await _context.UsersToApprove
                .Where(u => u.EmailConfirmed == true)
                .Select(u => _mapper.Map<UserToApproveIndex>(u))
                .Select(u => EncryptUserToApproveIndexID(u, _protector))
                .ToListAsync());
        }

        public async Task<UserToApprove> GetUserToApproveByEncryptedIDAsync(string userToApproveEncryptedID)
        {
            return EncryptUserToApproveID(
                await _context.UsersToApprove.FindAsync(Convert.ToInt32(_protector.Unprotect(userToApproveEncryptedID)))
                );
        }

        public async Task<UserToApprove> GetUserToApproveByEmailAsync(string email)
        {
            return await _context.UsersToApprove.SingleOrDefaultAsync(u => u.Email == email);
        }

        public EntityEntry<UserToApprove> InsertUserToApprove(UserToApprove userToApprove)
        {
            return _context.UsersToApprove.Add(userToApprove);
        }

        public EntityEntry<UserToApprove> UpdateUserToApprove(UserToApprove userToApproveChanges)
        {
            EntityEntry<UserToApprove> userToApprove = _context.Entry(userToApproveChanges);
            userToApprove.State = EntityState.Modified;
            return userToApprove;
        }

        public async Task<EntityEntry<UserToApprove>> DeleteUserToApproveByEncryptedIDAsync(string userToApproveEncryptedID)
        {
            return _context.Remove(await _context.UsersToApprove.FindAsync(Convert.ToInt32(_protector.Unprotect(userToApproveEncryptedID))));
        }

        public async Task<EntityEntry<UserToApprove>> DeleteUserToApproveByIDAsync(int userToApproveID)
        {
            return _context.Remove(await _context.UsersToApprove.FindAsync(userToApproveID));
        }

        public async Task<AppUser> InsertAppUserByEncryptedIDAsync(string userToApproveEncryptedID, string role, CancellationToken cancellationToken)
        {
            var userToApproveID = Convert.ToInt32(_protector.Unprotect(userToApproveEncryptedID));
            var userToApprove = await _context.UsersToApprove.FindAsync(userToApproveID);

            if (userToApprove != null)
            {
                var user = new AppUser { UserName = userToApprove.Email, Email = userToApprove.Email, EmailConfirmed = true };
                await _userManager.CreateAsync(user);
                await _userManager.AddToRoleAsync(user, role);
                user = await _userManager.FindByEmailAsync(user.Email);

                await DeleteUserToApproveByIDAsync(userToApproveID);
                await SaveChangesAsync(cancellationToken);

                Contact contact = new Contact()
                {
                    FirstName = userToApprove.FirstName,
                    LastName = userToApprove.LastName,
                    Birthday = userToApprove.Birthday
                };

                var company = await _companiesSQLRepository.GetCompanyByNameAsync(userToApprove.CompanyName);

                if (company == null)
                {
                    company = new Company()
                    {
                        CompanyName = userToApprove.CompanyName,
                        CompanyMainPhone = userToApprove.CompanyMainPhone,
                        CompanyEmail = userToApprove.CompanyEmail,
                        WebPage = userToApprove.WebPage
                    };
                }

                Address address = new Address()
                {
                    BusinessAddress = userToApprove.BusinessAddress,
                    BusinessAddressPostalCode = userToApprove.BusinessAddressPostalCode,
                    BusinessAddressCity = userToApprove.BusinessAddressCity,
                    BusinessAddressCountry = userToApprove.BusinessAddressCountry
                };

                CompanyContact companyContact = new CompanyContact()
                {
                    BusinessPhone = userToApprove.BusinessPhone,
                    MobilePhone = userToApprove.MobilePhone,
                    Email = userToApprove.Email,
                    Contact = contact,
                    Company = company,
                    AppUser = user,
                    Address = address
                };

                _context.CompanyContacts.Add(companyContact);

                return user;
            }

            return null;
        }

        public UserToApprove EncryptUserToApproveID(UserToApprove userToApprove)
        {
            userToApprove.EncryptedId = _protector.Protect(userToApprove.ID.ToString());

            return userToApprove;
        }

        public async Task<string> GetUserToApproveEmailLanguageByEncryptedIDAsync(string userToApproveEncryptedID)
        {
            return (await GetUserToApproveByEncryptedIDAsync(userToApproveEncryptedID)).EmailLanguage;
        }

        private static UserToApproveIndex EncryptUserToApproveIndexID(UserToApproveIndex userToApproveIndex, IDataProtector protector)
        {
            userToApproveIndex.EncryptedId = protector.Protect(userToApproveIndex.ID.ToString());
            return userToApproveIndex;
        }
    }
}
