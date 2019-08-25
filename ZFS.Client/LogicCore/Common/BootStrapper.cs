using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFS.Client.LogicCore.Configuration;
using ZFS.Client.LogicCore.Interface;

namespace ZFS.Client.LogicCore.Common
{
    public class BootStrapper
    {
        /// <summary>
        /// 注册方法
        /// </summary>
        public static void Initialize(IAutoFacLocator autoFacLocator)
        {
            ServiceProvider.RegisterServiceLocator(autoFacLocator);
            ServiceProvider.Instance.Register();
        }
    }
}
