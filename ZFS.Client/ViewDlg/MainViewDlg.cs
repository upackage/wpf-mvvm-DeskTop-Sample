using GalaSoft.MvvmLight.Messaging;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ZFS.Client.LogicCore.Common;
using ZFS.Client.LogicCore.Interface;
using ZFS.Client.ViewModel;
using ZFS.Client.ViewModel.VMBase;

namespace ZFS.Client.ViewDlg
{
    /// <summary>
    /// 首页
    /// </summary>
    [Autofac(true)]
    public class MainViewDlg : BaseViewDialog<MainWindow>, IModelDialog
    {
        public override void BindDefaultViewModel()
        {
            MainViewModel model = new MainViewModel();
            model.InitDefaultView();
            GetDialogWindow().DataContext = model;
        }

        public override void BindViewModel<TViewModel>(TViewModel viewModel)
        {
            GetDialogWindow().DataContext = viewModel;
        }

        public override void Close()
        {
            GetDialogWindow().Close();
        }

        public override Task<bool> ShowDialog(DialogOpenedEventHandler openedEventHandler = null, DialogClosingEventHandler closingEventHandler = null)
        {
            GetDialogWindow().ShowDialog();
            return Task.FromResult(true);
        }

        public override void RegisterDefaultEvent()
        {
            GetDialogWindow().MouseDown += (sender, e) => { if (e.LeftButton == MouseButtonState.Pressed) { GetDialogWindow().DragMove(); } };
            Messenger.Default.Register<string>(GetDialogWindow(), "MainExit", new Action<string>(async (msg) =>
            {
                if (await Msg.Question(msg))
                {
                  //  IUser user = ServiceProvider.Instance.Get<IBridgeManager>
                  //(Loginer.LoginerUser.ServerBridgeType).GetUserManager();
                  //  var r = await user.Logout(Loginer.LoginerUser.Account);
                  //  if (r.Success)
                  //      this.Close();
                  //  else
                  //      Msg.Error(r.ErrorCode);
                }
            }));
            Messenger.Default.Register<string>(GetDialogWindow(), "MinWindow", new Action<string>((msg) => { GetDialogWindow().WindowState = WindowState.Minimized; }));
            Messenger.Default.Register<bool>(GetDialogWindow(), "MaxWindow", new Action<bool>((arg) =>
            {
                var win = GetDialogWindow();
                if (win.WindowState == WindowState.Maximized)
                    win.WindowState = WindowState.Normal;
                else
                    win.WindowState = WindowState.Maximized;
            }));
        }

        private Window GetDialogWindow()
        {
            return GetDialog() as Window;
        }
    }
}
