using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFS.Core.Interfaces;
using ZFS.Model.Query;
using ZFS.Model.RequestModel;
using ZFS.Model.ResponseModel;

namespace ZFS.Service.Service
{
    public class GroupService : IGroupService
    {
        public async Task<GroupResponse> GetGroupsAsync(GroupParameters parameters)
        {
            BaseServiceRequest<GroupResponse> baseService = new BaseServiceRequest<GroupResponse>();
            var r = await baseService.GetRequest(new GroupRequest() { parameters = parameters });
            return r;
        }
    }
}
