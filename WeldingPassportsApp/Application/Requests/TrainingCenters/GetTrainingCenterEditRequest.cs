using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.TrainingCenters
{
    public class GetTrainingCenterEditRequest : IRequest<ActionResult>
    {
        public GetTrainingCenterEditRequest(string encryptedID, string returnUrl, Controller controller)
        {
            EncryptedID = encryptedID;
            ReturnUrl = returnUrl;
            Controller = controller;
        }

        public string EncryptedID { get; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }
    }
}
