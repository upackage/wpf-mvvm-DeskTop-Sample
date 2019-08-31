using System;
using System.Collections.Generic;
using System.Text;

namespace ZFS.Model.Entity
{
    [Serializable]
    public class Group : BaseEntity
    {
        public string GroupCode { get; set; }

        public string GroupName { get; set; }

    }
}
