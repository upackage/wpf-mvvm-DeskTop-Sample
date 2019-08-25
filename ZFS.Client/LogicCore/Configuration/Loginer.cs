using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFS.Model.ResponseModel;

namespace ZFS.Client.LogicCore.Configuration
{
    /// <summary>
    /// 登录用户信息
    /// </summary>
    public class Loginer
    {
        private Loginer() { }
        private static Loginer _Loginer = new Loginer(); //饿汉式单例

        /// <summary>
        /// 当前用户
        /// </summary>
        public static Loginer LoginerUser
        {
            get { return _Loginer; }
        }

        /// <summary>
        /// 登录名
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 是否属于管理员
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 服务类型
        /// </summary>
        public string ServerBridgeType { get; set; }

        /// <summary>
        /// 用户权限缓存
        /// </summary>
        public List<AuthorityEntity> authorityEntity { get; set; }

    }
}
