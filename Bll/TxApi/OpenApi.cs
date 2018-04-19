using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
//泛型
using System.Collections.Generic;
//正则
using System.Text.RegularExpressions;
//http请求类
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity.Enums;
using NS_SnsNetWork;
//签名生成类
using NS_SnsSigCheck;
//上报类
using NS_SnsStat;
//json类
using System.Web.Script.Serialization;
//xml
using System.Xml;

namespace NS_OpenApiV3
{
	/**
	 * .NET SDK for  OpenAPI V3
	 *
	 * @version 3.0.0
	 * @author open.qq.com
	 * @copyright © 2012, Tencent Corporation. All rights reserved.
	 * @ History:
	 *               3.0.1 | coolinchen | 2013-02-28 12:01:05 | modify response code
	 *               3.0.0 | coolinchen | 2013-01-11 11:11:11 | initialization
	 */
	 
    public class OpenApiV3
    {
         /*错误码定义*/
        const int OPENAPI_ERROR_REQUIRED_PARAMETER_EMPTY = 1801;// 参数为空
        const int OPENAPI_ERROR_REQUIRED_PARAMETER_INVALID = 1802;// 参数格式错误
        const int OPENAPI_ERROR_RESPONSE_DATA_INVALID = 1803;// 返回包格式错误
        const int OPENAPI_ERROR_HTPP = 1900;// http请求异常, 偏移量1900

	    private string stat_url = "apistat.tencentyun.com";
	    private bool is_stat = true;
        private bool OpenUalog = false;



        private string openId = "";
        private string openKey = "";
        private int appid = 0;
        private string pf = "";
        private string format = "json";
        private string userip = "";
        private string appkey = "";
        private string server_name = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.Tx_server_name);

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="appid">应用的ID </param>
        /// <param name="appkey">应用的密钥</param>
        public OpenApiV3(string openId, string openKey, int appid, string pf, string appkey, bool openUalog)
        {
            this.openId = openId;
            this.appid = appid;
            this.appkey = appkey;
            this.openKey = openKey;
            this.pf = pf;
            OpenUalog = openUalog;
        }
        public void SetFormat(string format)
        {
            this.format = format;
        }
         public void SetStatUrl(string stat_url)
        {
            this.stat_url = stat_url;
        }
         public void SetIsStat(bool is_stat)
        {
            this.is_stat = is_stat;
        }

        public void SetUserIp(string userIp)
        {
            this.userip = userIp;
        }

        /// <summary>
         /// 执行API调用，返回结果数组
         /// </summary>
         /// <param name="script_name">调用的API方法，比如/v3/user/get_info， 参考 http://wiki.open.qq.com/wiki/API_V3.0%E6%96%87%E6%A1%A3 </param>
         /// <param name="param">调用API时带的参数</param>
         /// <param name="method">请求方法 post / get</param>
         /// <param name="protocol">协议类型 http / https </param>
         /// <returns>返回结果数组</returns>
         public RstArray Api(string script_name, Dictionary<string, string> param, string method, string protocol)
        {
            RstArray result_array = new RstArray();
            // 添加一些参数
            param["openid"] = openId;
            param["openkey"] = openKey;
            param["pf"] = pf;
            param["userip"] = userip;
            param["appid"] = appid.ToString();
            param["format"] = format;
            // 检查 openid 是否为空
             if (string.IsNullOrEmpty(param["openid"]))
            {
                result_array.Msg = "openid is empty";
                result_array.Ret = OPENAPI_ERROR_REQUIRED_PARAMETER_EMPTY;
                return result_array;
            }
            // 检查 openid 是否合法
            if (!IsOpenId(param["openid"]))
            {
                result_array.Msg = "openid is invalid";
                result_array.Ret = OPENAPI_ERROR_REQUIRED_PARAMETER_INVALID;
                return result_array;
            }
            // 无需传sig, 会自动生成
            if (param.ContainsKey("sig"))
            {
                param.Remove("sig");
            }

           
			
            // 生成签名
            string secret = appkey + "&";
            string sig =  SnsSigCheck.MakeSig(method, script_name, param, secret);
            param.Add("sig", sig);

            string url = protocol + "://" + server_name + script_name;

            // 增加cookie
            Dictionary<string, string> cookie = new Dictionary<string, string>();

            //记录接口调用开始时间
            long start_time = SnsStat.GetTime();

            //通过调用以下方法，可以打印出最终发送到openapi服务器的请求参数以及url，不打印可以注释
		    PrintRequest(url,param,method);
			
		    // 发起请求
            result_array = SnsNetWork.MakeRequest(url, param, cookie, method, protocol);
            if (result_array.Ret != 0)
		    {
                result_array.Ret += OPENAPI_ERROR_HTPP;
                PrintRespond(result_array,2);
                return result_array;
		    }

            
            //解析返回结果的返回码
            string stat_ret = "";
            try
            {
                if (this.format == "xml")
                {
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(result_array.Msg);
                    stat_ret = xml.LastChild["ret"].InnerText.ToString();
                }
                else 
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    RstArray json_obj = new RstArray();
                    json_obj = serializer.Deserialize<RstArray>(result_array.Msg);
                    stat_ret = json_obj.Ret.ToString();
                }
            }
            catch (Exception e)
            {
                result_array.Msg = e.Message;
                // 远程返回的不是 json或者xml 格式, 说明返回包有问题
                result_array.Ret += OPENAPI_ERROR_RESPONSE_DATA_INVALID;
                PrintRespond(result_array,3);
                return result_array;
            }
		    // 统计上报
		    if (is_stat)
		    {
                Dictionary<string, string> stat_params = new Dictionary<string, string>();
                stat_params["appid"] = appid.ToString();
                stat_params["pf"] = param["pf"];
                stat_params["rc"] = stat_ret;
                stat_params["svr_name"] = server_name;
                stat_params["interface"] = script_name;
                stat_params["protocol"] = protocol;
                stat_params["method"] = method;
			    SnsStat.StatReport(stat_url, start_time, stat_params);
		    }

