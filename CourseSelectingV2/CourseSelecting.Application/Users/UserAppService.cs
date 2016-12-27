using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using CourseSelecting.Authorization;
using CourseSelecting.Users.Dto;
using Microsoft.AspNet.Identity;
using Abp.UI;
using Abp.Threading;

namespace CourseSelecting.Users
{
    /* THIS IS JUST A SAMPLE. */
    [AbpAuthorize]
    public class UserAppService : CourseSelectingAppServiceBase, IUserAppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IPermissionManager _permissionManager;

        public UserAppService(IRepository<User, long> userRepository, IPermissionManager permissionManager)
        {
            _userRepository = userRepository;
            _permissionManager = permissionManager;
        }

        //public async Task ProhibitPermission(ProhibitPermissionInput input)
        //{
        //    var user = await UserManager.GetUserByIdAsync(input.UserId);
        //    var permission = _permissionManager.GetPermission(input.PermissionName);

        //    await UserManager.ProhibitPermissionAsync(user, permission);
        //}

        ////Example for primitive method parameters.
        //public async Task RemoveFromRole(long userId, string roleName)
        //{
        //    CheckErrors(await UserManager.RemoveFromRoleAsync(userId, roleName));
        //}

        //public async Task<ListResultOutput<UserListDto>> GetUsers()
        //{
        //    var users = await _userRepository.GetAllListAsync();

        //    return new ListResultOutput<UserListDto>(
        //        users.MapTo<List<UserListDto>>()
        //        );
        //}

        //public async Task CreateUser(CreateUserInput input)
        //{
        //    var user = input.MapTo<User>();

        //    user.TenantId = AbpSession.TenantId;
        //    user.Password = new PasswordHasher().HashPassword(input.Password);
        //    user.IsEmailConfirmed = true;

        //    CheckErrors(await UserManager.CreateAsync(user));
        //}
        public async Task UpdatePassword(UserUpdatePwdInput input)
        {
            var user = AsyncHelper.RunSync(() => UserManager.GetUserByIdAsync(AbpSession.UserId ?? 0));
            if (user == null) throw new UserFriendlyException("ÇëÖØÐÂµÇÂ¼£¡");


            var loginResult = await UserManager.LoginAsync(user.UserName, input.Password);

            if (loginResult.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Ô­ÃÜÂë´íÎó£¡");
            }

            user.Password = new PasswordHasher().HashPassword(input.NewPassword);
            await UserManager.UpdateAsync(user);
            //await _userRepository.UpdateAsync(user);
            //CheckErrors();
        }

    }
}