using school_management_graphql.GraphQL.DataLoaders;
using school_management_graphql.Models;
using school_management_graphql.Services;

namespace school_management_graphql.GraphQL.Types
{
    public class DepartmentType : ObjectType<Department>
    {
        protected override void Configure(IObjectTypeDescriptor<Department> descriptor)
        {
            descriptor.Field(x => x.Teachers)
             .Description("This is the list of teachers in the school.")
             .Type<ListType<TeacherType>>()
             .Resolve(async context =>
             {
                 var teacherService = context.Service<ITeacherService>();
                 var teachers = await teacherService.GetTeachersAsync();
                 return teachers;
             });
        }
    }

    public class DepartmentResolvers
    {
        public async Task<List<Teacher>> GetTeachers([Parent] Department department, TeachersByDepartmentIdDataLoader
        teachersByDepartmentIdDataLoader, CancellationToken cancellationToken)
        {
            var teachers = await teachersByDepartmentIdDataLoader.LoadAsync(department.Id, cancellationToken);
            return teachers.ToList();
        }
    }
}
