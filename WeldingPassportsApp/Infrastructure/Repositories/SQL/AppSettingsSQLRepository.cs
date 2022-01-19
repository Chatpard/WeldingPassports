using Application.Interfaces.Repositories.SQL;
using Domain.Models;
using Infrastructure.Services.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQL
{
    public class AppSettingsSQLRepository : SaveChangesSQL, IAppSettingsSQLRepository
    {
        private readonly AppDbContext _context;

        public AppSettingsSQLRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AppSettings>> GetAllAppSettingsAsync()
        {
            return await _context.AppSettings.ToListAsync();
        }
        
        public async Task<AppSettings> GetAppsetingsByIDAsync(int AppSettingsID)
        {
            return await _context.AppSettings.FindAsync(AppSettingsID);
        }
        public async Task<AppSettings> GetAppsetingsAsync()
        {
            return await _context.AppSettings.SingleAsync();
        }

        public EntityEntry<AppSettings> InsertAppsettings(AppSettings appSettings)
        {
            return _context.AppSettings.Add(appSettings);
        }

        public async Task<EntityEntry<AppSettings>> DeleteAppsettingsAsync(int appSettingsID)
        {
            return _context.Remove(await _context.AppSettings.FindAsync(appSettingsID));
        }

        public EntityEntry<AppSettings> UpdateAppSettings(AppSettings appSettingsChanges)
        {
            EntityEntry<AppSettings> appSettings = _context.Entry(appSettingsChanges);
            appSettings.State = EntityState.Modified;
            return appSettings;
        }
    }
}
