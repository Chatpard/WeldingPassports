using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.API
{
    public interface IPEWeldersAPIRepository
    {
        public SelectList GetWeldersNotFromTrainingCenterID(int? TrainingCenterID);
    }
}
