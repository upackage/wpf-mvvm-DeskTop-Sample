using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZFS.Model.Entity;
using ZFS.Model.Query;
using ZFS.Model.ResponseModel;

namespace ZFS.Core.Interfaces
{
    public interface IDictionariesService
    {
        Task<DictionariesResponse> GetDictionariesAsync(DictionariesParameters parameters);

    }
}
