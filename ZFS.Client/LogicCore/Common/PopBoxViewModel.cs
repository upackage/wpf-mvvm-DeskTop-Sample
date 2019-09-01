using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFS.Client.LogicCore.Configuration;
using ZFS.Client.LogicCore.Interface;
using ZFS.Client.View;

namespace ZFS.Client.LogicCore.Common
{
    /// <summary>
    /// 辅助窗口
    /// </summary>
    public class PopBoxViewModel : ViewModelBase
    {
        public PopBoxViewModel()
        {
            pupBoxModelsl = new ObservableCollection<PupBoxModel>();
            SkinCommand = new RelayCommand(() => Skin());
            StepCommand = new RelayCommand(() => Step());
            AboutCommand = new RelayCommand(() => About());
            NoticeCommand = new RelayCommand(() => OpenNotice());
            this.GetBoxModels();
        }

        #region 功能集合


        /// <summary>
        /// 获取默认定义
        /// </summary>
        private void GetBoxModels()
        {
            pupBoxModelsl.Add(new PupBoxModel() { KindName = "Palette", Name = "个性化", ApplyCommand = this.SkinCommand });
            pupBoxModelsl.Add(new PupBoxModel() { KindName = "Settings", Name = "系统设置", ApplyCommand = this.StepCommand });
            pupBoxModelsl.Add(new PupBoxModel() { KindName = "CommentMultipleOutline", Name = "消息通知", ApplyCommand = this.NoticeCommand });
            pupBoxModelsl.Add(new PupBoxModel() { KindName = "NearMe", Name = "关于作者", ApplyCommand = this.AboutCommand });
        }

        private ObservableCollection<PupBoxModel> pupBoxModelsl;

        public ObservableCollection<PupBoxModel> PupBoxModels
        {
            get { return pupBoxModelsl; }
        }

        #endregion

        #region ICommand

        public RelayCommand SkinCommand { get; }

        public RelayCommand StepCommand { get; }

        public RelayCommand AboutCommand { get; }

        public RelayCommand NoticeCommand { get; }



        #endregion

        #region ICommand 实现

        public void Min()
        {
            Messenger.Default.Send("", "MinWindow");
        }


        /// <summary>
        /// 皮肤设置
        /// </summary>
        private void Skin()
        {
            var dialog = ServiceProvider.Instance.Get<IShowContent>();
            dialog.BindDataContext(new SkinWindow(), new SkinViewModel());
            dialog.Show();
        }

        /// <summary>
        /// 系统设置
        /// </summary>
        private void Step()
        {

        }

        /// <summary>
        /// 关于作者
        /// </summary>
        private void About()
        {
            //About about = new About();
            //about.ShowDialog();
        }

        /// <summary>
        /// 通知中心
        /// </summary>
        public void OpenNotice()
        {
            NoticeViewModel view = new NoticeViewModel();
            var Dialog = ServiceProvider.Instance.Get<IModelDialog>("NoticeViewDlg");
            Dialog.BindViewModel(view);
            Dialog.ShowDialog();
        }

        #endregion
    }

    /// <summary>
    /// 首页辅助弹出窗口功能定义
    /// </summary>
    public class PupBoxModel : ViewModelBase
    {
        private string kindName;
        private string name;

        /// <summary>
        /// 字体代码[显示LOGO]
        /// </summary>
        public string KindName
        {
            get { return kindName; }
            set { kindName = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 功能名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                name = value; RaisePropertyChanged();
            }
        }

        public RelayCommand ApplyCommand { get; set; }

    }
}
