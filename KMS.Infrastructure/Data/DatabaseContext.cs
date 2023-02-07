using KMS.Core.Exceptions;
using Microsoft.Data.SqlClient;
using Utils.Library.Exceptions;

namespace KMS.Infrastructure.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> configuration) : base(configuration)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        try
        {
            var modifiedEntries = ChangeTracker.Entries<BaseEntity>()
                .Where(x => x.State == EntityState.Modified)
                .ToArray();

            foreach (var entry in modifiedEntries)
            {
                entry.Entity.ModifiedAt = DateTimeOffset.Now;
            }

            var createdEntries = ChangeTracker.Entries<BaseEntity>()
                .Where(x => x.State == EntityState.Added)
                .ToArray();
            
            foreach (var entry in createdEntries)
            {
                if (entry.Entity.Id == Guid.Empty)
                {
                    entry.Entity.Id = Guid.NewGuid();
                }

                entry.Entity.CreatedAt = DateTimeOffset.Now;
                entry.Entity.ModifiedAt = DateTimeOffset.Now;
            }

            var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return result;
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException is SqlException sqlException)
            {
                if (sqlException.Number == 2601)
                {
                    throw new DuplicatedException("Tried to create duplicated entry", ex);
                }
            }
            throw;
        }
    }

    public override int SaveChanges()
    {
        return SaveChangesAsync().GetAwaiter().GetResult();
    }
}
