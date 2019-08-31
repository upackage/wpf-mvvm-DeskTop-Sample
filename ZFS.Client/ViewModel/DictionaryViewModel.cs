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
        
        public override async void Del<TModel>(TModel model)
        {

        }

        public override async void Save()
        {

        }
    }
}
