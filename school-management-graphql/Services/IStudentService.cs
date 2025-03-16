using school_management_graphql.Models;

namespace school_management_graphql.Services
{
    public interface IStudentService
    {
        Task<IQueryable<Student>> GetStudentsAsync();
        Task<List<Student>> GetStudentsByGroupIdAsync(Guid groupId);
        Task<List<Student>> GetStudentsByGroupIdsAsync(List<Guid> groupIds);
    }
}
