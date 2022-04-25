using System.Linq;
using Gamification.Shared.Core.Entities;
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

            builder.Entity<User>(entity =>
            {
                entity.ToTable(name: "Users");
            });

            builder.Entity<Role>(entity =>
            {
                entity.ToTable(name: "Roles");
            });

            builder.Entity<RoleClaim>(entity =>
            {
                entity.ToTable(name: "RoleClaims");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleClaims)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        public static void ApplyModuleConfiguration(this ModelBuilder builder, PersistenceSettings persistenceOptions)
        {
            // build model for MSSQL
        }
    }
}