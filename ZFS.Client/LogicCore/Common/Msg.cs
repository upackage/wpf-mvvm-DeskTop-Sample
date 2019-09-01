using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFS.Client.LogicCore.Configuration;
using ZFS.Client.LogicCore.Interface;
using ZFS.Client.UiCore.Template;

namespace ZFS.Client.LogicCore.Common
{
    public enum Notify
    {
        /// <summary>
        /// 错误
        /// </summary>
        [System.ComponentModel.Description("错误")]
        Error,
        /// <summary>
        /// 警告
        /// </summary>
        [System.ComponentModel.Description("警告")]
        Warning,
        /// <summary>
        /// 提示信息
        /// </summary>
        [System.ComponentModel.Description("提示信息")]
        Info,
        /// <summary>
        /// 询问信息
        /// </summary>
        [System.ComponentModel.Description("询问信息")]
        Question,
    }

    /// <summary>
    /// 消息类
    /// </summary>
    public class Msg
    {

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="msg"></param>
        public static async void Error(string ex, bool Host = false)
        {
            await Show(Notify.Error, ex, Host);
        }

        /// <summary>
        /// 信息提示
        /// </summary>
        /// <param name="msg"></param>
        public static async void Info(string ex, bool Host = false)
        {
            await Show(Notify.Info, ex, Host);
        }

        /// <summary>
        /// 真香警告
        /// </summary>
        /// <param name="msg"></param>
        public static async void Warning(string ex, bool Host = false)
        {
            await Show(Notify.Warning, ex, Host);
        }

        /// <summary>
        /// 真香询问
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static async Task<bool> Question(string ex, bool Host = false)
        {
            return await Show(Notify.Question, ex, Host);
        }

        /// <summary>
        /// 弹出窗口
        /// </summary>
        /// <param name="notify">类型</param>
        /// <param name="msg">文本信息</param>
        /// <returns></returns>
        private static async Task<bool> Show(Notify notify, string msg, bool Host = false)
        {
            string Icon = string.Empty;
            string Color = string.Empty;
            bool Hide = true;
            switch (notify)
            {
                case Notify.Error:
                    Icon = "CommentRemoveOutline";
                    Color = "#FF4500";
                    break;
                case Notify.Warning:
                    Icon = "CommentWarning";
                    Color = "#FF8247";
                    break;
                case Notify.Info:
                    Icon = "CommentProcessingOutline";
                    Color = "#1C86EE";
                    break;
                case Notify.Question:
                    Icon = "CommentQuestionOutline";
                    Color = "#20B2AA";
                    Hide = false;
                    break;
            }

            var dialog = ServiceProvider.Instance.Get<IShowContent>();
            dialog.BindDataContext(new MsgBox(), new MsgBoxViewModel() { Msg = msg, Icon = Icon, Color = Color, BtnHide = Hide });
            var result = await dialog.Show();
            return result;
        }
    }
}
