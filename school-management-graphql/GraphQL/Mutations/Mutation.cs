using school_management_graphql.Data;
using school_management_graphql.Models;

namespace school_management_graphql.GraphQL.Mutations
{
    public class Mutation
    {
        public async Task<AddTeacherPayload> AddTeacherAsync(
        AddTeacherInput input,
        [Service] AppDbContext context)
        {
            try
            {
                var teacher = new Teacher
                {
                    Id = Guid.NewGuid(),
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    Email = input.Email,
                    Phone = input.Phone,
                    Bio = input.Bio
                };
                context.Teachers.Add(teacher);
                await context.SaveChangesAsync();
                return new AddTeacherPayload(teacher);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message); // THIS is usually where the real cause is
                throw;
            }
        }
    }
}
