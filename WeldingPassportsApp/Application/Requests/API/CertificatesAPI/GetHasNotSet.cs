using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.API.CertificatesAPI
{
    public class GetHasNotSet: IRequest<ActionResult<bool?>>
    {
        public GetHasNotSet(int? pePassortID, int? processID, ControllerBase controller)
        {
            PePassortID=pePassortID;
            ProcessID=processID;
            Controller=controller;
        }

        public int? PePassortID { get; }
        public int? ProcessID { get; }
        public ControllerBase Controller { get; }
    }
}
