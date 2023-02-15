using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Requests.API.PEPassportsAPI
{
    public class IsAVNumberAvailableRequest: IRequest<ActionResult<bool>>
    {
        public IsAVNumberAvailableRequest(int avNumber, char letter, ControllerBase controller)
        {
            AVNumber = avNumber;
            Letter = letter;
            Controller = controller;
        }

        public int AVNumber { get; }
        public char Letter { get; }
        public ControllerBase Controller { get; }
    }
}
