using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ZFS.Model.Entity
{
    [Serializable]
    public class Menu : BaseEntity
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Caption { get; set; }

        public string NameSpace { get; set; }

        [DefaultValue(0)]
        public int Authorities { get; set; }

        public string ParentName { get; set; }
    }
}
