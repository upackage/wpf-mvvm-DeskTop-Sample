using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFS.Client.LogicCore.Common;
using ZFS.Client.LogicCore.Configuration;
using ZFS.Client.LogicCore.Enums;
using ZFS.Client.ViewModel.VMBase;
using ZFS.Core.Interfaces;
using ZFS.Model.Entity;
using ZFS.Model.Query;

namespace ZFS.Client.ViewModel
{
    /// <summary>
    /// 组列表
    /// </summary>
    [Module(ModuleType.BasicData, "GroupDlg", "权限管理")]
    public class GroupViewModel : DataProcess<Group>
    {
        private readonly IGroupService service;
        public GroupViewModel()
        {
            service = ServiceProvider.Instance.Get<IGroupService>();
            this.Init();
        }

        public override async void GetPageData(int pageIndex)
        {
            try
            {
                var r = await service.GetGroupsAsync(new GroupParameters()
                {
                    PageIndex = pageIndex,
                    PageSize = PageSize,
                    Search = SearchText,
                });
                if (r.success)
                {
                    TotalCount = r.TotalRecord;
                    GridModelList.Clear();
                    r.groups.ForEach((arg) => GridModelList.Add(arg));
                    base.SetPageCount();
                }
            }
            catch (Exception ex)
            {
                Msg.Error(ex.Message);
            }
        }
    }
}
