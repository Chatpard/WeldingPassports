using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Controllers
{
    public interface IExamCentersApiController
    {
        bool IsCompanyIDInUse(int companyID);
    }
}
