using Microsoft.EntityFrameworkCore;
using school_management_graphql.Data;
using school_management_graphql.Models;

namespace school_management_graphql.GraphQL.Queries
{
    public class Query
    {
        public async Task<List<Teacher>> GetTeachers([Service]
        AppDbContext context) =>
                await context.Teachers.ToListAsync();

        public async Task<Teacher?> GetTeacher(Guid id, [Service]
        AppDbContext context) =>
         await context.Teachers.FindAsync(id);
    }
}
