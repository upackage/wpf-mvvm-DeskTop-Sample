using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFS.Client.LogicCore.Configuration;
using ZFS.Client.LogicCore.Helpers;
using ZFS.Client.LogicCore.Helpers.Files;
using ZFS.Client.LogicCore.Interface;
using ZFS.Core.Interfaces;

namespace ZFS.Client.ViewModel
{
    /// <summary>
    /// 登录逻辑处理
    /// </summary>
    public class LoginViewModel : ViewModelBase
    {

        #region 用户名/密码

        private string _Report;
        private string userName = string.Empty;
        private string passWord = string.Empty;
        private bool _IsCancel = true;
        private bool _UserChecked;

        /// <summary>
        /// 数据库访问类型
        /// </summary>
        public string ServerType { get; set; }

        /// <summary>
        /// 皮肤样式
        /// </summary>
        public string SkinName { get; set; }

        /// <summary>
        /// 进度报告
        /// </summary>
        public string Report
        {
            get { return _Report; }
            set { _Report = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 记住密码
        /// </summary>
        public bool UserChecked
        {
            get { return _UserChecked; }
            set { _UserChecked = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return passWord; }
            set { passWord = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 禁用按钮
        /// </summary>
        public bool IsCancel
        {
            get { return _IsCancel; }
            set { _IsCancel = value; RaisePropertyChanged(); }
        }

        #endregion

        #region 命令(Binding Command)

        private RelayCommand _signCommand;

        public RelayCommand SignCommand
        {
            get
            {
                if (_signCommand == null)
                {
                    _signCommand = new RelayCommand(() => Login());
                }
                return _signCommand;
            }
        }

        private RelayCommand _exitCommand;

        public RelayCommand ExitCommand
        {
            get
            {
                if (_exitCommand == null)
                {
                    _exitCommand = new RelayCommand(() => ApplicationShutdown());
                }
                return _exitCommand;
            }
        }

        #endregion

        #region Login/Exit

        /// <summary>
        /// 登陆系统
        /// </summary>
        public async void Login()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
                {
                    this.Report = "请输入用户名密码";
                    return;
                }

                #region login

                this.IsCancel = false;
                var user = ServiceProvider.Instance.Get<IUserService>();
                this.Report = "正在验证登录 . . .";
                var LoginTask = user.LoginAsync(UserName, CEncoder.Encode(Password));
                var timeouttask = Task.Delay(3000);
                var completedTask = await Task.WhenAny(LoginTask, timeouttask);
                if (completedTask == timeouttask)
                {
                    this.Report = "系统连接超时,请联系管理员!";
                }
                else
                {
                    var task = await LoginTask;
                    if (task.success)
                    {
                        if (task.user == null)
                        {
                            this.Report = task.message;
                            return;
                        }
                        if (UserChecked) SaveLoginInfo();
                        var req = task.user;
                        #region 设置用户基础信息

                        Loginer.LoginerUser.Account = req.Account;
                        Loginer.LoginerUser.UserName = req.UserName;
                        Loginer.LoginerUser.IsAdmin = req.FlagAdmin == '1' ? true : false;
                        Loginer.LoginerUser.Email = req.Email;
                        var result =await user.LoginUserAuthAsync(req.Account);
                        if(result.success&&result.authorityEntity!=null)
                        {
                            Loginer.LoginerUser.authorityEntity = result.authorityEntity;
                        }

                        this.Report = "加载用户信息 . . .";

                        var dialog = ServiceProvider.Instance.Get<IModelDialog>("MainViewDlg");
                        dialog.BindDefaultViewModel();
                        Messenger.Default.Send(string.Empty, "ApplicationHiding");
                        bool taskResult = await dialog.ShowDialog();
                        this.ApplicationShutdown();

                        #endregion
                    }
                    else
                        this.Report = task.message;
                }

                #endregion
            }
            catch (Exception ex)
            {
                this.Report = ex.Message;
            }
            finally
            {
                this.IsCancel = true;
            }
        }

        /// <summary>
        /// 关闭系统
        /// </summary>
        public void ApplicationShutdown()
        {
            Messenger.Default.Send("", "ApplicationShutdown");
        }

        #endregion

        #region 记住密码

        /// <summary>
        /// 读取本地配置信息
        /// </summary>
        public void ReadConfigInfo()
        {
            string cfgINI = AppDomain.CurrentDomain.BaseDirectory + SerivceFiguration.INI_CFG;
            if (File.Exists(cfgINI))
            {
                IniFile ini = new IniFile(cfgINI);
                UserName = ini.IniReadValue("Login", "User");
                Password = CEncoder.Decode(ini.IniReadValue("Login", "Password"));
                UserChecked = ini.IniReadValue("Login", "SaveInfo") == "Y";
                SkinName = ini.IniReadValue("Skin", "Skin");
                ServerType = ini.IniReadValue("Server", "Bridge");
            }
        }

        /// <summary>
        /// 保存登录信息
        /// </summary>
        private void SaveLoginInfo()
        {
            string cfgINI = AppDomain.CurrentDomain.BaseDirectory + SerivceFiguration.INI_CFG;
            IniFile ini = new IniFile(cfgINI);
            ini.IniWriteValue("Login", "User", UserName);
            ini.IniWriteValue("Login", "Password", CEncoder.Encode(Password));
            ini.IniWriteValue("Login", "SaveInfo", UserChecked ? "Y" : "N");
        }

        #endregion
    }
}
