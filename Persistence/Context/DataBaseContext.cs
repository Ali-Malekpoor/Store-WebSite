using Application.Interfaces.Contexts;
using Domain.Attributes;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public class DataBaseContext : DbContext, IDataBaseContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.GetCustomAttributes(typeof(AuditableAttribute),true).Length > 0)
                {
                    modelBuilder.Entity(entityType.Name).Property<DateTime>("InsertTime");
                    modelBuilder.Entity(entityType.Name).Property<DateTime?>("UpdateTime");
                    modelBuilder.Entity(entityType.Name).Property<DateTime?>("RemoveTime");
                    modelBuilder.Entity(entityType.Name).Property<bool>("IsRemoved");
                }
            }

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries().Where(p =>
                                                                p.State == EntityState.Modified
                                                                || p.State == EntityState.Added
                                                                || p.State == EntityState.Deleted);

            foreach (var entry in modifiedEntries) 
            {
                var entityType = entry.Context.Model.FindEntityType(entry.Entity.GetType());

                var inserted = entityType.FindProperty("InsertTime");
                var updated = entityType.FindProperty("UpdateTime");
                var removed = entityType.FindProperty("RemoveTime");
                var isRemove = entityType.FindProperty("IsRemoved");

                if(entry.State == EntityState.Modified && updated != null)
                { 
                    entry.Property("UpdateTime").CurrentValue = DateTime.Now;
                }

                if(entry.State == EntityState.Added && inserted != null)
                { 
                    entry.Property("InsertTime").CurrentValue = DateTime.Now;
                }

                if(entry.State == EntityState.Deleted && removed != null && isRemove != null)
                { 
                    entry.Property("RemoveTime").CurrentValue = DateTime.Now;
                    entry.Property("IsRemoved").CurrentValue = true;
                }
            }

            return base.SaveChanges(); 
        }
    }
}
