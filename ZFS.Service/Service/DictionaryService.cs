using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFS.Core.Interfaces;
using ZFS.Model.Entity;
using ZFS.Model.Query;
using ZFS.Model.RequestModel;
using ZFS.Model.ResponseModel;

namespace ZFS.Service.Service
{
    public class DictionaryService : IDictionariesService
    {
        public async Task<DictionariesResponse> GetDictionariesAsync(DictionariesParameters parameters)
        {
            BaseServiceRequest<DictionariesResponse> baseService = new BaseServiceRequest<DictionariesResponse>();
            var r = await baseService.GetRequest(new DictionariesRequest() { parameters = parameters });
            return r;
        }
    }
}
