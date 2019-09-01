using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFS.Model.Query;

namespace ZFS.Model.RequestModel
{
    /// <summary>
    /// 菜单查询
    /// </summary>
    public class MenuRequest : BaseRequest
    {
        public override string route { get { return ServerIP + "api/Menu/GetMenu"; } }

        public MenuParameters parameters { get; set; }
    }
}
