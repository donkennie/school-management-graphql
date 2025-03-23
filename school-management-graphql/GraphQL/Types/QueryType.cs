using Microsoft.EntityFrameworkCore;
using school_management_graphql.Data;
using school_management_graphql.Services;

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

            descriptor.Field(x => x.Departments)
             .Description("This is the list of departments in the school.")
             .Type<ListType<DepartmentType>>()
             .Resolve(async context =>
             {
                 var dbContextFactory = context.Service<IDbContextFactory<AppDbContext>>();
                 await using var dbContext = await dbContextFactory.CreateDbContextAsync();
                 var departments = await dbContext.Departments.ToListAsync();
                 return departments;
             });

            descriptor.Field(x => x.SchoolRooms)
           .Description("This is the list of school rooms in the school.")
           .Type<ListType<SchoolRoomType>>()
           .Resolve(async context =>
           {
               var service = context.Service<ISchoolRoomService>();
               var schoolRooms = await service.GetSchoolRoomsAsync();
               return schoolRooms;
           });


            descriptor.Field(x => x.SchoolItems)
             .Description("This is the list of school items in the school.")
             .Type<ListType<SchoolItemType>>()
             .Resolve(async context =>
             {
                 var equipmentService = context.
                  Service<IEquipmentService>();
                 var furnitureService = context.
         Service<IFurnitureService>();
                 var equipmentTask = equipmentService.
         GetEquipmentListAsync();
                 var furnitureTask = furnitureService.
         GetFurnitureListAsync();
                 await Task.WhenAll(equipmentTask, furnitureTask);
                 var schoolItems = new List<object>();
                 schoolItems.AddRange(equipmentTask.Result);
                 schoolItems.AddRange(furnitureTask.Result);
                 return schoolItems;
             });

        }
    }
}
