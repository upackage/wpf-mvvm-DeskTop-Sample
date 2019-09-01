using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFS.Client.LogicCore.Common;
using ZFS.Client.LogicCore.Interface;
using ZFS.Client.View;
using ZFS.Client.ViewModel;
using ZFS.Client.ViewModel.VMBase;

namespace ZFS.Client.ViewDlg
{
    /// <summary>
    /// 菜单
    /// </summary>
    [Autofac(true)]
    public class MenuDlg : BaseView<MenuView, MenuViewModel>, IModel
    {

    }
}
