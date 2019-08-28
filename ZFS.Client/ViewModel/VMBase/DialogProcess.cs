using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZFS.Client.ViewModel.VMBase
{
    /// <summary>
    /// 弹出式窗口基类
    /// </summary>
    public class DialogProcess : ViewModelBase
    {
        public bool Result { get; set; }

        private RelayCommand _CancelCommand;
        private RelayCommand _SaveCommand;

        public RelayCommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                    _CancelCommand = new RelayCommand(() => Cancel());
                return _CancelCommand;
            }
        }

        public RelayCommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                    _SaveCommand = new RelayCommand(() => Save());
                return _SaveCommand;
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        public void Cancel()
        {
            Result = false;
            Messenger.Default.Send("", "DialogClose");
        }

        /// <summary>
        /// 确定
        /// </summary>
        public void Save()
        {
            Result = true;
            Messenger.Default.Send("", "DialogClose");
        }


    }
}
