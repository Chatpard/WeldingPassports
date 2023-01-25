using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Requests.API.TrainingCentersAPI
{
    public class GetTrainingCenterLetterByTrainingCenterIdApiRequest : IRequest<ActionResult<char?>>
    {
        public GetTrainingCenterLetterByTrainingCenterIdApiRequest(int companyID, ControllerBase controller)
        {
            CompanyID = companyID;
            Controller = controller;
        }

        public int CompanyID { get; }
        public ControllerBase Controller { get; }
    }
}