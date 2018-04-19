using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.UAFacade;

namespace UA_Web
{
    public partial class ChooseSuit : System.Web.UI.Page
    {
        public string platformCode = string.Empty;
        public string accountName = string.Empty;
        public string GetHost = string.Empty;
        public string GetPlant = string.Empty;
        private static NBThreadPool _threadPool = new NBThreadPool(10);

        protected void Page_Load(object sender, EventArgs e)
        {
            accountName = GetParam("account");
            platformCode = UAFactory.Instance.FactoryName;
            GetPlant = GetplatformCode();

            var isDebug = ConfigurationManager.AppSettings["IsDebug"];

            if (string.IsNullOrEmpty(accountName))
            {
                if (isDebug == "true")
                {
                    Response.Redirect("Passport.aspx");
                }
                else
                {
                    var page = UAFactory.Instance.PlatformUrl;
                    Response.AddHeader("Access-Control-Allow-Origin", "*");
                    Response.Write("<script>alert(\"请重新登陆。\");" + UAFactory.Instance.JumpScript + "=\"" + page + "\";</script>");
                }
                Response.End();
            }
        }

        /// <summary>
        /// 获取表单参数,默认返回 string.Empty.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public string GetParam(string key)
        {
            return GetParam(key, string.Empty);
        }
        /// <summary>
        /// 获取表单参数.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public string GetParam(string key, string defaultValue)
        {
            if (!string.IsNullOrEmpty(Request[key]))
            {
                return Context.Server.UrlDecode(Request[key]);
            }

            return defaultValue;
        }

        /// <summary>
        /// js 平台编码识别
        /// </summary>
        /// <returns></returns>
        public string GetplatformCode()
        {
            string plant = UAFactory.Instance.FactoryName;
           
            return plant;
        }

        //protected string SelectZoneId()
        //{
        //    var allZoneInfo = ZoneCache.Instance.GetAllZoneByPlatform(platformCode);
        //    string allZoneString = "";
        //    foreach (var allZoneinfoEntity in allZoneInfo)
        //    {
        //        string host = ZoneCache.Instance.GetZone(allZoneinfoEntity.Idx).ApiUrl.Replace("/Index.aspx", "").Replace("/SelectZone.aspx", "");
        //        string timestamp = ShareUtil.DateTime2UnixTimeStamp(DateTime.Now).ToString();
        //        string zoneInfo = allZoneinfoEntity.Idx + "," + host + "/SelectZoneApi.aspx?account=" + accountName + "&serverId=" + allZoneinfoEntity.PlatformZoneName + "&selectZoneId=" + allZoneinfoEntity.Idx;
        //        if (allZoneString.Length > 0)
        //            allZoneString += "|";
        //        allZoneString += zoneInfo;
        //    }
        //    return allZoneString;
        //}

        ///// <summary>
        ///// 登录记录接口(此接口针对在游戏里选择区服的情况，当玩家选择区服，登录游戏时调用)
        ///// </summary>
        ///// <param name="userid"></param>
        ///// <param name="serverid"></param>
        ///// <param name="timestamp"></param>
        //private void doLoginRedirect(string userid, string serverid, string timestamp)
        //{
        //    //URL?userid={用户的账号}&gameid={游戏编号}&serverid={游戏服标识}&timestamp={LINUX时间戳,单位为秒}&sign={加密签名}
        //    //http://api.game.qidian.com/stat/qg_stat_login.php

        //    string url = AppsettingCache.Instance.GetAppSetting(EnumAppsetting.QidianLoginRedirectURL);
        //    string gameid = AppsettingCache.Instance.GetAppSetting(EnumAppsetting.QidianGameId);
        //    //验证MD5加密
        //    var platformEntity = UAFactory.Instance.Platform;
        //    if (platformEntity == null)
        //    {
        //        return;
        //    }
        //    string cryptKey = platformEntity.LoginKey;
        //    //签名(userid + gameid + serverid + timestamp + key)
        //    string sign = CryptHelper.GetMD5(userid + gameid + serverid + timestamp + cryptKey);
        //    url = string.Format("{0}?userid={1}&gameid={2}&serverid={3}&timestamp={4}&sign={5}", url, userid, gameid, serverid, timestamp, sign);
        //    GetGeneralContent(url);

        //}

        //private string GetGeneralContent(string strUrl)
        //{
        //    string strMsg = string.Empty;
        //    try
        //    {
        //        WebRequest request = WebRequest.Create(strUrl);
        //        WebResponse response = request.GetResponse();
        //        StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("gb2312"));

        //        strMsg = reader.ReadToEnd();

        //        reader.Close();
        //        reader.Dispose();
        //        response.Close();

        //        LogHelper.Info("Qidian GetGeneralContent Return:" + strMsg.ToString());

        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error("Qidian GetGeneralContent", ex);
        //        return "";

        //    }
        //    return strMsg;
        //}
    }
}