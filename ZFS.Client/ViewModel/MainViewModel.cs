using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZFS.Client.LogicCore.Common;
using ZFS.Client.LogicCore.Configuration;
using ZFS.Client.LogicCore.Enums;
using ZFS.Client.LogicCore.Interface;
using ZFS.Client.UiCore.Template.DemoCharts;

namespace ZFS.Client.ViewModel
{
    /// <summary>
    /// 首页
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region 模块系统

        private ModuleManager _ModuleManager;

        public ObservableCollection<PageInfo> OpenPageCollection { get; set; } = new ObservableCollection<PageInfo>();

        /// <summary>
        /// 模块管理器
        /// </summary>
        public ModuleManager ModuleManager
        {
            get { return _ModuleManager; }
        }

        #endregion

        #region 工具栏

        private PopBoxViewModel _PopBoxView;

        /// <summary>
        /// 辅助窗口
        /// </summary>
        public PopBoxViewModel PopBoxView
        {
            get { return _PopBoxView; }
        }

        private NoticeViewModel _NoticeView;

        /// <summary>
        /// 通知模块
        /// </summary>
        public NoticeViewModel NoticeView
        {
            get { return _NoticeView; }
        }

        #endregion

        #region 命令(Binding Command)

        private object _CurrentPage;

        /// <summary>
        /// 当前选择页
        /// </summary>
        public object CurrentPage
        {
            get { return _CurrentPage; }
            set { _CurrentPage = value; RaisePropertyChanged(); }
        }

        private RelayCommand<Module> _ExcuteCommand;
        private RelayCommand<PageInfo> _ExitCommand;
        private RelayCommand<ModuleGroup> _ExcuteGroupCommand;

        /// <summary>
        /// 打开分组
        /// </summary>
        public RelayCommand<ModuleGroup> ExcuteGroupCommand
        {
            get
            {
                if (_ExcuteGroupCommand == null)
                {
                    _ExcuteGroupCommand = new RelayCommand<ModuleGroup>(t => ExcuteGroup(t));
                }
                return _ExcuteGroupCommand;
            }
            set { _ExcuteGroupCommand = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 打开模块
        /// </summary>
        public RelayCommand<Module> ExcuteCommand
        {
            get
            {
                if (_ExcuteCommand == null)
                {
                    _ExcuteCommand = new RelayCommand<Module>(t => Excute(t));
                }
                return _ExcuteCommand;
            }
            set { _ExcuteCommand = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 关闭页
        /// </summary>
        public RelayCommand<PageInfo> ExitCommand
        {
            get
            {
                if (_ExitCommand == null)
                {
                    _ExitCommand = new RelayCommand<PageInfo>(t => ExitPage(t));
                }
                return _ExitCommand;
            }
            set { _ExitCommand = value; RaisePropertyChanged(); }
        }

        #endregion

        #region 初始化/页面相关

        /// <summary>
        /// 初始化首页
        /// </summary>
        public async void InitDefaultView()
        {
            //初始化工具栏,通知窗口
            _PopBoxView = new PopBoxViewModel();
            _NoticeView = new NoticeViewModel();
            //加载窗体模块
            _ModuleManager = new ModuleManager();
            await _ModuleManager.LoadModules();
            //设置系统默认首页
            var page = OpenPageCollection.FirstOrDefault(t => t.HeaderName.Equals("系统首页"));
            if (page == null)
            {
                //演示Demo加载默认首页,较消耗性能。 实际开发务移除患者更新开发部件。
                HomeAbout about = new HomeAbout();
                OpenPageCollection.Add(new PageInfo() { HeaderName = "系统首页", Body = about });
                CurrentPage = OpenPageCollection[OpenPageCollection.Count - 1];
            }
        }

        public void ExcuteGroup(ModuleGroup group)
        {
            ModuleManager.Modules.Clear();
            foreach (var m in group.Modules)
                ModuleManager.Modules.Add(m);
            if (expansionState == ExpansionState.Open)
                expansionAction();
        }

        /// <summary>
        /// 执行模块
        /// </summary>
        /// <param name="module"></param>
        private async void Excute(Module module)
        {
            try
            {
                var page = OpenPageCollection.FirstOrDefault(t => t.HeaderName.Equals(module.Name));
                if (page != null) { CurrentPage = page; return; }
                if (string.IsNullOrWhiteSpace(module.Code))
                {
                    //404页面
                    //DefaultViewPage defaultViewPage = new DefaultViewPage();
                    //OpenPageCollection.Add(new PageInfo() { HeaderName = module.Name, Body = defaultViewPage });
                    //CurrentPage = defaultViewPage;
                }
                else
                {
                    expansionAction();//收起
                    await Task.Factory.StartNew(() =>
                    {
                        var dialog = ServiceProvider.Instance.Get<IModel>(module.Code);
                        dialog.BindDefaultModel();
                        OpenPageCollection.Add(new PageInfo() { HeaderName = module.Name, Body = dialog.GetView() });
                    }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
                }
                CurrentPage = OpenPageCollection[OpenPageCollection.Count - 1];
            }
            catch (Exception ex)
            {
                Msg.Error(ex.Message);
            }
            finally
            {
                Messenger.Default.Send(false, "PackUp");
                GC.Collect();
            }
        }

        /// <summary>
        /// 关闭页面
        /// </summary>
        /// <param name="module"></param>
        private void ExitPage(PageInfo module)
        {
            try
            {
                var tab = OpenPageCollection.FirstOrDefault(t => t.HeaderName.Equals(module.HeaderName));
                if (tab.HeaderName != "系统首页") OpenPageCollection.Remove(tab);
            }
            catch (Exception ex)
            {
                Msg.Error(ex.Message);
            }
        }

        #endregion

        #region 首页UI_Command

        private ExpansionState expansionState = ExpansionState.Open;

        private RelayCommand expansionCommand;
        private RelayCommand<string> inputChangeCommand;

        public RelayCommand ExpansionCommand
        {
            get
            {
                if (expansionCommand == null)
                    expansionCommand = new RelayCommand(() => expansionAction());
                return expansionCommand;
            }
        }

        public RelayCommand<string> InputChangeCommand
        {
            get
            {
                if (inputChangeCommand == null)
                    inputChangeCommand = new RelayCommand<string>((t) => inputEvent(t));
                return inputChangeCommand;
            }
        }

        private void expansionAction()
        {
            bool v = expansionState == ExpansionState.Close;
            expansionState = v ? ExpansionState.Open : ExpansionState.Close;
            Messenger.Default.Send(expansionState, "expansionCommand");
        }

        public void inputEvent(string input)
        {
            ModuleManager.Modules.Clear();
            if (string.IsNullOrWhiteSpace(input)) return;
            var groups = ModuleManager.ModuleGroups.Select(t => t.Modules.Where(q => q.Name.Contains(input)).ToList()).ToList();
            if (groups != null)
            {
                groups.ForEach(arg =>
                {
                    arg.ForEach(args =>
                    {
                        ModuleManager.Modules.Add(args);
                    });
                });
            }
        }


        #endregion
    }
}