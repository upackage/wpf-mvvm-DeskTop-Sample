using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFS.Model.Query;

namespace ZFS.Model.RequestModel
{
    /// <summary>
    /// 字典查询
    /// </summary>
    public class DictionariesRequest : BaseRequest
    {
        public override string route { get { return ServerIP + "api/Dictionary/GetDictionaries"; } }

        public DictionariesParameters parameters { get; set; }
    }
}
