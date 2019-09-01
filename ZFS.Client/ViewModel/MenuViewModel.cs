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
    /// 菜单
    /// </summary>
    [Module(ModuleType.BasicData, "MenuDlg", "菜单管理")]
    public class MenuViewModel : DataProcess<Menu>
    {
        private readonly IMenuService service;
        public MenuViewModel()
        {
            service = ServiceProvider.Instance.Get<IMenuService>();
            this.Init();
        }

        public override async void GetPageData(int pageIndex)
        {
            try
            {
                var r = await service.GetMenusAsync(new MenuParameters()
                {
                    PageIndex = pageIndex,
                    PageSize = PageSize,
                    Search = SearchText,
                });
                if (r.success)
                {
                    TotalCount = r.TotalRecord;
                    GridModelList.Clear();
                    r.menus.ForEach((arg) => GridModelList.Add(arg));
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
