using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.API.CompanyContactsAPI
{
    public class GetCompanyContactsFromCompanyRequest : IRequest<ActionResult<SelectList>>
    {
        public GetCompanyContactsFromCompanyRequest(int companyID)
        {
            CompanyID = companyID;
        }

        public int CompanyID { get; }
    }
}
