using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ZFS.Client.LogicCore.Interface
{
    /// <summary>
    /// 弹窗接口(表单编辑页实现该接口)
    /// </summary>
    public interface IModelDialog
    {
        /// <summary>
        /// 关联默认上下文
        /// </summary>
        void BindDefaultViewModel();

        /// <summary>
        /// 关联数据上下文
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="viewModel"></param>
        void BindViewModel<TViewModel>(TViewModel viewModel);

        /// <summary>
        /// 注册窗口默认事件
        /// </summary>
        void RegisterDefaultEvent();

        /// <summary>
        /// 弹出窗口
        /// </summary>
        Task<bool> ShowDialog(DialogOpenedEventHandler openedEventHandler = null, DialogClosingEventHandler closingEventHandler = null);

        /// <summary>
        /// 关闭窗口
        /// </summary>
        void Close();

        /// <summary>
        /// 获取窗口
        /// </summary>
        /// <returns></returns>
        object GetDialog();

    }
}
