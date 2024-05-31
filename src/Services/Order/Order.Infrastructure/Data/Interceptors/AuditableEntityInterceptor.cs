using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Order.Infrastructure.Data.Interceptors
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public void UpdateEntities(DbContext? context)
        {
            if (context == null) return;

            foreach(var entry in context.ChangeTracker.Entries<IEntity>())
            {
                if(entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = "onurcan";
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                }

                if(entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedBy = "onurcan";
                    entry.Entity.UpdatedDate = DateTime.UtcNow;
                }
            }
        }
    }
}
