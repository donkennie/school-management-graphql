using Microsoft.EntityFrameworkCore;
using school_management_graphql.Data;
using school_management_graphql.GraphQL.DataLoaders;
using school_management_graphql.Models;

namespace school_management_graphql.GraphQL.Types
{
    public class TeacherType : ObjectType<Teacher>
    {
        protected override void Configure(IObjectTypeDescriptor<Teacher> descriptor)
        {
            descriptor.Field(x => x.Department)
            .Name("department")
            .Description("This is the department to which the teacher belongs.")
            .Resolve(async context =>
            {
                var dbContextFactory = context.Service<IDbContextFactory<AppDbContext>>();
                await using var dbContext = await dbContextFactory.
           CreateDbContextAsync();
                var department = await dbContext.Departments.FindAsync(context.Parent<Teacher>().DepartmentId);
                return department;
            });
        }
    }


    public class TeacherResolvers
    {
        public async Task<Department> GetDepartment([Parent] Teacher teacher, DepartmentByTeacherIdBatchDataLoader
        departmentByTeacherIdBatchDataLoader, CancellationToken cancellationToken)
        {
            var department = await departmentByTeacherIdBatchDataLoader.LoadAsync(teacher.DepartmentId, cancellationToken);
            return department;
        }
    }
}
