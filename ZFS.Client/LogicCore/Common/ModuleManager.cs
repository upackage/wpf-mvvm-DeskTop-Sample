using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFS.Client.LogicCore.Configuration;
using ZFS.Client.LogicCore.Enums;
using ZFS.Client.LogicCore.UserAttribute;

namespace ZFS.Client.LogicCore.Common
{
    /// <summary>
    /// 模块组
    /// </summary>
    public class ModuleManager : ViewModelBase
    {
        public ModuleManager()
        {
            InitModuleGroups();
        }

        private void InitModuleGroups()
        {
            Array array = System.Enum.GetValues(typeof(ModuleType));

            foreach (var m in array)
            {
                ModuleType t = (ModuleType)m;
                var attr = GetEnumAttrbute.GetDescription(t);
                if (attr != null)
                    _ModuleGroups.Add(new ModuleGroup() { ModuleType = t, GroupName = attr.ModuleName, GroupIcon = attr.ModuleIcon });
            }
        }

        private ObservableCollection<Module> _Modules = new ObservableCollection<Module>();
        private ObservableCollection<ModuleGroup> _ModuleGroups = new ObservableCollection<ModuleGroup>();

        /// <summary>
        /// 已加载模块<含分组>
        /// </summary>
        public ObservableCollection<ModuleGroup> ModuleGroups
        {
            get { return _ModuleGroups; }
            set { _ModuleGroups = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 已加载模块
        /// </summary>
        public ObservableCollection<Module> Modules
        {
            get { return _Modules; }
            set { _Modules = value; RaisePropertyChanged(); }
        }
        
        /// <summary>
        /// 加载模块-根据权限
        public async Task LoadModules()
        {
            try
            {
                ModuleComponent loader = new ModuleComponent();
                var IModule = await loader.GetModules();
                foreach (var i in IModule)
                {
                    if (!loader.ModuleVerify(i)) continue;

                    var m = ModuleGroups.FirstOrDefault(t => t.ModuleType.Equals(i.ModuleType));
                    if (m != null)
                    {
                        if (m.Modules == null) m.Modules = new ObservableCollection<Module>();
                        int value = Loginer.LoginerUser.IsAdmin == true ? int.MaxValue : loader.Authority.authorities;
                        m.Modules.Add(new Module(i.Code, i.Name, value, i.ICON));
                    }
                }
                GC.Collect();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
