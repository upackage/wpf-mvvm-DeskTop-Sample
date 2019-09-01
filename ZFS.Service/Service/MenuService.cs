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
    public class MenuService : IMenuService
    {
        public async Task<MenuResponse> GetMenusAsync(MenuParameters parameters)
        {
            BaseServiceRequest<MenuResponse> baseService = new BaseServiceRequest<MenuResponse>();
            var r = await baseService.GetRequest(new MenuRequest() { parameters = parameters });
            return r;
        }
    }
}
