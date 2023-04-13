using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Requests.TrainingCenters
{
    public class PostTrainingCenterEditRequest : IRequest<ActionResult>
    {
        public PostTrainingCenterEditRequest(TrainingCenterEditViewModel trainingCenterEditVM, string returnUrl, Controller controller)
        {
            TrainingCenterEditVM = trainingCenterEditVM;
            ReturnUrl = returnUrl;
            Controller = controller;
        }

        public TrainingCenterEditViewModel TrainingCenterEditVM { get; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }
    }
}
