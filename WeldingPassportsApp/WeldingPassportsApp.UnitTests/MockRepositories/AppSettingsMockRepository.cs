using Application.Interfaces.Repositories.SQL;
using Domain.Models;
using Infrastructure.SeedData;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WeldingPassportsApp.UnitTests.MockRepositories
{
    public class AppSettingsMockRepository : IAppSettingsSQLRepository
    {
        private readonly List<AppSettings> _appSettingsList;

        public AppSettingsMockRepository()
        {
            _appSettingsList = new List<AppSettings>(SeedDataStore.GetSeedData<AppSettings>());
        }
        public Task<IEnumerable<AppSettings>> GetAllAppSettingsAsync()
        {
            return Task.FromResult<IEnumerable<AppSettings>>(_appSettingsList);
        }

        public Task<EntityEntry<AppSettings>> DeleteAppsettingsAsync(int AppSettingsID)
        {
            throw new NotImplementedException();
        }

        public Task<AppSettings> GetAppsetingsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AppSettings> GetAppsetingsByIDAsync(int AppSettingsID)
        {
            throw new NotImplementedException();
        }

        public EntityEntry<AppSettings> InsertAppsettings(AppSettings appSettings)
        {
            throw new NotImplementedException();
        }

        public EntityEntry<AppSettings> UpdateAppSettings(AppSettings appSettingsChanges)
        {
            throw new NotImplementedException();
        }
        public Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
