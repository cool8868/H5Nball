using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Games.NBall.Common;

namespace Games.NBall.WebServerFacade
{
    /// <summary>
    /// WebService工厂
    /// </summary>
    public class WebServiceFactory
    {
        /// <summary>
        /// 获取webservice实例
        /// </summary>
        /// <param name="url">地址</param>
        /// <returns></returns>
        public static NwWebService.NwWebService GetWebService(string zoneId)
        {
            var url = ZoneCache.Instance.GetWebServerUrl(zoneId);
            if (string.IsNullOrEmpty(url))
                throw new Exception("no webserver config ,zone id:" + zoneId);
            NwWebService.NwWebService ws = new NwWebService.NwWebService();
            ws.Url = url;
            ws.Timeout = 100000;
            LogHelper.Insert("start to web service :" + url, LogType.Info);
            return ws;
        }

        public static NwWebService.NwWebService GetWebServicePlatform(string platformCode, string platformZoneId)
        {
            var url = ZoneCache.Instance.GetWebServerUrlForPlatform(platformCode, platformZoneId);
            if (string.IsNullOrEmpty(url))
                throw new Exception("no webserver config ,platform zone id:" + platformZoneId);
            NwWebService.NwWebService ws = new NwWebService.NwWebService();
            ws.Url = url;
            ws.Timeout = 100000;
            LogHelper.Insert("start to web service :" + url, LogType.Info);
            return ws;
        }

    }
}
