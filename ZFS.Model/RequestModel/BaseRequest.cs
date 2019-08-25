using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ZFS.Model.RequestModel
{
    public class BaseRequest
    {
        private readonly string _basePath = ConfigurationManager.AppSettings["ServerIP"];

        [Prevent]
        public virtual string ServerIP
        {
            get { return _basePath; }
        }

        /// <summary>
        /// 路由地址
        /// </summary>
        [Prevent]
        public virtual string route { get; set; }

        /// <summary>
        /// 获取请求对象得属性转换值
        /// </summary>
        /// <returns></returns>
        public string GetPropertiesObject()
        {
            StringBuilder builder = new StringBuilder();
            var type = this.GetType();
            var propertyArray = type.GetProperties();
            if (propertyArray != null && propertyArray.Length > 0)
            {
                foreach (PropertyInfo property in propertyArray)
                {
                    var prevent = property.GetCustomAttribute<PreventAttribute>();
                    if (prevent != null)
                        continue;
                    var pvalue = property.GetValue(this);
                    var str = pvalue.GetType().Namespace;
                    if (pvalue != null && pvalue.GetType().Namespace == "ZFS.Model.Query")
                    {
                        //当参数作为Query类型是, 则进行拆解对象拼接字符串
                        StringBuilder pbuilder = new StringBuilder();
                        var QpropertyArray = pvalue.GetType().GetProperties();
                        if (QpropertyArray != null && QpropertyArray.Length > 0)
                        {
                            foreach (PropertyInfo Qproperty in QpropertyArray)
                            {
                                var Qprevent = Qproperty.GetCustomAttribute<PreventAttribute>();
                                if (Qprevent != null)
                                    continue;
                                var Qpvalue = Qproperty.GetValue(pvalue);
                                if (Qpvalue != null && Qpvalue.ToString() != "")
                                {
                                    if (pbuilder.ToString() == string.Empty) pbuilder.Append("?");
                                    pbuilder.Append(Qproperty.Name + "=" + HttpUtility.UrlEncode(Convert.ToString(Qpvalue)) + "&");
                                }
                            }
                        }
                        builder.Append(pbuilder.ToString());
                    }
                    else if (pvalue != null && pvalue.GetType().Namespace == "ZFS.Model.RequestModel")
                    {
                        //当属性为对象得情况下, 进行序列化
                        pvalue = JsonConvert.SerializeObject(pvalue);
                        builder.Append(pvalue);
                    }
                    else
                    {
                        //当属性为C#基础类型得情况下,默认Get传参
                        if (builder.ToString() == string.Empty) builder.Append("?");
                        builder.Append(property.Name + "=" + HttpUtility.UrlEncode(Convert.ToString(pvalue)) + "&");
                    }
                }
            }
            return builder.ToString().Trim('&');
        }
    }
}
