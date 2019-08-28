using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZFS.Client.LogicCore.Interface
{
    /// <summary>
    ///权限操作接口
    /// </summary>
    public interface IPermissions
    {
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="authValue"></param>
        /// <returns></returns>
        bool GetButtonAuth(int authValue);

        /// <summary>
        /// 加载模板权限
        /// </summary>
        void LoadModuleAuth();

        /// <summary>
        /// 权限值
        /// </summary>
        int? AuthValue { get; set; }

    }
}
