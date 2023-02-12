using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Requests.API.TrainingCentersAPI
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