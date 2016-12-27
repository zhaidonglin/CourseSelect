using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CourseSelecting.Users.Dto;
using CourseSelecting.Education.Dto;        //8

namespace CourseSelecting.Users
{
    public interface ITeacherAppService : IApplicationService
    {
        Task<PagedResultOutput<TeacherListDto>> GetTeachers(GetTeachersInput input);

        Task AddTeacher(AddTeacherInput input);

        Task EditTeacher(EditTeacherInput input);

        TeacherDetailsDto GetTeacher(long id);

        Task DeleteTeacher(long id);
        Task CreatTeacherInfo(EditTeacherInput input);
        Task ResetTeacher(long id);


        Task<PagedResultOutput<CourseTimeDto>> GetTeacherCourseTables(GetSSCourseTimeInput input);  //8

        Task DeleteTeacherCourseTables(int id);       //8

        //Task<CourseTimeDto> GetCourseTime(GetSSCourseTimeInput input);  //10
        Task<PagedResultOutput<CourseTimeDto>> GetCourseTime(GetSSCourseTimeInput input);  //10


        Task DeleteStudentCourseTables(int id);
        //Task<CourseTimeDto> GetCourseTime(int id);
    }
}
