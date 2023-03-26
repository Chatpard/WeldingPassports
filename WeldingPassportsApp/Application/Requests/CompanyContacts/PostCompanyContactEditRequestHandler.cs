using Application.Interfaces.Repositories.SQL;
using AutoMapper;
using Domain.Models;
using MediatR;
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
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public PostCompanyContactEditRequestHandler(ICompanyContactsSQLRepository companyContactsSQLRepository, UserManager<AppUser> userManager, IMapper mapper)
        {
            _companyContactsSQLRepository = companyContactsSQLRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(PostCompanyContactEditRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

            CompanyContact companyContact = _mapper.Map<CompanyContact>(request.ContactChanges);
            AppRole appRole = _mapper.Map<AppRole>(request.ContactChanges);

            await _companyContactsSQLRepository.PostCompanyContactEdit(companyContact, appRole);
            
            await _companyContactsSQLRepository.SaveChangesAsync(cancellationToken);
                
            return request.Controller.LocalRedirect(request.ReturnUrl);
       }
    }
}
