using Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class EntityEntryExtensions
    {
        private static bool HasDependencies(this EntityEntry entityEntry)
        {
            IEntityType entityType = entityEntry.Metadata.GetRootType();
            var collectionNavigations = entityType.GetNavigations()
                .Where(nav => nav.IsCollection)
                .Concat<INavigationBase>(entityType.GetSkipNavigations());

            foreach (INavigationBase collectionNavigation in collectionNavigations)
            {
                var property = entityEntry.GetType().GetProperty(collectionNavigation.PropertyInfo.Name);
                if (property.GetValue(entityEntry) is IEnumerable<object> collection && collection.Any())
                    return true;
            }

            var referenceNavigations = entityType.GetNavigations()
                .Where(nav => !nav.IsOnDependent);

            foreach (INavigation referenceNavigation in referenceNavigations)
            {
                var property = entityEntry.GetType().GetProperty(referenceNavigation.PropertyInfo.Name);
                if (property.GetValue(entityEntry) is IEnumerable<object> collection && collection.Any())
                    return true;
            }

            return false;
        }

    }
}
