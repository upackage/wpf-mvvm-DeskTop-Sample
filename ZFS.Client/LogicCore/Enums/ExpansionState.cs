using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZFS.Client.LogicCore.Enums
{
    /// <summary>
    /// 菜单展开状态
    /// </summary>
    public enum ExpansionState
    {
        Open,
        Close,
    }

    public class AnimationDesgin
    {
        /// <summary>
        ///  DoubleAnimation
        /// </summary>
        /// <param name="dependency"></param>
        /// <param name="Width"></param>
        /// <param name="TimeSpan"></param>
        public static void StoryBoard(DependencyObject dependency, double Form, double To, int TimeSpan, string PropertyName)
        {
            System.Windows.Media.Animation.Storyboard sb = new System.Windows.Media.Animation.Storyboard();
            System.Windows.Media.Animation.DoubleAnimation dmargin = new System.Windows.Media.Animation.DoubleAnimation();
            dmargin.Duration = new TimeSpan(0, 0, 0, 0, TimeSpan);
            dmargin.From = Form;
            dmargin.To = To;
            System.Windows.Media.Animation.Storyboard.SetTarget(dmargin, dependency);
            System.Windows.Media.Animation.Storyboard.SetTargetProperty(dmargin, new PropertyPath(PropertyName, new object[] { }));
            sb.Children.Add(dmargin);
            sb.Begin();
        }

    }
}
