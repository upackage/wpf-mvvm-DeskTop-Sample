using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFS.Client.LogicCore.Enums;

namespace ZFS.Client.LogicCore.Common
{
    /// <summary>
    /// 模块组
    /// </summary>
    public class ModuleGroup : ViewModelBase
    {
        private int groupid;
        private string _groupIcon = "BlockHelper";
        private string _groupName;
        private ModuleType _moduleType;
        private ObservableCollection<Module> modules = new ObservableCollection<Module>();

        /// <summary>
        /// 模块ICO
        /// </summary>
        public string GroupIcon
        {
            get { return _groupIcon; }
            set { _groupIcon = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string GroupName
        {
            get { return _groupName; }
            set { _groupName = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 模块类型
        /// </summary>
        public ModuleType ModuleType
        {
            get { return _moduleType; }
            set { _moduleType = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 父模块ID
        /// </summary>
        public int GroupId
        {
            get { return groupid; }
            set { groupid = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 子模块集合
        /// </summary>
        public ObservableCollection<Module> Modules
        {
            get { return modules; }
            set { modules = value; RaisePropertyChanged(); }
        }
    }

    /// <summary>
    /// 模块类
    /// </summary>
    public class Module
    {
        public Module(string Code, string Name, int? Auth, string Icon)
        {
            _Code = Code;
            _Name = Name;
            _Authorities = Auth;
            _Icon = Icon;
        }

        private string _Code;
        private string _Name;
        private int? _Authorities;
        private string _Icon;


        public string Code
        {
            get { return _Code; }
        }

        /// <summary>
        /// 图标-IconFont
        /// </summary>
        public string ICON
        {
            get { return _Icon; }
        }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
        }

        /// <summary>
        /// 权限值
        /// </summary>
        public int? Authorities
        {
            get { return _Authorities; }
        }

    }
}
