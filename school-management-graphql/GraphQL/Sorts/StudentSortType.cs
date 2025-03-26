using HotChocolate.Data.Sorting;
using school_management_graphql.Models;

namespace school_management_graphql.GraphQL.Sorts
{
    public class StudentSortType : SortInputType<Student>
    {
        protected override void Configure(ISortInputTypeDescriptor<Student> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Field(x => x.FirstName);
            descriptor.Field(x => x.LastName);
            descriptor.Field(x => x.DateOfBirth);
        }
    }
}
