using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CourseSelecting.Education.Dto;

namespace CourseSelecting.Education
{
    public interface ISubjectProjectAppService : IApplicationService
    {
        Task AddSubjectProject(AddSubjectProjectInput input);
        // Task<ListResultOutput<SubjectProjectsListDto>> GetSubjectProjects(GetSubjectProjectsInput input);
        Task<PagedResultOutput<SubjectProjectsListDto>> GetSubjectProjects(GetSubjectProjectsInput input);
        Task DeleteSubjectProject(int id);
      
        Task<SubjectProjectDto> GetSubjectProject(int id);
        Task EditSubjectProject(EditSubjectProjectInput input);
        Task<SubjectProjectCoursesDto> GetSubjectProjectCourses(int id);
        Task<ListResultOutput<SubjectProjectSimpleDto>> GetCurrentSemesterSubjectProjects();
        Task<ListResultOutput<SubjectProjectCoursesDto>> GetSubjectProjectsWithCourses();
        Task<PagedResultOutput<GetSelectSubjectProjectsListDto>> GetSelectSubjectProjects(GetSubjectProjectsInput input);
        Task<PagedResultOutput<GetSelectSubjectProjectsListDto>> GetStudentSelectCourses(GetSubjectProjectsInput input);
        Task AddSelectSubjectProject(AddSelectSubjectProjectInput input);
        Task SelectDeleteSubjectProject(int id);
        Task selectDeleteStudentCourse(int studentId,int courseId);
        Task AddSelectSubjectProject1(GetCourseTimeInput input);           //3
    }
}
