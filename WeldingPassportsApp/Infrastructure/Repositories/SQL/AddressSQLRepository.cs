using Application.Interfaces;
using Application.Interfaces.Repositories.SQL;
using Application.Requests.Addresses;
using Application.Security;
using Application.SQLModels;
using Application.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models;
using Infrastructure.Services.Persistence;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQL
{
    public class AddressesSQLRepository : SaveChangesSQL, IAddressesSQLRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDataProtector _protector;

        public AddressesSQLRepository(AppDbContext context, IMapper mapper,
            IDataProtectionProvider dataProtectionProvider, IDataProtectionPurposeStrings dataProtectionPurposeStrings) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _protector = dataProtectionProvider
                .CreateProtector(dataProtectionPurposeStrings.IdRouteValue);
        }

        public async Task<AddressEditViewModel> GetAddressEditAsync(string encryptedID)
        {
            if(!int.TryParse(encryptedID, out int decryptedID))
            {
                decryptedID = Convert.ToInt32(_protector.Unprotect(encryptedID));
            }

            IQueryable<Address> query = _context.Addresses
                .Where(address => address.ID == decryptedID);

            return await query.ProjectTo<AddressEditViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public EntityEntry<Address> PostAddressEdit(Address addressChanges)
        {
            EntityEntry<Address> address = _context.Entry(addressChanges);
            address.State = EntityState.Modified;
            return address;
        }
        public async Task<EntityEntry<Address>> PostAddressCreateAsync(Address address)
        {
            EntityEntry<Address> newAddress = await _context.Addresses.AddAsync(address);
            newAddress.State = EntityState.Added;
            return newAddress;
        }

        public SelectList AddressSelectList()
        {
            var address = _context.Addresses
                .OrderBy(address => address.BusinessAddress)
                .Select(address => new AddressSelectListSQLModel { 
                    ID = address.ID,
                    AddressPostCodeCity = address.BusinessAddress + " " + address.BusinessAddressPostalCode + " " + address.BusinessAddressCity
                });

            return new SelectList(address, nameof(AddressSelectListSQLModel.ID), nameof(AddressSelectListSQLModel.AddressPostCodeCity));
        }
    }
}
