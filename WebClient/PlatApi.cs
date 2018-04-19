using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using Games.NBall.Entity.Enums;
using Newtonsoft.Json;
using Games.NBall.WebClient.Data;
using Games.NBall.WebClient.Util;
using Games.NBall.WebClient.Plat;
using Games.NBall.Common;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;

namespace Games.NBall.WebClient
{
    public class PlatApi
    {
        static string WebGetCore(string url, int timeOut = 3000, int retryTimes = 2)
        {
            string err = string.Empty;
            do
            {
                try
                {
                    Guid id = Guid.NewGuid();
                    LogHelper.Insert(string.Format("id:{0}..call:{1}", id, url), LogType.Debug);
                    string rep = WebUtil.WebGet(url, timeOut);
                    LogHelper.Insert(string.Format("id:{0}..rep:{1}", id, rep), LogType.Debug);
                    return rep;
                }
                catch (WebException wex)
                {
                    try
                    {
                        var errRep = wex.Response as HttpWebResponse;
                        if (null != errRep)
                            err = wex.Status.ToString();
                        else
                            err = WebUtil.GetResonseText(errRep);
                        LogHelper.Insert(string.Format("call:{0}..error:{1}", url, err), LogType.Error);
                    }
                    catch (Exception wex2)
                    {
                        LogHelper.Insert(wex2);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Insert(ex, string.Format("call:{0}..", url));
                }
            }
            while (--retryTimes > 0);
            return WebErrorCode.ErrorResponse;
        }

        static string WebPostCore(string url, string data, int timeOut = 3000, int retryTimes = 2)
        {
            string err = string.Empty;
            do
            {
                try
                {
                    Guid id = Guid.NewGuid();
                    LogHelper.Insert(string.Format("id:{0}..call:{1} data:{2}", id, url, data), LogType.Debug);
                    string rep = WebUtil.WebPost(url, data, timeOut);
                    LogHelper.Insert(string.Format("id:{0}..rep:{1}", id, rep), LogType.Debug);
                    return rep;
                }
                catch (WebException wex)
                {
                    try
                    {
                        var errRep = wex.Response as HttpWebResponse;
                        if (null != errRep)
                        {
                            err = WebUtil.GetResonseText(errRep);
                            LogHelper.Insert(string.Format("call:{0}..error:{1}", url, err), LogType.Error);
                        }
                    }
                    catch (Exception wex2)
                    {
                        LogHelper.Insert(wex2);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Insert(ex, string.Format("call:{0}..", url));
                }
            }
            while (--retryTimes > 0);
            return WebErrorCode.ErrorResponse;
        }
    }
}
