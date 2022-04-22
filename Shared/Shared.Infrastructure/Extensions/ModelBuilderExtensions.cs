using System.Linq;
using Gamification.Shared.Core.Settings;
using Microsoft.EntityFrameworkCore;

namespace Gamification.Shared.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyApplicationConfiguration(this ModelBuilder builder, PersistenceSettings persistenceOptions)
        {
            if (persistenceOptions.UseMsSql)
            {
                foreach (var property in builder.Model.GetEntityTypes()
                    .SelectMany(t => t.GetProperties())
                    .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
                {
                    property.SetColumnType("decimal(23,2)");
                }
            }
        }

        public static void ApplyModuleConfiguration(this ModelBuilder builder, PersistenceSettings persistenceOptions)
        {
            // build model for MSSQL
        }
    }
}