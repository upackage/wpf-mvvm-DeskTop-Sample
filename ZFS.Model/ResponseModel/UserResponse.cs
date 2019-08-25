using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFS.Model.Entity;

namespace ZFS.Model.ResponseModel
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
        public List<User> users {
            get
            {
                if (dynamicObj == null) return null;
                return JsonConvert.DeserializeObject<List<User>>(dynamicObj.ToString());
            }
        }
    }

    public class LoginUserResponse : BaseResponse
    {
        public User user
        {
            get
            {
                if (dynamicObj == null) return null;
                return JsonConvert.DeserializeObject<User>(dynamicObj.ToString());
            }
        }
    }

    public class LoginUserAuthResponse : BaseResponse
    {
        public List<AuthorityEntity> authorityEntity
        {
            get
            {
                if (dynamicObj == null) return null;
                return JsonConvert.DeserializeObject<List<AuthorityEntity>>(dynamicObj.ToString());
            }
        }
    }

    #region 用户权限返回数据实体

    public class AuthorityEntity
    {
        public string account { get; set; }

        public string groupName { get; set; }

        public string menuName { get; set; }

        public string menuCaption { get; set; }

        public string menuNameSpace { get; set; }

        public string parentName { get; set; }

        public int authorities { get; set; }
    }

    #endregion
}
