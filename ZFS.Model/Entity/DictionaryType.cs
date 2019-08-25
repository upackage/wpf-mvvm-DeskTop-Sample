using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ZFS.Model.Entity
{
    public class DictionaryType : BaseEntity
    {
        public string TypeCode { get; set; }

        public string TypeName { get; set; }
    }
}
