using Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.SQL
{
    public interface IAppSettingsSQLRepository
    {
        Task<IEnumerable<AppSettings>> GetAllAppSettingsAsync();
        Task<AppSettings> GetAppsetingsByIDAsync(int AppSettingsID);
        Task<AppSettings> GetAppsetingsAsync();
        EntityEntry<AppSettings> InsertAppsettings(AppSettings appSettings);
        Task<EntityEntry<AppSettings>> DeleteAppsettingsAsync(int AppSettingsID);
        EntityEntry<AppSettings> UpdateAppSettings(AppSettings appSettingsChanges);
        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}
