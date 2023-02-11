using Application.Interfaces.Repositories.SQL;
using Application.Security;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Examinations
{
    public class PostExaminationsCreateRequestHandler : IRequestHandler<PostExaminationsCreateRequest, IActionResult>
    {
        private readonly IExaminationsSQLRepository _repository;
        private readonly IDataProtector _protector;

        public PostExaminationsCreateRequestHandler(IExaminationsSQLRepository repository, IDataProtectionProvider dataProtectionProvider, IDataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            _repository = repository;
            _protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.IdRouteValue);
        }

        public async Task<IActionResult> Handle(PostExaminationsCreateRequest request, CancellationToken cancellationToken)
        {
            EntityEntry<Examination> examination = await _repository.PostExaminationCreateAsync(request.Vm, cancellationToken);
            string encryptedExaminationID = _protector.Protect(examination.Entity.ID.ToString());

            request.Controller.TempData["EncryptedExaminationID"] = encryptedExaminationID;
            request.Controller.TempData["ReturnUrl"] = request.Controller.Url.Action(request.ExaminationEditActionName, request.ExaminationsControllerName, new { id = encryptedExaminationID, returnUrl = request.ReturnUrl });

            return request.Controller.LocalRedirect(request.ReturnUrl);
        }
    }
}
