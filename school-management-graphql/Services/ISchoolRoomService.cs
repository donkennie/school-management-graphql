using school_management_graphql.Models;

namespace school_management_graphql.Services
{
    public interface ISchoolRoomService
    {
        Task<List<ISchoolRoom>> GetSchoolRoomsAsync();
        Task<List<LabRoom>> GetLabRoomsAsync();
        Task<List<Classroom>> GetClassroomsAsync();
        Task<LabRoom> GetLabRoomAsync(Guid labRoomId);
        Task<Classroom> GetClassroomAsync(Guid classroomId);
    }

}
