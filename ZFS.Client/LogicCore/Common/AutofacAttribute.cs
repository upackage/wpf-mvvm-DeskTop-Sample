using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZFS.Client.LogicCore.Common
{
    /// <summary>
    /// autofac 注册特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AutofacAttribute : Attribute
    {
        public AutofacAttribute(bool allow)
        {
            _allow = allow;
        }

        private bool _allow;

        public bool Allow { get { return _allow; } }
    }
}
