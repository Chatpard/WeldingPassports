using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.API
{
    public interface ITrainingCentersAPIRepository
    {
        Task<char?> GetLetterById(int id);
    }
}
