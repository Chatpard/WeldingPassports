using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Requests.API.ExamCentersAPI
{
    public class IsCompanyIDInUseRequest: IRequest<JsonResult>
    {
        public IsCompanyIDInUseRequest(int companyID)
        {
            CompanyID=companyID;
        }

        public int CompanyID { get; }
    }
}
