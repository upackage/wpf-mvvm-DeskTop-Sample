using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ZFS.Client.LogicCore.Interface;

namespace ZFS.Client.ViewModel.VMBase
{
    /// <summary>
    /// 基层中间层模型 构建View/ViewModel 中介
    /// </summary>
    /// <typeparam name="View">视图</typeparam>
    /// <typeparam name="TViewModel">逻辑层</typeparam>
    public class BaseView<View, TViewModel> : IModel
        where View : UserControl, new()
        where TViewModel : new()
    {
        /// <summary>
        /// 泛型视图
        /// </summary>
        public View TView;

        /// <summary>
        /// 泛型ViewModel
        /// </summary>
        public TViewModel SViewModel;

        /// <summary>
        /// 绑定上下文(默认)
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="viewModel"></param>
        public void BindViewModel<T>(T viewModel)
        {
            this.GetView().DataContext = viewModel;
        }

        /// <summary>
        /// 绑定默认视图
        /// </summary>
        public void BindDefaultModel()
        {
            if (SViewModel == null) SViewModel = new TViewModel();
            this.GetView().DataContext = SViewModel;
        }

        /// <summary>
        /// 获取视图页
        /// </summary>
        /// <returns></returns>
        public UserControl GetView()
        {
            if (TView == null) TView = new View();
            return TView;
        }
    }
}
