using System;
using System.Collections.Generic;
using System.Text;

namespace ZFS.Model.Entity
{
    [Serializable]
    public class Group : BaseEntity
    {
        public string Code { get; set; }

        public string Name { get; set; }

    }
}
