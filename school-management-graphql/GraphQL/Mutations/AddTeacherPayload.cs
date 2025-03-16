using school_management_graphql.Models;

namespace school_management_graphql.GraphQL.Mutations
{
    public class AddTeacherPayload
    {
        public Teacher Teacher { get; }
        public AddTeacherPayload(Teacher teacher)
        {
            Teacher = teacher;
        }
    }
}
