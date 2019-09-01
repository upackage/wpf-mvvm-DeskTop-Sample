using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ZFS.Client.LogicCore.Interface
{
    public interface IShowContent
    {
        void BindDataContext<T, V>(T control, V viewModel) where T : UserControl where V : class, new();

        void BindDataContext<V>(V viewModel) where V : class, new();

        Task<bool> Show();
    }
}
