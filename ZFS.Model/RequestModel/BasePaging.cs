using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZFS.Model.RequestModel
{
    public class BasePaging
    {
        public int pageIndex { get; set; }

        public int pageSize { get; set; }
    }

    public class BaseTime
    {
        public DateTime startTime { get; set; }

        public DateTime endTime { get; set; }
    }
}
