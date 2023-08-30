using Application.Interfaces.Repositories.SQL;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.CompanyContacts
{
    public class PostCompanyContactEditRequestHandler : IRequestHandler<PostCompanyContactEditRequest, IActionResult>
    {
        private readonly ICompanyContactsSQLRepository _companyContactsSQLRepository;
        private readonly IContactsSQLRepository _contactsSQLRepository;
        private readonly ICompaniesSQLRepository _companiesSQLRepository;
        private readonly IAddressesSQLRepository _addressesSQLRepository;
        private readonly IUsersSQLRepository _appUsersSQLRepository;
        private readonly IAppRolesSQLRepository _appRolesSQLRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public PostCompanyContactEditRequestHandler(
            ICompanyContactsSQLRepository companyContactsSQLRepository, 
            IContactsSQLRepository contactsSQLRepository,
            ICompaniesSQLRepository companiesSQLRepository,
            IAddressesSQLRepository addressesSQLRepository,
            IUsersSQLRepository appUsersSQLRepository,
            IAppRolesSQLRepository appRolesSQLRepository,
            UserManager<AppUser> userManager, IMapper mapper)
        {
            _companyContactsSQLRepository = companyContactsSQLRepository;
            _contactsSQLRepository=contactsSQLRepository;
            _companiesSQLRepository=companiesSQLRepository;
            _addressesSQLRepository=addressesSQLRepository;
            _appUsersSQLRepository=appUsersSQLRepository;
            _appRolesSQLRepository=appRolesSQLRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(PostCompanyContactEditRequest request, CancellationToken cancellationToken)
        {
            request.Controller.ModelState.Remove("submit");
            if (request.Controller.ModelState.IsValid)
            {
                CompanyContact companyContact = _mapper.Map<CompanyContact>(request.ContactChanges);
                AppRole appRole = _mapper.Map<AppRole>(request.ContactChanges);

                await _companyContactsSQLRepository.PostCompanyContactEdit(companyContact, appRole);
            
                await _companyContactsSQLRepository.SaveChangesAsync(cancellationToken);
                
                return request.Controller.LocalRedirect(request.ReturnUrl);
            }

            if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

            request.Controller.ViewBag.CurrentUrl = request.CurrentUrl;

            request.ContactChanges.ContactSelectList = _contactsSQLRepository.ContactSelectList();
            request.ContactChanges.CompanySelectList = _companiesSQLRepository.CompanySelectList();
            request.ContactChanges.AddressSelectList = _addressesSQLRepository.AddressSelectList();
            request.ContactChanges.AppUsersSelectList = _appUsersSQLRepository.AppUsersSelectList(request.ContactChanges.AppUserID);
            request.ContactChanges.RoleNamesSelectList = _appRolesSQLRepository.RoleNamesSelectList(request.ContactChanges.CompanyID);

            return request.Controller.View(request.ContactChanges);
       }
    }
}
