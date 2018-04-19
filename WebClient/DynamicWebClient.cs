using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Net;
using System.Web;
using Games.NBall.WebClient.Data;
using Games.NBall.WebClient.Weibo.Data;
using Games.NBall.WebClient.Util;

namespace Games.NBall.WebClient
{
    //public class DynamicWebClient : WebClientBase
    //{
    //    #region ctor
    //    public DynamicWebClient(string uri, WyxWebArgs getArgs = null, WyxWebArgs postArgs = null)
    //        : base(uri, getArgs, postArgs)
    //    {
    //    }
    //    #endregion

    //    #region Facade
    //    public dynamic WebGetJson()
    //    {
    //        return WebRequestJson(EnumWebRequestMethod.GET);
    //    }
    //    public dynamic WebPostJson()
    //    {
    //        return WebRequestJson(EnumWebRequestMethod.POST);
    //    }
    //    protected dynamic WebRequestJson(EnumWebRequestMethod method)
    //    {
    //        string text = RequestCore(method);
    //        if (JsonUtil.MaybeJson(text))
    //            return JsonConvert.DeserializeObject(text);
    //        return text;
    //    }
    //    protected virtual T WebRequestJson<T>(EnumWebRequestMethod method)
    //    {
    //        string text = RequestCore(method);
    //        if (JsonUtil.MaybeJson(text))
    //            return JsonConvert.DeserializeObject<T>(text);
    //        return default(T);
    //    }
    //    #endregion

       
    //}
}
