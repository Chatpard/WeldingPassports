using Application.Interfaces.Repositories.API;
using Application.Interfaces.Repositories.SQL;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.API
{
    public class PEWeldersAPIRepository : IPEWeldersAPIRepository
    {
        private readonly IPEWeldersSQLRepository _peWeldersSQLRepository;

        public PEWeldersAPIRepository(IPEWeldersSQLRepository peWeldersSQLRepository)
        {
            _peWeldersSQLRepository = peWeldersSQLRepository;
        }

        public SelectList GetWeldersNotFromTrainingCenterID(int? trainingCenterID)
        {
            return _peWeldersSQLRepository.PEWelderSelectList(trainingCenterID) ;
        }
    }
}
