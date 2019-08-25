using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFS.Model.Entity;

namespace ZFS.StandardCore.ResponseModel
{
    /// <summary>
    /// 新增修改用户响应
    /// </summary>
    public class UserResponse : BaseResponse
    {
        public bool Result { get; set; }
    }

    public class GetUserResponse : BaseResponse
    {
        public List<User> users { get; set; }
    }

    public class LoginUserResponse : BaseResponse
    {
        public User user { get; set; }
    }
}
