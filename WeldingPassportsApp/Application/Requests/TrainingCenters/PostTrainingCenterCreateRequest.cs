using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Requests
{
    public class PostTrainingCenterCreateRequest : IRequest<ActionResult>
    {
        public PostTrainingCenterCreateRequest(TrainingCenterCreateViewModel trainingCenterCreateVM, string nameOfDetailsAction, string returnUrl, Controller controller)
        {
            TrainingCenterCreateVM = trainingCenterCreateVM;
            NameOfDetailsAction = nameOfDetailsAction;
            ReturnUrl = returnUrl;
            Controller = controller;
        }

        public TrainingCenterCreateViewModel TrainingCenterCreateVM { get; }
        public string NameOfDetailsAction { get; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }
    }
}