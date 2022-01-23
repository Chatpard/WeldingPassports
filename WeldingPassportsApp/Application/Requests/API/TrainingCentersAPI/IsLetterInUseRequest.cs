using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.API.TrainingCentersAPI
{
    public class IsLetterInUseRequest : IRequest<JsonResult>
    {
        public IsLetterInUseRequest(char letter)
        {
            Letter = letter;
        }

        public char Letter { get; }
    }
}
