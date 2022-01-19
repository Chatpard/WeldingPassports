using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Requests.TrainingCenters
{
    public class PostTrainingCenterEditRequest : IRequest<ActionResult>
    {
        public PostTrainingCenterEditRequest(TrainingCenterEditViewModel trainingCenterChanges, string returnUrl, Controller controller)
        {
            TrainingCenterChanges = trainingCenterChanges;
            ReturnUrl = returnUrl;
            Controller = controller;
        }

        public TrainingCenterEditViewModel TrainingCenterChanges { get; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }
    }
}
