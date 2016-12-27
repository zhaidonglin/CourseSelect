using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CourseSelecting.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSelecting.Users
{
    public interface IStudentAppService : IApplicationService
    {
        //ListResultOutput<StudentListDto> GetStudents(GetStudentsInput input);
        Task<PagedResultOutput<StudentListDto>> GetStudents(GetStudentsInput input);
        Task AddStudent(AddStudentInput input);
        Task EditStudent(EditStudentInput input);

        StudentDetailsDto GetStudent(long id);

        Task DeleteStudent(long id);
        Task CreatStudentInfo(EditStudentInput input);
        //ListResultOutput<StudentListDto> GetStudents(GetStudentsInput input);
        Task ResetStudent(long id);
    }
}
