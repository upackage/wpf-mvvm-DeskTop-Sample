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
    public partial class BaseOperation<T> : ViewModelBase where T : class, new()
    {
        #region BaseProperty  [Query、Button、GridModel]

        private string searchText = string.Empty;
        private ObservableCollection<T> _GridModelList;
        private ObservableCollection<ToolBarDefault<T>> _ButtonDefaults;

        /// <summary>
        /// 搜索内容
        /// </summary>
        public string SearchText
        {
            get { return searchText; }
            set { searchText = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 表单数据
        /// </summary>
        public ObservableCollection<T> GridModelList
        {
            get { return _GridModelList; }
            set { _GridModelList = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 按钮
        /// </summary>
        public ObservableCollection<ToolBarDefault<T>> ButtonDefaults
        {
            get { return _ButtonDefaults; }
            set { _ButtonDefaults = value; RaisePropertyChanged(); }
        }

        #endregion

        #region Initialization parameters

        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Init()
        {
            GridModelList = new ObservableCollection<T>();
            ButtonDefaults = new ObservableCollection<ToolBarDefault<T>>();
            this.SetDefaultButton(); //默认功能按钮
            this.LoadModuleAuth();//加载模块权限
            this.GetPageData(this.PageIndex); //获取首次加载数据
        }

        /// <summary>
        /// 设置默认按钮
        /// </summary>
        public virtual void SetDefaultButton()
        {
            ButtonDefaults.Add(new ToolBarDefault<T>() { AuthValue = Authority.ADD, ModuleName = "新增", Command = this.AddCommand });
            ButtonDefaults.Add(new ToolBarDefault<T>() { AuthValue = Authority.EDIT, ModuleName = "编辑", Command = this.EditCommand, Hide = true });
            ButtonDefaults.Add(new ToolBarDefault<T>() { AuthValue = Authority.DELETE, ModuleName = "删除", Command = this.DelCommand, Hide = true });
        }

        #endregion

        #region Command

        private RelayCommand<T> _AddCommand;
        private RelayCommand<T> _EditCommand;
        private RelayCommand<T> _DelCommand;
        private RelayCommand _QueryCommand;
        private RelayCommand _ResetCommand;
        private RelayCommand _SaveCommand;
        private RelayCommand _CancelCommand;

        /// <summary>
        /// 新增
        /// </summary>
        public RelayCommand<T> AddCommand
        {
            get
            {
                if (_AddCommand == null) _AddCommand = new RelayCommand<T>(t => Add(t));
                return _AddCommand;
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        public RelayCommand<T> EditCommand
        {
            get
            {
                if (_EditCommand == null) _EditCommand = new RelayCommand<T>(t => Edit(t));
                return _EditCommand;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        public RelayCommand<T> DelCommand
        {
            get
            {
                if (_DelCommand == null) _DelCommand = new RelayCommand<T>(t => Del(t));
                return _DelCommand;
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        public RelayCommand QueryCommand
        {
            get
            {
                if (_QueryCommand == null) _QueryCommand = new RelayCommand(() => Query());
                return _QueryCommand;
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        public RelayCommand ResetCommand
        {
            get
            {
                if (_ResetCommand == null) _ResetCommand = new RelayCommand(() => Reset());
                return _ResetCommand;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        public RelayCommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null) _SaveCommand = new RelayCommand(() => Save());
                return _SaveCommand;
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        public RelayCommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null) _CancelCommand = new RelayCommand(() => Cancel());
                return _CancelCommand;
            }
        }



        #endregion

        #region TabPageIndex/Detail

        private T _Model;
        private int tabpageIndex;
        public int TabPageIndex { get { return tabpageIndex; } set { tabpageIndex = value; RaisePropertyChanged();  } }
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
    public partial class BaseOperation<T> : LogicCore.Interface.IPermission, IDataPager
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
