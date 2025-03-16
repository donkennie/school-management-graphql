using school_management_graphql.Models;

namespace school_management_graphql.Services
{
    public interface ITeacherService
    {
        Task<List<Course>> GetCoursesAsync(Guid teacherId);
        Task<Department> GetDepartmentAsync(Guid departmentId);
        Task<List<Teacher>> GetTeachersAsync();

        Task<Teacher> GetTeacherAsync(Guid teacherId);
        Task<Teacher> CreateTeacherAsync(Teacher teacher);
        Task<Teacher> UpdateTeacherAsync(Teacher teacher);
        Task DeleteTeacherAsync(Guid teacherId);
    }
}
