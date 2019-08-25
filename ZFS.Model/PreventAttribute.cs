using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZFS.Model
{
    /// <summary>
    /// 标记不序列化
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PreventAttribute : Attribute
    {
    }
}
