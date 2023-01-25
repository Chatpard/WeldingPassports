using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Repositories.API
{
    public interface IExamCentersAPIRepository
    {
        bool IsCompanyIDInUse(int companyID);
    }
}
