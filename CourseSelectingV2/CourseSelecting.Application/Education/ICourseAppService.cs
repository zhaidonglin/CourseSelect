using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using CourseSelecting.Education.Dto;

namespace CourseSelecting.Education
{
    public interface ICourseAppService : IApplicationService
    {
       // Task<ListResultOutput<CourseListDto>> GetCourses(GetCoursesInput input);
        Task<PagedResultOutput<CourseListDto>> GetCourses(GetSSCourseTimeInput input);

        Task<PagedResultOutput<SSCourseTimeDto>> GetSSCourseTimeTables(GetSSCourseTimeInput input);   //7
        Task<PagedResultOutput<StudentSubjectProjectDto>> GetCredits(GetCreditsInput input);
        Task<PagedResultOutput<StudentSubjectProjectDto>> GetSelfCredits(GetCreditsInput input);
        Task AddCourse(AddCourseInput input);

        Task EditCourse(EditCourseInput input);
        Task EditCredits(EditCreditsInput input);

        Task<CourseDetailsDto> GetCourse(int id);
        Task<CreditDetailsDto> GetCredit(int id);

        Task DeleteCourse(int id);
        Task<ListResultOutput<TeacherCourseDto>> GetTeacherCourses(long teacherid);
        Task EditTeacherCourses(EditTeacherCoursesInput input);

        Task<CourseTimeDto> CourseTimeAdd(CourseTimeAddInput input);
        Task<StudentCourseTimeDto> CourseExamTimeAdd(CourseTimeAddInput input);

        Task StudentCourseExamTimeAdd(int id);

        Task<ListResultOutput<CourseTimeDto>> GetTeacherCourseTimes(GetCourseTimeEventInput input);
        Task<ListResultOutput<StudentCourseTimeDto>> GetTeacherCourseExamTimes(GetExamTimeEventInput input);
     

    

        Task<ListResultOutput<StudentEnabledCourseTimeDto>> GetStudentCourseTimes(GetCourseTimeEventInput input);
        Task<ListResultOutput<StudentEnabledExamTimeDto>> GetStudentCourseExamTimes(GetExamTimeEventInput input);
        Task CourseTimeDelete(int id);
        Task CourseExamTimeDelete(int id);
        
        Task StudentCourseTimeOrdered(int id);
        Task StudentCourseTimeDeleteOrdered(int id);
        Task<CheckTeacherEnabledDeleteOutput> CheckTeacherEnabledDeleted(long teacherid);
        Task StudentCourseExamTimeDeleteOrdered(int id);
        Task DeleteSSCourse(int id);       //5
    }
}
