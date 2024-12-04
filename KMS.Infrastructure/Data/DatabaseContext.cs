using KMS.Core.Aggregates.Role.Entities;
using KMS.Core.Aggregates.User.Entities;
using Microsoft.Data.SqlClient;
using Utils.Library.Exceptions;

namespace KMS.Infrastructure.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> configuration) : base(configuration) { }

    public DbSet<RoleEntity> Roles => Set<RoleEntity>();
    public DbSet<RolePermissionEntity> RolePermissions => Set<RolePermissionEntity>();
    public DbSet<UserRoleEntity> UserRoles => Set<UserRoleEntity>();
    public DbSet<UserEntity> Users => Set<UserEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        try
        {
            ChangeTracker.Entries<BaseEntity>()
                .Where(x => x.State == EntityState.Modified)
                .ToList()
                .ForEach(entry => entry.Entity.ModifiedAt = DateTimeOffset.Now);

            ChangeTracker.Entries<BaseEntity>()
                .Where(x => x.State == EntityState.Added)
                .ToList()
                .ForEach(entry =>
                {
                    if (entry.Entity.Id == Guid.Empty)
                    {
                        entry.Entity.Id = Guid.NewGuid();
                    }

                    entry.Entity.CreatedAt = DateTimeOffset.Now;
                    entry.Entity.ModifiedAt = DateTimeOffset.Now;
                });

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
