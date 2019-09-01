using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFS.Model.Entity;

namespace ZFS.Model.ResponseModel
{
    public class GroupResponse : BaseResponse
    {
        public List<Group> groups
        {
            get
            {
                if (dynamicObj == null) return null;
                return JsonConvert.DeserializeObject<List<Group>>(dynamicObj.ToString());
            }
        }
    }
}
