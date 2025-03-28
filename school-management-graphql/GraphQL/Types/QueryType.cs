﻿using HotChocolate.Data.Filters;
using Microsoft.EntityFrameworkCore;
using school_management_graphql.Data;
using school_management_graphql.GraphQL.Filters;
using school_management_graphql.GraphQL.Sorts;
using school_management_graphql.Models;
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
                 var equipmentService = context.Service<IEquipmentService>();

                 var furnitureService = context.Service<IFurnitureService>();

                 var equipmentTask = equipmentService.GetEquipmentListAsync();

                 var furnitureTask = furnitureService.GetFurnitureListAsync();

                 await Task.WhenAll(equipmentTask, furnitureTask);

                 var schoolItems = new List<object>();
                 schoolItems.AddRange(equipmentTask.Result);
                 schoolItems.AddRange(furnitureTask.Result);
                 return schoolItems;
             });

            descriptor.Field(x => x.Students)
                .Description("This is the list of students in the school.")
                .UseFiltering()
                .Resolve(async context =>
               {
                   var dbContextFactory = context.Service<IDbContextFactory<AppDbContext>>();
                   var dbContext = await dbContextFactory.CreateDbContextAsync();
                   var students = dbContext.Students.AsQueryable();
                   return students;
               });


            descriptor.Field(x => x.StudentsWithCustomFilter)
           .Description("This is the list of students in the school.")
           .UseFiltering<CustomStudentFilterType>()
           .UseSorting<StudentSortType>()
           .Resolve(async context =>
           {
               var service = context.Service<IStudentService>();

               // The following code uses the custom filter.
               var filter = context.GetFilterContext()?.ToDictionary();
               if (filter != null && filter.ContainsKey("groupId"))
               {
                   var groupFilter = filter["groupId"]! as Dictionary<string, object>;
                   if (groupFilter != null && groupFilter.ContainsKey("eq"))
                   {
                       if (!Guid.TryParse(groupFilter["eq"].ToString(), out var groupId))
                       {
                           throw new ArgumentException("Invalid group id", nameof(groupId));
                       }

                       var students = await service.GetStudentsByGroupIdAsync(groupId);
                       return students;
                   }

                   if (groupFilter != null && groupFilter.ContainsKey("in"))
                   {
                       if (groupFilter["in"] is not IEnumerable<string> groupIds)
                       {
                           throw new ArgumentException("Invalid group ids", nameof(groupIds));
                       }

                       groupIds = groupIds.ToList();
                       if (groupIds.Any())
                       {
                           var students =
                               await service.GetStudentsByGroupIdsAsync(groupIds
                                   .Select(x => Guid.Parse(x.ToString())).ToList());
                           return students;
                       }
                       return new List<Student>();

                   }
               }
               var allStudents = await service.GetStudentsAsync();
               return allStudents;
           });
        }
    }
}
