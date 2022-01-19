using Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.SQL
{
    public interface IListTrainingCentersSQLRepository
    {
        Task<EntityEntry<ListTrainingCenter>> Create(int trainingCenterID, int companyContactID);

        Task<EntityEntry<ListTrainingCenter>> Edit(int trainingCenterID, int companyContactID);
    }
}
