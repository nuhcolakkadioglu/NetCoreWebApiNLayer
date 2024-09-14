using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace App.Repositories.Interceptors
{
    public class AuditDbContextInterceptor : SaveChangesInterceptor
    {
        private static readonly Dictionary<EntityState, Action<DbContext, IAuditEntity>> _behaviors = new()
        {
            {EntityState.Added,AddBehavior },
            {EntityState.Modified,ModifiedBehavior },
        };


        private static void AddBehavior(DbContext context, IAuditEntity audid)
        {
            audid.Created = DateTime.Now;
            context.Entry(audid).Property(x => x.Updated).IsModified = false;
        }

        private static void ModifiedBehavior(DbContext context, IAuditEntity audid)
        {
            audid.Updated = DateTime.Now;
            context.Entry(audid).Property(x => x.Created).IsModified = false;
        }



        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            foreach (var item in eventData.Context!.ChangeTracker.Entries().ToList())
            {
                if (item.Entity is not IAuditEntity audid) continue;

                _behaviors[item.State](eventData.Context, audid);

            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
