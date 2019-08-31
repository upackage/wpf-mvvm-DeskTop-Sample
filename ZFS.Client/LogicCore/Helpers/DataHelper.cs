using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ZFS.Client.LogicCore.Helpers
{
    public static class DataHelper
    {
        public static T ToDynamic<T>(this T source)
        {
            BinaryFormatter bFormatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            bFormatter.Serialize(stream, source);
            stream.Seek(0, SeekOrigin.Begin);
            return (T)bFormatter.Deserialize(stream);
        }
    }
}
