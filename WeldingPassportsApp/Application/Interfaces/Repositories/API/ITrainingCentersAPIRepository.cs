using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.API
{
    public interface ITrainingCentersAPIRepository
    {
        Task<char?> GetLetterByTrainingCenterID(int id);

        bool IsLetterInUse(char letter);
    }
}
