using Ardalis.Specification.EntityFrameworkCore;
using Utils.Library.Interfaces;

namespace KMS.Infrastructure.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(DatabaseContext dbContext) : base(dbContext)
    {
    }
}
