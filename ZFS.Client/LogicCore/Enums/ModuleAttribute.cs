using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFS.Client.LogicCore.UserAttribute;

namespace ZFS.Client.LogicCore.Enums
{
    /// <summary>
    /// 模块类型
    /// </summary>
    public enum ModuleType
    {
        //[Description("产品信息", "\xe642")]
        //Product,

        //[Description("生产计划", "\xe63a")]
        //ProductionPlan,

        //[Description("仓库管理", "\xe81b")]
        //WarehouseManagement,

        //[Description("设备管理", "\xe64a")]
        //EquipmentManagement,

        //[Description("插件管理", "\xe63b")]
        //PluginManagement,

        //[Description("参数配置", "\xe6ee")]
        //ParameterConfiguration,

        //[Description("调试软件", "\xe629")]
        //DebuggingSoftware,

        //[Description("演示平台", "\xe667")]
        //DemoPlatform,

        //[Description("演示软件", "\xe6a0")]
        //DemoSoftware,

        //[Description("标签功能", "\xe75c")]
        //LabelFunction,

        [Description("基础数据", "\xe74b")]
        BasicData,

        //[Description("公共数据", "\xe610")]
        //PublicData,

        //[Description("文档中心", "\xe64d")]
        //DocumentCenter,

        [Description("系统设置", "\xe644")]
        SystemSettings,
    }

    /// <summary>
    /// 模块特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ModuleAttribute : Attribute
    {
        /// <summary>
        /// 模块构造函数
        /// </summary>
        /// <param name="code">模块编码</param>
        /// <param name="name">模块名称</param>
        /// <param name="icon">ICON</param>
        public ModuleAttribute(ModuleType moduleType, string code, string name, string icon = "")
        {
            this.moduleType = moduleType;
            this.code = code;
            this.name = name;
            this.icon = icon;
        }

        #region private

        private ModuleType moduleType;
        private string code;
        private string name;
        private string icon;

        #endregion

        #region 只读属性

        public string Code { get { return code; } }

        /// <summary>
        /// 图标
        /// </summary>
        public string ICON
        {
            get { return icon; }
        }

        /// <summary>
        /// 菜单名
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        public ModuleType ModuleType
        {
            get { return moduleType; }
        }

        public string ModuleTypeName
        {
            get { return GetEnumAttrbute.GetDescription(ModuleType).ModuleName; }
        }


        #endregion
    }

    /// <summary>
    /// 模块类型特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class DescriptionAttribute : Attribute
    {
        protected string _ModuleName = string.Empty;
        protected string _ModuleIcon = string.Empty;

        public string ModuleName { get { return _ModuleName; } }
        public string ModuleIcon { get { return _ModuleIcon; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="caption">显示名称</param>
        /// <param name="disableLoading">禁止模块加载</param>
        /// <param name="ico">Logo</param>
        public DescriptionAttribute(string caption, string ico = "BorderAll")
        {
            this._ModuleName = caption;
            this._ModuleIcon = ico;
        }

    }
}
