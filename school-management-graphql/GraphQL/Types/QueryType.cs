using Microsoft.EntityFrameworkCore;
using school_management_graphql.Data;

namespace school_management_graphql.GraphQL.Types
{
    public class QueryType : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            /* descriptor.Field(x => x.Teachers)
             .Name("teachers")
             .Description("This is the teacher in the school.")
             .Type<ListType<TeacherType>>()
             .Argument("id", a =>
            a.Type<NonNullType<UuidType>>())
             .Resolve(async context =>
             {
                 var id = context.ArgumentValue<Guid>("id");
                 var teacher = await context.
                Service<AppDbContext>().Teachers.FindAsync(id);
                 return teacher;
             });*/

            descriptor.Field(x => x.Teachers)
            .Description("This is the list of teachers in the school.")
            .Type<ListType<TeacherType>>()
            .Resolve(async context =>
            {
                var dbContextFactory = context.Service<IDbContextFactory<AppDbContext>>();
                await using var dbContext = await dbContextFactory.CreateDbContextAsync();
                var teachers = await dbContext.Teachers.ToListAsync();
                return teachers;
            });
        }
    }
}
