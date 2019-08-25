using GalaSoft.MvvmLight.Messaging;
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
using ZFS.Client.LogicCore.Enums;

namespace ZFS.Client
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Zone.MouseDoubleClick += (sender, e) => { Max(); };
            //listMain.PreviewMouseWheel += (sender, e) =>
            //{
            //    var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
            //    eventArg.RoutedEvent = UIElement.MouseWheelEvent;
            //    eventArg.Source = sender;
            //    listMain.RaiseEvent(eventArg);
            //};
            Messenger.Default.Register<ExpansionState>(this, "expansionCommand", expansionComand);
        }
        
        #region Messenger

        /// <summary>
        /// 最大化
        /// </summary>
        /// <param name="msg"></param>
        public void Max(bool Mask = false)
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else
                this.WindowState = WindowState.Maximized;
        }

        #endregion

        private void expansionComand(ExpansionState expansion)
        {
            //if (expansion == ExpansionState.Close)
            //    AnimationDesgin.StoryBoard(glf, 80, 250, 300, "Width");
            //else
            //    AnimationDesgin.StoryBoard(glf, 250, 80, 300, "Width");
        }
    }
}
