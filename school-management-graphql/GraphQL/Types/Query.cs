using Microsoft.EntityFrameworkCore;
using school_management_graphql.Data;
using school_management_graphql.Models;

namespace school_management_graphql.GraphQL.Types
{
    public class Query
    {

        public TeacherType? Teacher { get; set; } = new();

        public async Task<List<Teacher>> GetTeachers([Service]
        AppDbContext context) =>
                await context.Teachers.ToListAsync();


    }
}
