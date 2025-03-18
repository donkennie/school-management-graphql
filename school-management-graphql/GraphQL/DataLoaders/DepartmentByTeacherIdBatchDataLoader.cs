using Microsoft.EntityFrameworkCore;
using school_management_graphql.Data;
using school_management_graphql.Models;

namespace school_management_graphql.GraphQL.DataLoaders
{
    public class DepartmentByTeacherIdBatchDataLoader(
          IDbContextFactory<AppDbContext> dbContextFactory,
          IBatchScheduler batchScheduler,
          DataLoaderOptions? options = null) : BatchDataLoader<Guid, Department>(batchScheduler, options)
    {
        protected override async Task<IReadOnlyDictionary<Guid, Department>> LoadBatchAsync(IReadOnlyList<Guid> keys,
        CancellationToken cancellationToken)
        {
            await using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
            var departments = await dbContext.Departments.Where(x => keys.Contains(x.Id)).ToDictionaryAsync(x => x.Id, cancellationToken);
            return departments;
        }
    }

}
