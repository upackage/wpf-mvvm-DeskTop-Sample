using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZFS.Model.Query;
using ZFS.Model.ResponseModel;

namespace ZFS.Core.Interfaces
{
    public interface IGroupService 
    {
        Task<GroupResponse> GetGroupsAsync(GroupParameters parameters);
    }
}
