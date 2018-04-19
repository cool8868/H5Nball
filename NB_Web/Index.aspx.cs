using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Share;
using Games.NBall.NB_Web.BaseCommon;
using Games.NBall.UAFacade;

namespace Games.NBall.NB_Web
{
    public partial class Index : System.Web.UI.Page
    {
        public string accountName = string.Empty;
        public string cookie = string.Empty;
        public string v = string.Empty;
        public string GetHost = string.Empty;
        public string GetPlant = string.Empty;
        public int zoneId = 1;
        public string sid = string.Empty;
        public string token = string.Empty;
        public string device_id = string.Empty;
        public string uuid = string.Empty;
        public string pf = string.Empty;
        public string pfUrl = string.Empty;

        #region 选区登录专用

        public string openid = "";
        public string state = "";
        public string nowTimestamp = "";
        public string sessionId = "";
        public string sign = "";
        public string jsNeed = "";
        public string nickName = "";
        public string skipUrl = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isDebug = ConfigurationManager.AppSettings["IsDebug"];
                string ck = GetParam("ck");
                if (ck == "default")
                {
                    SelectZone();
                }
                else
                {
                    if (!string.IsNullOrEmpty(ck))
                    {
                        try
                        {
                            //cookie是否正确
                            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(ck);
                            UserAccountEntity userAccountEntity = UserAccountMgr.Parse(ticket.UserData);
                            accountName = userAccountEntity.Account;
                            //cookie正确时存入cookielist
                            //if (UserAccountMgr.CookieList.ContainsKey(accountName))
                            //    UserAccountMgr.CookieList[accountName] = ck;
                            //else
                            //    UserAccountMgr.CookieList.Add(accountName, ck);
                        }
                        catch (Exception)
                        {
                            ck = string.Empty;
                        }
                    }
                    else
                    {
                        if (ShareUtil.PlatformCode == "1758")
                            accountName = GetParam("state");
                    }

                    if (string.IsNullOrEmpty(accountName))
                    {
                        if (isDebug == "true")
                        {
                            Response.Redirect("Passport.aspx");
                        }
                        else
                        {
                            Response.Redirect("Ul.aspx");
                        }
                        Response.End();
                    }
                    else
                    {

                        cookie = ck;
                        v = new Random().Next(1000, 10000).ToString();

                        zoneId = ShareUtil.ZoneId;
                        var zoneName = UAFactory.Instance.ZoneName;
                        var zoneCache = CacheFactory.FunctionAppCache.GetZone(zoneName);
                        if (zoneCache == null)
                        {
                            Response.Redirect("Error.aspx?Message=no zone config,zonename:" + zoneName);
                            Response.End();
                            return;
                        }
                        var platformEntity = UAFactory.Instance.GetPlatform(zoneCache.PlatformCode);
                        if (platformEntity == null)
                        {
                            Response.Redirect("Error.aspx?Message=no platform config,PlatformCode:" +
                                              zoneCache.PlatformCode);
                            Response.End();
                            return;
                        }

                        GetPlant = platformEntity.PlatformCode.ToLower();
                        GetHost = Request.Url.AbsoluteUri.Split('?')[0].Replace("/Index.aspx", ""); //前端需要的域名
                        sid = zoneCache.PlatformZoneName;
                        token = GetParam("token");
                        device_id = GetParam("device_id");
                        uuid = GetParam("uuid");
                        pf = GetParam("pf");
                        pfUrl = zoneCache.ChargeUrl.Replace("pf", pf == "" ? "h5_a8" : pf);
                        if (UAFactory.Instance.IsQunHei || UAFactory.Instance.IsTx)
                        {
                            pfUrl = zoneCache.Cdn;

                            skipUrl =
                                string.Format(
                                    "{0}?ck={1}&account={2}&plant={3}&httpRoot={4}&v={5}&sid={6}&token={7}&device_id={8}&pf={9}&isSelectZone=0",
                                    pfUrl, ck, accountName, GetPlant, GetHost, v, sid, token, device_id, pf);
                        }
                        else
                            skipUrl =
                              string.Format(
                                  "pfurl={0}&ck={1}&account={2}&plant={3}&httpRoot={4}&v={5}&sid={6}&token={7}&device_id={8}&pf={9}&isSelectZone=0",
                                  pfUrl, ck, accountName, GetPlant, GetHost, v, sid, token, device_id, pf);
                    }
                }
            }
        }

        public void SelectZone()
        {
            zoneId = ShareUtil.ZoneId;
            var zoneName = UAFactory.Instance.ZoneName;
            var zoneCache = CacheFactory.FunctionAppCache.GetZone(zoneName);
            if (zoneCache == null)
            {
                Response.Redirect("Error.aspx?Message=no zone config,zonename:" + zoneName);
                Response.End();
                return;
            }
            if (UAFactory.Instance.IsTx)
            {
                SelectZoneWb(zoneCache);
            }
            else if (UAFactory.Instance.IsEget)
                SelectZoneEget(zoneCache);
            else if (UAFactory.Instance.IsBear)
                SelectZoneBear(zoneCache);
            else if (UAFactory.Instance.IsQunHei)
                SelectZoneQunHei(zoneCache);
            else
                SelectZoneA8(zoneCache);
        }

        /// <summary>
        /// 玩吧选区
        /// </summary>
        private void SelectZoneWb(AllZoneinfoEntity zoneCache)
        {
            pfUrl = zoneCache.Cdn;
            GetHost = Request.Url.AbsoluteUri.Split('?')[0].Replace("/Index.aspx", ""); //前端需要的域名
            string pf = GetParam("pf");
            string openid = GetParam("openid");
            string openkey = GetParam("openkey");
            string pfkey = GetParam("pfkey");
            string serverId = GetParam("serverId");
            string platform = GetParam("platform");
           
            skipUrl =
                string.Format("{7}?isSelectZone=1&openid={0}&openkey={1}&pfkey={2}&serverId={3}&platform={4}&pf={5}&httpRoot={6}",
                    openid, openkey, pfkey, serverId, platform, pf, GetHost,pfUrl);
        }

        /// <summary>
        /// 群黑选区
        /// </summary>
        private void SelectZoneQunHei(AllZoneinfoEntity zoneCache)
        {
            pfUrl = zoneCache.Cdn;
            GetHost = Request.Url.AbsoluteUri.Split('?')[0].Replace("/Index.aspx", ""); //前端需要的域名
            string username = GetParamNOUrlDecode("username");
            string serverid = GetParam("serverid");
            string time = GetParam("time");
            string isadult = GetParam("isadult");//防沉迷标识(1是成年，0是未成年)
            string uimg = GetParam("uimg");//玩家头像
            string nname = GetParam("nname");//玩家昵称
            string unid = GetParam("unid");//玩家来源
            string flag = GetParam("flag");//验证签名，md5(username+serverid+isadult+time+key)
            skipUrl =
                string.Format("pfurl={9}?isSelectZone=1&username={0}&serverid={1}&time={2}&isadult={3}&uimg={4}&nname={5}&unid={6}&httpRoot={7}&flag={8}&platform=h5_qunhei&pf=h5_qunhei",
                    username, serverid, time, isadult, uimg, nname, unid, GetHost, flag,pfUrl);
        }

        /// <summary>
        /// A8选区
        /// </summary>
        /// <param name="zoneCache"></param>
        private void SelectZoneA8(AllZoneinfoEntity zoneCache)
        {
            pf = GetParam("pf");
            pfUrl = zoneCache.ChargeUrl.Replace("pf", pf == "" ? "h5_a8" : pf);//pfurl 是js文件  暂时占用chargeurl字段
            GetHost = Request.Url.AbsoluteUri.Split('?')[0].Replace("/Index.aspx", ""); //前端需要的域名

            openid = GetParamNOUrlDecode("openid");
            state = GetParam("state");
            nowTimestamp = GetParam("nowTimestamp");
            sessionId = GetParam("sessionId");
            sign = GetParam("sign");
            jsNeed = GetParam("jsNeed");
            nickName = GetParam("nickName");
            string qqOpenid = GetParam("qqopenid");
            string qqOpenkey = GetParam("qqopenkey");
            string qqPf = GetParam("qqpf");
            string platform = GetParam("platform");
            string share = GetParam("share");
            string shareType = GetParam("shareType");
            string isSubscribe = GetParam("isSubscribe");

            string isShare = GetParam("isShare");
            openid = UAHelper.StrToUtf8(openid);
            skipUrl =
                string.Format(
                    "pfurl={0}&pf={1}&isSelectZone=1&nickName={2}&sessionId={3}&jsNeed={4}&state={5}&openid={6}&nowTimestamp={7}&httpRoot={8}&sign={9}&qqopenid={10}&qqopenkey={11}&qqpf={12}&platform={13}&share={14}&shareType={15}&isSubscribe={16}&isShare={17}",
                    pfUrl, pf, nickName, sessionId, jsNeed, state, openid, nowTimestamp, GetHost, sign, qqOpenid,
                    qqOpenkey, qqPf, platform, share, shareType, isSubscribe, isShare);
        }

        /// <summary>
        /// 小熊选区
        /// </summary>
        /// <param name="zoneCache"></param>
        private void SelectZoneBear(AllZoneinfoEntity zoneCache)
        {
            pf = GetParam("pf");
            pfUrl = zoneCache.ChargeUrl.Replace("pf", pf == "" ? "h5_bear" : pf);
            GetHost = Request.Url.AbsoluteUri.Split('?')[0].Replace("/Index.aspx", ""); //前端需要的域名

            openid = GetParam("openid");
            string name = GetParam("name");
            string sex = GetParam("sex");
            string face = GetParam("face");
            string city = GetParam("city");
            string appId = GetParam("appId");

            skipUrl =
              string.Format(
                  "pfurl={0}&pf={1}&isSelectZone=1&openid={2}&name={3}&sex={4}&face={5}&city={6}&appId={7}&httpRoot={8}",
                  pfUrl, pf, openid, name, sex, face, city, appId, GetHost);

        }

        /// <summary>
        /// 白鹭选区
        /// </summary>
        private void SelectZoneEget(AllZoneinfoEntity zoneCache)
        {
            pf = GetParam("pf");
            pfUrl = zoneCache.ChargeUrl.Replace("pf", pf == "" ? "h5_egret" : pf);
            GetHost = Request.Url.AbsoluteUri.Split('?')[0].Replace("/Index.aspx", ""); //前端需要的域名
            string appId = GetParam("appId");
            string platInfo = GetParam("platInfo");
            string spid = GetParam("egret.runtime.spid");
            string channelId = GetParam("channelId");
            string isNewApi = GetParam("isNewApi");
            string egretSdkDomain = GetParam("egretSdkDomain");
            string egretServerDomain = GetParam("egretServerDomain");
            string egretRv = GetParam("egretRv");
            string token = GetParam("token");
            var egretId = GetParam("egretId");

            var userId = GetParam("userId");
            var userName = GetParam("userName");
            var userImg = GetParam("userImg");
            var userSex = GetParam("userSex");
            var egretOauthUser = GetParam("egretOauthUser");
            var egretChannelId = GetParam("egretChannelId");


            pf = GetParam("pf");
            skipUrl =
                string.Format("appId={0}&egret.runtime.spid={1}&isSelectZone=1&channelId={2}&platInfo={3}&isNewApi={4}&egretSdkDomain={5}&egretServerDomain={6}&egretRv={7}&httpRoot={8}&pf={9}&isStatistics=1&pfUrl={10}&token={11}&egretId={12}&userId={13}&userName={14}&userImg={15}&userSex={16}&egretOauthUser={17}&egretChannelId={18}",
                     appId, spid, channelId, platInfo, isNewApi, egretSdkDomain, egretServerDomain, egretRv, GetHost, pf, pfUrl, token, egretId, userId, userName, userImg, userSex, egretOauthUser, egretChannelId);
        }

        static string GetChargeUrl(string zoneChageUrl, string platformChargeUrl)
        {
            if (string.IsNullOrEmpty(zoneChageUrl))
                return platformChargeUrl;
            else
            {
                return zoneChageUrl;
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
        public string GetParamNOUrlDecode(string key)
        {
            if (!string.IsNullOrEmpty(Request[key]))
            {
                try
                {
                    var r = Request[key];
                    return r;
                }
                catch (Exception)
                {

                }

            }
            return "";
        }
    }
}