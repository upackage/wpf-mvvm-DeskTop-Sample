using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using ZFS.Client.LogicCore.Common;
using ZFS.Client.LogicCore.Configuration;
using ZFS.Client.LogicCore.Enums;
using ZFS.Client.LogicCore.Interface;

namespace ZFS.Client.ViewModel.VMBase
{
    /// <summary>
    /// 主窗口基类
    /// </summary>
    public partial class DataProcess<T> : ViewModelBase where T : class, new()
    {
        #region BaseProperty  [Query、Button、GridModel]

        private string searchText = string.Empty;
        private ObservableCollection<T> gridModelList;
        private ObservableCollection<ToolBarDefault<T>> buttonDefaults;
        private ObservableCollection<ToolBarDefault<T>> detailButtonDefaults;
        private ObservableCollection<ToolBarDefault<T>> toolButtonsDefaults;

        /// <summary>
        /// 搜索内容
        /// </summary>
        public string SearchText
        {
            get { return searchText; }
            set { searchText = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 抽象表单数据
        /// </summary>
        public ObservableCollection<T> GridModelList
        {
            get { return gridModelList; }
            set { gridModelList = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 表单按钮
        /// </summary>
        public ObservableCollection<ToolBarDefault<T>> ButtonDefaults
        {
            get { return buttonDefaults; }
            set { buttonDefaults = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 详细按钮
        /// </summary>
        public ObservableCollection<ToolBarDefault<T>> DetailButtonDefaults
        {
            get { return detailButtonDefaults; }
            set { detailButtonDefaults = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 工具按钮
        /// </summary>
        public ObservableCollection<ToolBarDefault<T>> ToolButtonsDefaults
        {
            get { return toolButtonsDefaults; }
            set { toolButtonsDefaults = value; RaisePropertyChanged(); }
        }

        #endregion

        #region Initialization parameters

        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Init()
        {
            GridModelList = new ObservableCollection<T>();
            this.SetDefaultButton(); //默认功能按钮
            this.LoadModuleAuth();//加载模块权限
            this.GetPageData(this.PageIndex); //获取首次加载数据
        }

        /// <summary>
        /// 设置默认按钮
        /// </summary>
        public virtual void SetDefaultButton()
        {
            //表单按钮
            ButtonDefaults = new ObservableCollection<ToolBarDefault<T>>();
            ButtonDefaults.Add(new ToolBarDefault<T>() { AuthValue = Authority.ADD, ModuleName = "新增", IconSting = "Plus", Command = new RelayCommand<T>(t => Add(t)) });
            //ButtonDefaults.Add(new ToolBarDefault<T>() { AuthValue = Authority.EDIT, ModuleName = "编辑", IconSting = "Pencil", Command = new RelayCommand<T>(t => Edit(t)), Hide = true });
            //ButtonDefaults.Add(new ToolBarDefault<T>() { AuthValue = Authority.DELETE, ModuleName = "删除", IconSting = "BookmarkRemove", Command = new RelayCommand<T>(t => Del(t)), Hide = true });

            //编辑栏按钮
            DetailButtonDefaults = new ObservableCollection<ToolBarDefault<T>>();
            DetailButtonDefaults.Add(new ToolBarDefault<T>() { ModuleName = "保存", IconSting = "Check", Command = new RelayCommand<T>(t => Save()) });
            DetailButtonDefaults.Add(new ToolBarDefault<T>() { ModuleName = "取消", IconSting = "Close", Command = new RelayCommand<T>(t => Cancel()) });

            //工具按钮
            ToolButtonsDefaults = new ObservableCollection<ToolBarDefault<T>>();
            ToolButtonsDefaults.Add(new ToolBarDefault<T>() { ModuleName = "查询", IconSting = "Magnify", Command = new RelayCommand<T>(t => Query()) });
            ToolButtonsDefaults.Add(new ToolBarDefault<T>() { ModuleName = "重置", IconSting = "Refresh", Command = new RelayCommand<T>(t => Reset()) });
        }

        #endregion

        #region TabPageIndex/Detail

        private T _Model;
        private int tabpageIndex;
        public int TabPageIndex { get { return tabpageIndex; } set { tabpageIndex = value; RaisePropertyChanged(); } }
        public ActionMode Mode { get; set; }

        /// <summary>
        /// 操作实体
        /// </summary>
        public T Model
        {
            get { return _Model; }
            set { _Model = value; RaisePropertyChanged(); }
        }

        #endregion
    }

    /// <summary>
    /// 主窗口/分布基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class DataProcess<T> : IPermissions, IDataPager
    {
        #region IPermission

        protected int? authValue;

        /// <summary>
        /// 权限值
        /// </summary>
        public int? AuthValue { get { return authValue; } set { authValue = value; } }


        /// <summary>
        /// 验证按钮权限
        /// </summary>
        /// <param name="authValue"></param>
        /// <returns></returns>
        public bool GetButtonAuth(int authValue)
        {
            var def = ButtonDefaults.FirstOrDefault(t => (AuthValue & t.AuthValue) == authValue);
            if (def != null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 设置权限
        /// </summary>
        public virtual void LoadModuleAuth()
        {
            if (Loginer.LoginerUser.IsAdmin) return;
            foreach (var b in ButtonDefaults)
                if ((this.AuthValue & b.AuthValue) != b.AuthValue)
                    b.Hide = true; //隐藏功能
        }


        #endregion

        #region IDataPager

        public RelayCommand GoHomePageCommand { get { return new RelayCommand(() => GoHomePage()); } }

        public RelayCommand GoOnPageCommand { get { return new RelayCommand(() => GoOnPage()); } }

        public RelayCommand GoNextPageCommand { get { return new RelayCommand(() => GoNextPage()); } }

        public RelayCommand GoEndPageCommand { get { return new RelayCommand(() => GoEndPage()); } }


        private int totalCount = 0;
        private int pageSize = 15;
        private int pageIndex = 1;
        private int pageCount = 0;

        /// <summary>
        /// 总数
        /// </summary>
        public int TotalCount { get { return totalCount; } set { totalCount = value; RaisePropertyChanged(); } }

        /// <summary>
        /// 当前页大小
        /// </summary>
        public int PageSize { get { return pageSize; } set { pageSize = value; RaisePropertyChanged(); } }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get { return pageIndex; } set { pageIndex = value; RaisePropertyChanged(); } }

        /// <summary>
        /// 分页总数
        /// </summary>
        public int PageCount { get { return pageCount; } set { pageCount = value; RaisePropertyChanged(); } }

        /// <summary>
        /// 首页
        /// </summary>
        public virtual void GoHomePage()
        {
            if (this.PageIndex == 1) return;

            PageIndex = 1;

            GetPageData(PageIndex);
        }

        /// <summary>
        /// 上一页
        /// </summary>
        public virtual void GoOnPage()
        {
            if (this.PageIndex == 1) return;

            PageIndex--;

            this.GetPageData(PageIndex);
        }

        /// <summary>
        /// 下一页
        /// </summary>
        public virtual void GoNextPage()
        {
            if (this.PageIndex == PageCount) return;

            PageIndex++;

            this.GetPageData(PageIndex);
        }

        /// <summary>
        /// 尾页
        /// </summary>
        public virtual void GoEndPage()
        {
            this.PageIndex = PageCount;

            GetPageData(PageCount);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="pageIndex"></param>
        public virtual void GetPageData(int pageIndex) { }

        /// <summary>
        /// 设置页数
        /// </summary>
        public virtual void SetPageCount()
        {
            PageCount = Convert.ToInt32(Math.Ceiling((double)TotalCount / (double)PageSize));
        }

        #endregion

        #region IDataOperation

        /// <summary>
        /// 新增
        /// </summary>
        public virtual void Add<TModel>(TModel model)
        {

            Model = model as T;
            TabPageIndex = 1;
            Mode = ActionMode.Add;
        }

        /// <summary>
        /// 编辑
        /// </summary>
        public virtual void Edit<TModel>(TModel model)
        {
            Model = model as T;
            TabPageIndex = 1;
            Mode = ActionMode.Edit;
        }

        /// <summary>
        /// 删除
        /// </summary>
        public virtual void Del<TModel>(TModel model) { }

        /// <summary>
        /// 查询
        /// </summary>
        public virtual void Query()
        {
            this.GetPageData(this.PageIndex);
        }

        /// <summary>
        /// 重置
        /// </summary>
        public virtual void Reset()
        {
            this.SearchText = string.Empty;
        }

        /// <summary>
        /// 保存
        /// </summary>
        public virtual void Save()
        {
            TabPageIndex = 0;
        }

        /// <summary>
        /// 返回
        /// </summary>
        public virtual void Cancel()
        {
            TabPageIndex = 0;
        }

        #endregion
    }
}
