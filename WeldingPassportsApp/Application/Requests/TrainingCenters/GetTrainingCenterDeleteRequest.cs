using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.TrainingCenters
{
    public class GetTrainingCenterDeleteRequest : IRequest<IActionResult>
    {
        public string EncryptedID { get; set; }
        public string ReturnUrl { get; set; }
        public Controller Controller { get; }

        public GetTrainingCenterDeleteRequest(string encryptedID, string returnUrl, Controller controller)
        {
            EncryptedID = encryptedID;
            ReturnUrl = returnUrl;
            Controller = controller;
        }
    }
}