        //通过调用以下方法，可以打印出调用openapi请求的返回码以及错误信息,不打印可以注释
		PrintRespond(result_array,1);

        return result_array;
        }




         /// <summary>
         /// 执行API调用，返回结果数组，重载函数
         /// </summary>
         /// <param name="script_name">调用的API方法，比如/v3/user/get_info， 参考 http://wiki.open.qq.com/wiki/API_V3.0%E6%96%87%E6%A1%A3 </param>
         /// <param name="param">调用API时带的参数</param>
         /// <returns>返回结果数组</returns>
         public RstArray Api(string script_name, Dictionary<string, string> param)
        {
            return Api(script_name, param, "get", "http");
         }

         public RstArray Apihttps(string script_name, Dictionary<string, string> param)
         {
             if (server_name == "119.147.19.43")
                 return Api(script_name, param, "get", "http");
             else
             {
                 return Api(script_name, param, "post", "https");
             }
         }

         /// <summary>
         /// 执行上传文件API调用，返回结果数组
         /// </summary>
         /// <param name="script_name">调用的API方法，比如/v3/t/add_pic_t， 参考 http://wiki.open.qq.com/wiki/API_V3.0%E6%96%87%E6%A1%A3 </param>
         /// <param name="param">调用API时带的参数</param>
         /// <param name="filename">文件名，文件相应参数的参数名，例如/v3/t/add_pic_t中的 "pic" </param>
         /// <param name="filepath">文件的路径 </param>
         /// <param name="protocol">协议类型 http / https</param>
         /// <returns>返回结果数组</returns>
         public RstArray ApiWithFile(string script_name, Dictionary<string, string> param, string filename,string filepath ,string protocol)
         {
             RstArray result_array = new RstArray();
             // 检查 openid 是否为空
             if (string.IsNullOrEmpty(param["openid"]))
             {
                 result_array.Msg = "openid is empty";
                 result_array.Ret = OPENAPI_ERROR_REQUIRED_PARAMETER_EMPTY;
                 return result_array;
             }
             // 检查 openid 是否合法
             if (!IsOpenId(param["openid"]))
             {
                 result_array.Msg = "openid is invalid";
                 result_array.Ret = OPENAPI_ERROR_REQUIRED_PARAMETER_INVALID;
                 return result_array;
             }
             // 无需传sig, 会自动生成
             if (param.ContainsKey("sig"))
             {
                 param.Remove("sig");
             }

             // 添加一些参数
             param.Add("appid", appid.ToString());
             param.Add("format", format);
			 
             // 生成签名
             string secret = appkey + "&";
             string sig = SnsSigCheck.MakeSig("POST", script_name, param, secret);
             param.Add("sig", sig);
			 
             string url = protocol + "://" + server_name + script_name;
             Dictionary<string, string> cookie = new Dictionary<string, string>();
             //记录接口调用开始时间
             long start_time = SnsStat.GetTime();

             //通过调用以下方法，可以打印出最终发送到openapi服务器的请求参数以及url，不打印可以注释
             PrintRequest(url, param, "post");

             // 发起请求
             result_array = SnsNetWork.MutilpartPostFile(url, param, cookie, protocol, filename, filepath);

             if (result_array.Ret != 0)
             {
                 result_array.Ret += OPENAPI_ERROR_HTPP;
                 return result_array;
             }

             //解析返回结果的返回码，仅用于统计上报
             string stat_ret = "";
             try
             {
                 if (this.format == "xml")
                 {
                     XmlDocument xml = new XmlDocument();
                     xml.LoadXml(result_array.Msg);
                     stat_ret = xml.LastChild["ret"].InnerText.ToString();
                 }
                 else
                 {
                     JavaScriptSerializer serializer = new JavaScriptSerializer();
                     RstArray json_obj = new RstArray();
                     json_obj = serializer.Deserialize<RstArray>(result_array.Msg);
                     stat_ret = json_obj.Ret.ToString();
                 }
             }
             catch (Exception e)
             {
                 result_array.Msg = e.Message;
                 // 远程返回的不是 json或者xml 格式, 说明返回包有问题
                 result_array.Ret += OPENAPI_ERROR_RESPONSE_DATA_INVALID;
                 return result_array;
             }
             // 统计上报
             if (is_stat)
             {
                 Dictionary<string, string> stat_params = new Dictionary<string, string>();
                 stat_params["appid"] = appid.ToString();
                 stat_params["pf"] = param["pf"];
                 stat_params["rc"] = stat_ret;
                 stat_params["svr_name"] = server_name;
                 stat_params["interface"] = script_name;
                 stat_params["protocol"] = protocol;
                 stat_params["method"] = "post";
                 SnsStat.StatReport(stat_url, start_time, stat_params);
             }

             //通过调用以下方法，可以打印出调用openapi请求的返回码以及错误信息,不打印可以注释
             PrintRespond(result_array,4);

             return result_array;
         }




         /// <summary>
         /// 执行上传文件API调用，返回结果数组，重载函数
         /// </summary>
         /// <param name="script_name">调用的API方法，比如/v3/t/add_pic_t， 参考 http://wiki.open.qq.com/wiki/API_V3.0%E6%96%87%E6%A1%A3 </param>
         /// <param name="param">调用API时带的参数</param>
         /// <param name="filename">文件名，文件相应参数的参数名，例如/v3/t/add_pic_t中的 "pic" </param>
         /// <param name="filepath">文件的路径 </param>
         /// <returns>返回结果数组</returns>
         public RstArray ApiWithFile(string script_name, Dictionary<string, string> param,  string filename, string filepath)
         {
             return ApiWithFile(script_name,param,filename,filepath,"http");
         }

         /// <summary>
         ///  打印出请求串的内容，当API中的这个函数的注释放开将会被调用。
         /// </summary>
         /// <param name="url">请求串内容</param>
         /// <param name="param">请求串的参数表单</param>
         /// <param name="method">请求的方法 get/post</param>
         private void PrintRequest(string url, Dictionary<string, string> param, string method)
         {
             if (!OpenUalog)
                 return;
             if(HttpContext.Current==null)
                 return;
             SystemlogMgr.Info("PrintRequest0", url + "?" + SnsNetWork.MakeQueryStringNoEncode(param));
             string query_string = SnsNetWork.MakeQueryString(param);
            if(method.Equals("get", StringComparison.OrdinalIgnoreCase))
		    {
			    url = url + "?" + query_string;
		    }

             string sb = "";
            //sb+="<br><br>============= request info ================<br>";
            sb+="method :" + method + "<br>";
            sb+="url    :" + url + "<br>";
		    if(method.Equals("post", StringComparison.OrdinalIgnoreCase))
		    {
                sb+="query_string : " + query_string + "<br>";
		    }
            sb+="<br>params:<br>";
            foreach(string key in param.Keys)
            {
                sb+=key + " = " + param[key] + "<br>";
            }
            sb+="<br><br>";
            SystemlogMgr.Info("PrintRequest", sb);
         }

         /// <summary>
         ///  打印出返回结果的内容，当API中的这个函数的注释放开将会被调用。
         /// </summary>
         /// <param name="RstArray">待打印的array</param>
         private void PrintRespond(RstArray result_array,int index)
        {
             if(!OpenUalog)
                 return;
            if (HttpContext.Current == null)
                return;

             string sb = index+",";//"<br>============= respond info ================<br>";
            sb+="ret = " + result_array.Ret + "<br>msg = " + result_array.Msg+"<br>";
            SystemlogMgr.Info("PrintRespond", sb);
        }

        /// <summary>
        ///  检查 openid 的格式
        /// </summary>
        /// <param name="openid">openid</param>
        /// <returns>返回验证结果</returns>
         static private bool IsOpenId(string openid)
        {
            return (0 < Regex.Match(openid, "/^[0-9a-fA-F]{32}$/", RegexOptions.IgnoreCase).Groups.Count);
        }

    }
}