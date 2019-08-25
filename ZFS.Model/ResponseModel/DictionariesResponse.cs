using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFS.Model.Entity;

namespace ZFS.Model.ResponseModel
{
    public class DictionariesResponse : BaseResponse
    {
        public List<Dictionaries> Dictionaries
        {
            get
            {
                if (dynamicObj == null) return null;
                return JsonConvert.DeserializeObject<List<Dictionaries>>(dynamicObj.ToString());
            }
        }
    }
}
