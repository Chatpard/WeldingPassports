using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Requests.API.CertificatesAPI
{
    public class GetCompanyIDByPEPassportRequest : IRequest<ActionResult<int?>>
    {
        public GetCompanyIDByPEPassportRequest(int? pePassportID, ControllerBase controller)
        {
            PePassportID=pePassportID;
            Controller=controller;
        }

        public int? PePassportID { get; }
        public ControllerBase Controller { get; }
    }
}
