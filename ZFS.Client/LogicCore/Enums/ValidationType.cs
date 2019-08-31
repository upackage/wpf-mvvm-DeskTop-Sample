using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZFS.Client.LogicCore.Enums
{
    /// <summary>
    /// 验证类型
    /// </summary>
    public enum ValidationType
    {
        None,

        [Description(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")]
        Email,
        
        [Description(@"^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$")]
        Phone,
        
        Str,
        //...
    }
}
