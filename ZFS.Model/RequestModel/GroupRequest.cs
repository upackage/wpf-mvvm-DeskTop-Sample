using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFS.Model.Query;

namespace ZFS.Model.RequestModel
{
    /// <summary>
    /// 组查询
    /// </summary>
    public class GroupRequest : BaseRequest
    {
        public override string route { get { return ServerIP + "api/Group/GetGroup"; } }

        public GroupParameters parameters { get; set; }
    }
}
