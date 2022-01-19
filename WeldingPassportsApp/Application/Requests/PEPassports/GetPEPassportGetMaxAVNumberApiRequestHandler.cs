using Application.Interfaces.Repositories.API;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.PEPassports
{
    class GetPEPassportGetMaxAVNumberApiRequestHandler : IRequestHandler<GetPEPassportGetMaxAVNumberApiRequest, ActionResult<string>>
    {
        private readonly IPEPassportsAPIRepository _peWeldersAPIRepository;

        public GetPEPassportGetMaxAVNumberApiRequestHandler(IPEPassportsAPIRepository peWeldersAPIRepository)
        {
            _peWeldersAPIRepository = peWeldersAPIRepository;
        }

        public async System.Threading.Tasks.Task<ActionResult<string>> Handle(GetPEPassportGetMaxAVNumberApiRequest request, CancellationToken cancellationToken)
        {
            string? MaxAVNumber =  _peWeldersAPIRepository.GetMaxAVNumber(request.TrainingCenterID);

            if(MaxAVNumber == null)
            {
                MaxAVNumber = String.Empty;
            }

            return request.Controller.Ok(MaxAVNumber);
        }
    }
}
