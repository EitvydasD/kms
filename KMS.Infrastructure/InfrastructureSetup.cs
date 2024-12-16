using KMS.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace KMS.Infrastructure;

public static class InfrastructureSetup
{
    public static void AddAppDbContext(this IServiceCollection services, string connectionString) =>
        services.AddDbContext<DatabaseContext>(options => 
            options.UseInMemoryDatabase(connectionString));
}
