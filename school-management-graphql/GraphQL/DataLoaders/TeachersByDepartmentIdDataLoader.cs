using Microsoft.EntityFrameworkCore;
using school_management_graphql.Data;
using school_management_graphql.Models;

namespace school_management_graphql.GraphQL.DataLoaders
{
    public class TeachersByDepartmentIdDataLoader(IDbContextFactory<AppDbContext> dbContextFactory, IBatchScheduler batchScheduler,
    DataLoaderOptions? options = null) : GroupedDataLoader<Guid, Teacher>(batchScheduler, options)
    {
        protected override async Task<ILookup<Guid, Teacher>> LoadGroupedBatchAsync(IReadOnlyList<Guid> keys,
        CancellationToken cancellationToken)
        {
            await using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
            var teachers = await dbContext.Teachers.Where(x => keys.Contains(x.DepartmentId))
            .ToListAsync(cancellationToken);
            return teachers.ToLookup(x => x.DepartmentId);
        }
    }
}
