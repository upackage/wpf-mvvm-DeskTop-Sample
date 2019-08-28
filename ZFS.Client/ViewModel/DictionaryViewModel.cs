using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFS.Client.LogicCore.Common;
using ZFS.Client.LogicCore.Configuration;
using ZFS.Client.LogicCore.Enums;
using ZFS.Client.ViewModel.VMBase;
using ZFS.Core.Interfaces;
using ZFS.Model.Entity;
using ZFS.Model.Query;

namespace ZFS.Client.ViewModel
{
    /// <summary>
    /// 数据字典
    /// </summary>
    [Module(ModuleType.BasicData, "DictionaryDlg", "字典管理")]
    public class DictionaryViewModel : DataProcess<Dictionaries>
    {
        private readonly IDictionariesService service;

        public DictionaryViewModel()
        {
            service = ServiceProvider.Instance.Get<IDictionariesService>();
            this.Init();
        }

        public List<DictionaryType> TypeList { get; set; }

        public override void LoadModuleAuth()
        {
            var customs = this.GetType().GetCustomAttributes(typeof(ModuleAttribute), false);
            if (customs.Length > 0)
            {
                var attr = (ModuleAttribute)customs[0];
                //访问缓存权限,设置AuthValue
            }
            base.LoadModuleAuth();
        }

        public override async void GetPageData(int pageIndex)
        {
            try
            {
                var r = await service.GetDictionariesAsync(new DictionariesParameters()
                {
                    PageIndex = pageIndex,
                    PageSize = PageSize,
                    Search = SearchText,
                });
                if (r.success)
                {
                    TotalCount = r.TotalRecord;
                    GridModelList.Clear();
                    r.Dictionaries.ForEach((arg) => GridModelList.Add(arg));
                    base.SetPageCount();
                }
            }
            catch (Exception ex)
            {
                Msg.Error(ex.Message);
            }
        }

        public override async void Add<TModel>(TModel model)
        {
            base.Add(model);
        }

        public override async void Edit<TModel>(TModel model)
        {
            if (!this.GetButtonAuth(Authority.EDIT)) return;
            
            base.Edit(model);
        }

        public override async void Del<TModel>(TModel model)
        {
            if (!this.GetButtonAuth(Authority.DELETE)) return;

            if (Model != null)
            {
                bool result = await Msg.Question($"确认删除字典：{Model.NativeName}?");
                try
                {
                    //if (!result) return;
                    //var DicSerivce = ServiceProvider.Instance.Get<IBridgeManager>(Loginer.LoginerUser.ServerBridgeType).GetDictionaryManager();
                    //var req = await DicSerivce.DeleteEntity(Model);
                    //if (req.Success)
                    //{
                    //    var mod = GridModelList.FirstOrDefault(t => t.isid.Equals(Model.isid));
                    //    GridModelList.Remove(mod);
                    //    Msg.Info("操作成功!");
                    //}
                    //else
                    //    Msg.Error(req.ErrorCode);
                }
                catch (Exception ex)
                {
                    Msg.Error(ex.Message);
                }
            }
        }

        public override async void Save()
        {
            if (string.IsNullOrWhiteSpace(Model.DataCode) ||
                string.IsNullOrWhiteSpace(Model.NativeName))
            {
                return;
            }

            if (Mode == ActionMode.Add)
            {
                Model.CreateBy = Loginer.LoginerUser.UserName;
                Model.CreateTime = DateTime.Now;
                //var req = await ServiceProvider.Instance.Get<IDictionary>().AddEntity(Model);
                //if (req.Success)
                //{
                //    IsChecked = false;
                //    Msg.Info("添加成功!");
                //    GridModelList.Add(Model);
                //}
            }
            else if (Mode == ActionMode.Edit)
            {
                //Model.LastUpdatedBy = Loginer.LoginerUser.UserName;
                //Model.LastUpdateDate = DateTime.Now;
                //var dicSerivce = ServiceProvider.Instance.Get<IBridgeManager>(Loginer.LoginerUser.ServerBridgeType).GetDictionaryManager();
                //var req = await dicSerivce.UpdateEntity(Model);
                //if (req.Success)
                //{
                //    IsChecked = false;
                //    Msg.Info("更新成功!");
                //}
            }
        }
    }
}
