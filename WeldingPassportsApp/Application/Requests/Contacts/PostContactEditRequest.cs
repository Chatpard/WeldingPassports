using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.ViewModels;

namespace Application.Requests.Contacts
{
    public class PostContactEditRequest : IRequest<IActionResult>
    {
        public PostContactEditRequest(ContactEditViewModel contactChanges, string nameOfDetailsAction, string returnUrl, Controller controller )
        {
            ContactChanges=contactChanges;
            NameOfDetailsAction=nameOfDetailsAction;
            ReturnUrl=returnUrl;
            Controller=controller;
        }

        public ContactEditViewModel ContactChanges { get; }
        public string NameOfDetailsAction { get; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }
    }
}
