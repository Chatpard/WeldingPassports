using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.API.PEWeldersAPI
{
    public class GetWeldersNotFromTrainingCenterRequest : IRequest<ActionResult<SelectList>>
    {
        public GetWeldersNotFromTrainingCenterRequest(int? trainingCenterID)
        {
            TrainingCenterID = trainingCenterID;
        }

        public int? TrainingCenterID { get; }
    }
}
