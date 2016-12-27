using System.Threading.Tasks;
using Abp.Application.Services;
using CourseSelecting.Roles.Dto;

namespace CourseSelecting.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
