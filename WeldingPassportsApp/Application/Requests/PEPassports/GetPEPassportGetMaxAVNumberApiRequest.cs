using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.PEPassports
{
    public class GetPEPassportGetMaxAVNumberApiRequest : IRequest<ActionResult<string>>
    {
        public GetPEPassportGetMaxAVNumberApiRequest(int trainingCenterID, ControllerBase controller)
        {
            TrainingCenterID = trainingCenterID;
            Controller = controller;
        }

        public int TrainingCenterID { get; }
        public ControllerBase Controller { get; }
    }
}
