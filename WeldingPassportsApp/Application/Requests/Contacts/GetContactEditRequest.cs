using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Requests.Contacts
{
    public class GetContactEditRequest : IRequest<IActionResult>
    {
        public string EncryptedID { get; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }

        public GetContactEditRequest(string encryptedID, string returnUrl, Controller controller)
        {
            EncryptedID = encryptedID;
            ReturnUrl = returnUrl;
            Controller = controller;
        }
    }
}
