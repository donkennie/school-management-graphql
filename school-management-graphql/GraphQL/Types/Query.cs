using Microsoft.EntityFrameworkCore;
using school_management_graphql.Data;
using school_management_graphql.Models;
using school_management_graphql.Services;

namespace school_management_graphql.GraphQL.Types
{
    public class Query
    {
        public List<TeacherType> Teachers { get; set; } = new();
        public TeacherType? Teacher { get; set; } = new();

        public List<DepartmentType> Departments { get; set; } = new();

        public async Task<List<Teacher>> GetTeachers([Service]
        AppDbContext context) =>
                await context.Teachers.ToListAsync();

        // The following code does not use the Service attribute to inject the ITeacherService service because the service is registered in the GraphQL server.
        public async Task<List<Teacher>> GetTeachersWithDI(ITeacherService teacherService) =>
            await teacherService.GetTeachersAsync();
    }
}
