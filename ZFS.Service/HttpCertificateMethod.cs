using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ZFS.Service
{
    /// <summary>
    /// HTTP方法封装
    /// </summary>
    public class HttpCertificateMethod
    {
        private string _header = String.Empty;

        /// <summary>
        /// Http头数据信息添加
        /// </summary>
        public string HeaderTxt
        {
            get { return _header; }
            set { _header = value; }
        }
        /// <summary>
        /// 请求数据信息
        /// </summary>
        public async Task<string> RequestBehavior(Method method, string url, string pms, bool isneedsession = false, bool istreatment = false, bool isJson = true)
        {
            try
            {
                //验证服务器证书回调方法 
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                //创建HttpWebRequest对象 
                HttpWebRequest https = (HttpWebRequest)WebRequest.Create(url);
                //方式方法
                https.Method = method.ToString();

                //改接口需要Session来处理
                if (isneedsession)
                {
                    https.Headers.Add("token", GlobalData.Token);
                }

                switch (https.Method.ToLower())
                {
                    case "post":
                        https.KeepAlive = true;
                        if (isJson)
                        {
                            https.ContentType = "application/json";
                            byte[] bytes = Encoding.UTF8.GetBytes(pms);
                            https.ContentLength = bytes.Length;
                            Stream reqstream = https.GetRequestStream();
                            reqstream.Write(bytes, 0, bytes.Length);
                        }
                        else
                        {
                            https.ContentType = "application/x-www-form-urlencoded";
                            using (StreamWriter sw = new StreamWriter(https.GetRequestStream()))
                            {
                                try
                                {
                                    sw.Write(pms);
                                }
                                catch
                                {
                                    throw new Exception("写入请求流数据错误");
                                }
                            }
                        }
                        break;
                    case "get":
                        https.Connection = "application/json";
                        break;
                    default:
                        break;
                }
                //获取请求返回的数据 
                var response = (HttpWebResponse)await https.GetResponseAsync();

                ///如果是需要处理头部的
                if (istreatment)
                {
                    string cookies = response.Headers["Set-Cookie"];
                    string[] strarray = cookies.Split(';');
                    //数组处理
                    if (strarray != null
                        && strarray.Length > 0)
                    {
                        foreach (string arr in strarray)
                        {
                            if (arr.Contains("SESSION")
                                || arr.Contains("session"))
                            {
                                string[] arrString = arr.Split('=');
                                GlobalData.Token = arrString[1];//赋值SessionID
                                break;
                            }
                        }
                    }
                }
                //读取返回的信息 
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), true))
                {
                    //获取响应格式信息
                    string result = sr.ReadToEnd();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 重写证书验证方法，总是返回TRUE，解决未能为SSL/TLS安全通道建立信任关系的问题 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            //总是返回TRUE 
            return true;
        }
    }
}
