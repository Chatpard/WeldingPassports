using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Repositories.API
{
    public interface IPEPassportsAPIRepository
    {
        int GetMaxAVNumber(int trainingCenterID);
    }
}
