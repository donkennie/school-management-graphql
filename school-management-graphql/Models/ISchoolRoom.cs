﻿namespace school_management_graphql.Models
{
    public interface ISchoolRoom
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string? Description { get; set; }
        public int Capacity { get; set; }
    }
}
