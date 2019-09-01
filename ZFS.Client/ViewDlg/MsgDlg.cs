using GalaSoft.MvvmLight.Messaging;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ZFS.Client.LogicCore.Common;
using ZFS.Client.LogicCore.Interface;
using ZFS.Client.UiCore.Template;

namespace ZFS.Client.ViewDlg
{
    /// <summary>
    /// 弹窗
    /// </summary>
    public class MsgDlg : IShowContent
    {
        private UserControl view;

        public void BindDataContext<T, V>(T control, V viewModel)
            where T : UserControl
            where V : class, new()
        {
            view = control;
            view.DataContext = viewModel;
        }

        public void BindDataContext<V>(V viewModel) where V : class, new()
        {
            view.DataContext = viewModel;
        }

        public async Task<bool> Show()
        {
            if (view == null) return false;
            object taskResult = await DialogHost.Show(view, "RootDialog"); //位于顶级窗口
            return (bool)taskResult;
        }
    }
}
