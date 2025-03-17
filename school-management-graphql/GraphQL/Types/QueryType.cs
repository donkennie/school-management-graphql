using school_management_graphql.Data;

namespace school_management_graphql.GraphQL.Types
{
    public class QueryType : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor.Field(x => x.Teacher)
            .Name("teacher")
            .Description("This is the teacher in the school.")
            .Type<TeacherType>()
            .Argument("id", a =>
           a.Type<NonNullType<UuidType>>())
            .Resolve(async context =>
            {
                var id = context.ArgumentValue<Guid>("id");
                var teacher = await context.
               Service<AppDbContext>().Teachers.FindAsync(id);
                return teacher;
            });
        }
    }
}
