using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Repositories.API
{
    public interface ICompanyContactsAPIRepository
    {
        SelectList GetCompanyContactsFromCompanySelectList(int? companyID = null);
    }
}
