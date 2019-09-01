using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZFS.Client.LogicCore.Common;
using ZFS.Client.LogicCore.Enums;
using ZFS.Client.ViewModel;

namespace ZFS.Client.UiCore.Template
{
    /// <summary>
    /// MainTabControl.xaml 的交互逻辑
    /// </summary>
    public partial class MainTabControl : UserControl
    {
        public MainTabControl()
        {
            InitializeComponent();
        }
        

        private void ExitCommand(MenuBehaviorType type, string pageName)
        {
            var obj = this.DataContext as MainViewModel;
            if (obj == null) return;
            switch (type)
            {
                case MenuBehaviorType.ExitCurrentPage:
                    obj.ExitPage(MenuBehaviorType.ExitCurrentPage, pageName);
                    break;
                case MenuBehaviorType.ExitAllPage:
                    obj.ExitPage(MenuBehaviorType.ExitAllPage, pageName);
                    break;
                case MenuBehaviorType.ExitAllExcept:
                    obj.ExitPage(MenuBehaviorType.ExitAllExcept, pageName);
                    break;
            }
        }

        private void ExitCurrentPage_Click(object sender, RoutedEventArgs e)
        {
            var pageInfo = (sender as MenuItem).DataContext as PageInfo;
            ExitCommand(MenuBehaviorType.ExitCurrentPage, pageInfo.HeaderName);
        }

        private void ExitAllPage_Click(object sender, RoutedEventArgs e)
        {
            var pageInfo = (sender as MenuItem).DataContext as PageInfo;
            ExitCommand(MenuBehaviorType.ExitAllPage, pageInfo.HeaderName);
        }

        private void ExitAllExcept_Click(object sender, RoutedEventArgs e)
        {
            var pageInfo = (sender as MenuItem).DataContext as PageInfo;
            ExitCommand(MenuBehaviorType.ExitAllExcept, pageInfo.HeaderName);
        }
    }
}
