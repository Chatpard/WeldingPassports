using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Requests.TrainingCenters
{
    public class GetTrainingCenterLetterByTrainingCenterIdApiRequest : IRequest<ActionResult<char?>>
    {
        public GetTrainingCenterLetterByTrainingCenterIdApiRequest(int trainingCenterID, ControllerBase controller)
        {
            TrainingCenterID = trainingCenterID;
            Controller = controller;
        }

        public int TrainingCenterID { get; }
        public ControllerBase Controller { get; }
    }
}