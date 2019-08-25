using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFS.Model.RequestModel;

namespace ZFS.Service
{
    public class BaseServiceRequest<T>
    {
        /// <summary>
        /// 带证书的http请求
        /// </summary>
        public HttpCertificateMethod certHttp = new HttpCertificateMethod();

        /// <summary>
        /// 获取证书获取字符串请求
        /// </summary> 
        /// <param name="dto"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public async Task<T> GetRequest(BaseRequest br, Method method = Method.GET)
        {
            string pms = br.GetPropertiesObject();
            string url = br.route + (method == Method.GET ? pms : string.Empty);
            string resultString = await certHttp.RequestBehavior(method, url, pms);
            T result = JsonConvert.DeserializeObject<T>(resultString);
            return result;
        }
    }
}
