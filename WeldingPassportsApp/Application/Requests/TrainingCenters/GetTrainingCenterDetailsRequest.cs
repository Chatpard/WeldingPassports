using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.TrainingCenters
{
    public class GetTrainingCenterDetailsRequest : IRequest<IActionResult>
    {
        public string EncryptedID { get; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }

        public GetTrainingCenterDetailsRequest(string encryptedID, string returnUrl, Controller controller)
        {
            EncryptedID = encryptedID;
            ReturnUrl = returnUrl;
            Controller = controller;
        }
    }
}
