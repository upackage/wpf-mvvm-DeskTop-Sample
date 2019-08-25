using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZFS.Core.Interfaces;
using ZFS.Model.Entity;
using ZFS.Model.Query;
using ZFS.Model.RequestModel;
using ZFS.Model.ResponseModel;

namespace ZFS.Service.Service
{
    public class UserService : IUserService
    {
        public async Task<bool> AddUserAsync(User model)
        {
            BaseServiceRequest<UserResponse> baseService = new BaseServiceRequest<UserResponse>();
            var r = await baseService.GetRequest(new UserAeRequest() { Entity = model });
            return r.Result;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            BaseServiceRequest<UserResponse> baseService = new BaseServiceRequest<UserResponse>();
            var r = await baseService.GetRequest(new UserDeleteRequest() { id = id });
            return r.Result;
        }

        public async Task<GetUserResponse> GetUsersAsync(UserParameters parameters)
        {
            BaseServiceRequest<GetUserResponse> baseService = new BaseServiceRequest<GetUserResponse>();
            var r = await baseService.GetRequest(new UserQueryRequest() {  parameters = parameters });
            return r;
        }

        public async Task<LoginUserResponse> LoginAsync(string account, string passWord)
        {
            BaseServiceRequest<LoginUserResponse> baseService = new BaseServiceRequest<LoginUserResponse>();
            var r = await baseService.GetRequest(new UserLoginRequest() { account = account, passWord = passWord });
            return r;
        }

        public async Task<LoginUserAuthResponse> LoginUserAuthAsync(string account)
        {
            BaseServiceRequest<LoginUserAuthResponse> baseService = new BaseServiceRequest<LoginUserAuthResponse>();
            var r = await baseService.GetRequest(new UserLoginAuthRequest() { account = account });
            return r;
        }

        public async Task<bool> UpdateUserAsync(User model)
        {
            BaseServiceRequest<UserResponse> baseService = new BaseServiceRequest<UserResponse>();
            var r = await baseService.GetRequest(new UserAeRequest() { Entity = model });
            return r.Result;
        }
    }
}
