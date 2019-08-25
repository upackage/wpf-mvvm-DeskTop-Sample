using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZFS.Model.Entity;
using ZFS.Model.Query;
using ZFS.Model.ResponseModel;

namespace ZFS.Core.Interfaces
{
    public interface IUserService 
    {
        Task<LoginUserResponse> LoginAsync(string account, string passWord);

        Task<LoginUserAuthResponse> LoginUserAuthAsync(string account);

        Task<bool> AddUserAsync(User model);

        Task<bool> DeleteUserAsync(Guid id);

        Task<GetUserResponse> GetUsersAsync(UserParameters parameter);

        Task<bool> UpdateUserAsync(User model);
    }
}
