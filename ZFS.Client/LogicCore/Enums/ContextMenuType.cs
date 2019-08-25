using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZFS.Client.LogicCore.Enums
{
    /// <summary>
    /// 菜单类型
    /// </summary>
    public enum ContextMenuType
    {
        [Description("列表模式")]
        ListMode,

        [Description("磁贴模式")]
        MetroMode,

        //...

    }
}
