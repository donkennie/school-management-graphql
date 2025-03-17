﻿namespace school_management_graphql.GraphQL.Mutations
{
    public record AddTeacherInput(
         string FirstName,
         string LastName,
         string Email,
         string? Phone,
         string? Bio,
         Guid DepartmentId);
}
