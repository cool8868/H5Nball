using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Item;
using Games.NBall.Core.Mall;
using Games.NBall.Entity;
using Games.NBall.NB_Web.BaseCommon;
using Games.NBall.NB_Web.Helper;
using Games.NBall.ServiceContract.Client;
using Games.NBall.UAFacade;
using Games.NBall.WebClient.Util;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Response;
using Games.NBall.NB_Web.Command;
using Games.NBall.UAFacade.UABll;
using Games.NBall.WebServerFacade;
using Games.NBall.Core.Turntable;
using Games.NBall.Core.Teammember;
using Games.NBall.Core;
using Games.NBall.Core.Transfer;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Activity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NS_OpenApiV3;
using NS_SnsNetWork;

namespace Games.NBall.NB_Web
{
    public partial class Test : System.Web.UI.Page
    {
        public string ServerIp { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            string ip = string.Empty;
            try
            {
                WbUserInfo();
            }
            catch 
            {
            }
          
        }

        public int WbUserInfo()
        {
            try
            {
                string openId = "3C12344A556DADF34634CEFB80B05D7D";
                string openKey = "2DC6A89C97E6AAFA9F74C03E15D0CEFA";
                string pf = "qzone";
                //format
                //userip
                //sig
                //UAFactory
                int appId = 1105806369;
                string appKey = "uOFRHYPj69tRtk3i";
                string serverName = "v3/user/buy_playzone_item";
                OpenApiV3 sdk = new OpenApiV3(openId, openKey, appId, pf, appKey, UAFactory.Instance.OpenUALog);
                sdk.SetUserIp(UAHelper.GetRealIP());
                RstArray result = buyPlayzoneItem(sdk);
                if (result.Ret != 0)
                {
                    return result.Ret;
                }
                var json = JsonConvert.DeserializeObject(result.Msg) as JObject;
                if (json == null)
                {
                    return 1;
                }
                var nickName = JsonUtil.GetJsonValue(json, "nickname");
                var logo = JsonUtil.GetJsonValue(json, "figureurl");
                //var extraData = BuildExtraData(openkey, pf, pfkey);
                if (pf == "wanba")
                {
                    var result1 = GetWanBaUserInfo(sdk, "1");
                    if (result1.Ret != 0)
                    {
                        return result1.Ret;
                    }
                    var json1 = JsonConvert.DeserializeObject(result1.Msg) as JObject;
                    if (json1 == null)
                    {
                        return 1;
                    }
                    var data = json1["data"];
                    if (data == null)
                        return 1;
                    var is_vip = JsonUtil.GetJsonValue(data[0], "is_vip"); //是否开通游戏达人
                    var vip_level = JsonUtil.GetJsonValue(data[0], "vip_level"); //达人等级
                    var score = JsonUtil.GetJsonValue(data[0], "score"); //用户积分
                    var expiredtime = JsonUtil.GetJsonValue(data[0], "vip_level"); //	达人过期时间
                    TxYellowvipMgr.Add(ShareUtil.GetWanBaVipAccount(openId), is_vip == "true", false, false,
                        ConvertHelper.ConvertToInt(vip_level), "");
                }
                return 0;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("腾讯获取用户", ex);
                return -1;
            }
        }

        RstArray GetUserInfo(OpenApiV3 sdk)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            string script_name = "/v3/user/get_info";
            return sdk.Api(script_name, param);
        }
        RstArray GetWanBaUserInfo(OpenApiV3 sdk, string zoneType)
        {
            //zoneType  1安卓 2ios
            Dictionary<string, string> param = new Dictionary<string, string>();
            param["zoneid"] = zoneType;
            string script_name = "/v3/user/get_playzone_userinfo";
            return sdk.Api(script_name, param);
        }

        RstArray buyPlayzoneItem(OpenApiV3 sdk)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            string script_name = "/v3/user/buy_playzone_item";
            param["zoneid"] = "1";
            param["itemid"] = "7863";
            param["count"] = "1";
            return sdk.Api(script_name, param);
        }


        private void TestcsdkInterface()
        {
            LeagueCore.Instance.RefreshExchange(new Guid("04789B65-5006-4466-A415-A62600E5BDCA"));
        }
        private string GetTxItemId(EnumAppsetting ItemCode, string platform)
        {
            var itemstr = AppsettingCache.Instance.GetAppSetting((EnumAppsetting)ItemCode);
            var itemId = "";
            var i = itemstr.Split('|');
            try
            {
                if (platform.IndexOf("and") == -1)
                {
                    itemId = i[1];
                }
                else
                {
                    itemId = i[0];
                }
                return itemId;
            }
            catch (Exception ex)
            {


            }

            return "";
        }

        private string TxBuyItem(MallBuyPoint entity)
        {

            var url = AppsettingCache.Instance.GetAppSetting(EnumAppsetting.TxWb_ChargeUrl);//txh5_a8购买url
            //用  |   连接qqOpenid, qqOpenkey, qqPf, platform,nickName5个参数放在公共参数Common里
            var strCommon = entity.StrCommon;
            var qqOpenid = "";
            var qqOpenkey = "";
            var qqPf = "";
            var platform = "";
            if (strCommon != null)
            {
                var strlist = strCommon.Split('|');
                if (strlist.Length != 4)
                    return "-1";
                qqOpenid = strlist[0];
                qqOpenkey = strlist[1];
                qqPf = strlist[2];
                platform = strlist[3];

            }
            //拿配置的腾讯itemId
            var itemstr = AppsettingCache.Instance.GetAppSetting((EnumAppsetting)entity.ItemCode);
            var itemId = "";
            var i = itemstr.Split('|');
            try
            {
                if (platform.IndexOf("and") == -1)
                {
                    itemId = i[1];
                }
                else
                {
                    itemId = i[0];
                }
            }
            catch (Exception ex)
            {


            }

            var str = "";
            double cash = (double)entity.Cash;//金额单位转为  分
            str = "qqopenid=" + qqOpenid + "&qqopenkey=" + qqOpenkey + "&qqpf=" + qqPf + "&platform=" + platform
                 + "&itemid=" + itemId + "&count=" + entity.itemCount + "&gameOrderId=" + entity.OrderId + "&price=" + cash;
            return UAHelper.HttpPost(url, str);
        }

        private int IsSuccess(string result)
        {
            if (!string.IsNullOrEmpty(result))
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                var json = serializer.Deserialize<TxJsonResult2>(result);
                if (json != null)
                {
                    var ret = json.ret;
                    var msg = json.msg;
                    var code = json.code;
                    if (string.IsNullOrEmpty(ret))
                    {
                        return (int)MessageCode.TxBuyPointFail;
                    }
                    else if (ret == "fail")
                    {
                        if (code == "1004")
                            return (int)MessageCode.NotSufficientFunds;
                        if (msg == "游戏订单已经完成")
                            return (int)MessageCode.Success;

                        return (int)MessageCode.TxBuyPointFail;
                    }
                    else if (ret == "success")
                    {
                        return (int)MessageCode.Success;
                    }
                }
            }
            return (int)MessageCode.TxBuyPointFail;
        }

        private void TestChannelFault()
        {
            //ManagerClient reader = new ManagerClient();
            //try
            //{
            //    Response.Write(string.Format("<div>x / y = {2} when x = {0} and y = {1}</div>", 2, 0, reader.Device(2, 0)));
            //}
            //catch (Exception ex)
            //{
            //    Response.Write(string.Format("<div>{0}</div>", ex.Message));
            //}
            //Response.Write(string.Format("<div>x + y = {2} when x = {0} and y = {1}</div>", 2, 0, reader.Add(2, 0)));
        }

        protected void btnFitler1_Click(object sender, EventArgs e)
        {
            lblText.Text = NB_Web.Helper.FilterHelper.Instance.ScanContent(txtText.Text);
        }

        protected void FastFilter_Click(object sender, EventArgs e)
        {
            //FilterHelper.LoadFiltedKeys();
            //lblText.Text = NB_Web.WordFilter.FastWordFilter.Instance.Replace(txtText.Text);
        }

        protected void btnTimetick_Click(object sender, EventArgs e)
        {
            try
            {
                var currentTime = UAHelper.ConvertDateTimeFromUnix(Convert.ToDouble(txtTimetick.Text));
                lblTimetick.Text = currentTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                lblTimeServer.Text =
                    UAHelper.ConvertDateTimeFromUnix(Convert.ToDouble(txtTimeServer.Text))
                        .ToString("yyyy-MM-dd HH:mm:ss.fff");
            }
            catch (Exception ex)
            {
                lblTimetick.Text = ex.Message;
            }

        }



        private void OutputPackage(ItemPackageResponse packageResponse)
        {
            if (packageResponse.Code == (int) MessageCode.Success)
            {
                var response = new ItemPackageDataResponse();
                response.Data = new ItemPackageData();
                //var packageItemsEntity = SerializationHelper.FromByte<ItemPackageItemsEntity>(packageResponse.Data.ItemString);
                //if (packageItemsEntity == null || packageItemsEntity.Items == null)
                //    response.Data.Items = new List<ItemInfoEntity>();
                //else
                //{
                //    response.Data.Items = packageItemsEntity.Items;
                //}
                response.Data.Items = packageResponse.Data.Items;
                response.Data.PackageSize = packageResponse.Data.PackageSize;
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", ShareUtil.DomainUrl);
                var json = SerializationHelper.ToJson(response);
                var bytes = Encoding.Unicode.GetBytes(json);
                byte[] data = Encoding.Unicode.GetBytes(json);
                StringBuilder result = new StringBuilder(data.Length*8);

                foreach (byte b in data)
                {
                    result.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
                }
                //var bytes = SerializationHelper.ToByte(json);
                HttpContext.Current.Response.Write(bytes.ToString());
                //OutputHelper.Output(response);
            }
            else
            {
                OutputHelper.Output(packageResponse.Code);
            }
        }

        private void TestMall()
        {

            int mallcode = 70108;
            var reader = new MallClient();
            var resp = reader.BuyPoint(new Guid("418A8F6F-7868-4A8D-A008-A62600E5BF1E"), mallcode);
            if (resp.Code == (int) MessageCode.Success)
            {
                try
                {
                    var pf = UAFactory.Instance.GetPlatform("txh5_a8");
                    bool flag = false;
                    if (pf.Idx == 1005)
                        flag = true;
                    //以下a8传参

                    var url = UAFactory.Instance.GetRedirectURL(UAFactory.Instance.FactoryName, "charge");
                    var chargeKey = pf.ChargeKey;
                    var channelAlias = resp.Data.ChannelAlias;
                    var zone = UAFactory.Instance.GetZoneByZoneName("a8s3");
                    string serverId = "";
                    string serverName = "";
                    if (zone != null)
                    {
                        serverId = zone.PlatformZoneName;
                        serverName = zone.Name;
                    }
                    var ext = resp.Data.Ext;
                    resp.Data.AccountId = resp.Data.DateEntity.Account;
                    resp.Data.ServerId = serverId;
                    resp.Data.ServerName = serverName;
                    var str = "appKey=" + chargeKey + "&gameOrderId=" + resp.Data.OrderId + "&price=" +
                              resp.Data.Cash + "&" +
                              "goodsName=" + resp.Data.ItemName + "&" +
                              "goodsId=" + resp.Data.ItemCode + "&title=" + resp.Data.ItemName + "&csdkId=" +
                              (int) A8csdkEnum.csdkId + "&" +
                              "channelAlias=" + channelAlias + "&subChannel=&serverId=" + serverId + "&" +
                              "serverName=" + serverName + "&roleId=" + resp.Data.DateEntity.Account + "&roleName=" +
                              resp.Data.DateEntity.Name + "&" +
                              "roleLevel=" + resp.Data.DateEntity.Level + "&sessionId=" + resp.Data.SessionId +
                              "&model=&release=&deviceId=" + resp.Data.IP + "&ext=" + ext +
                              "&uid=" + resp.Data.DateEntity.Account;
                    if (string.IsNullOrEmpty(str))
                    {
                        resp.Code = (int) MessageCode.BuyPointFail;
                    }
                    else
                    {
                        var result = UAHelper.HttpPost(url, str);
                        if (result.Length > 0)
                        {
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            var json = serializer.Deserialize<JsonResult>(result);
                            if (json != null)
                            {
                                var ret = json.ret;
                                if (string.IsNullOrEmpty(ret))
                                {
                                    resp.Code = (int) MessageCode.BuyPointFail;
                                }
                                else if (ret == "fail")
                                {
                                    resp.Code = (int) MessageCode.BuyPointFail;
                                }
                                else if (ret == "success")
                                {
                                    resp.PR = json.payURL;
                                    resp.Code = (int) MessageCode.Success;
                                    var resultTx = "";
                                    if (flag) //是否腾讯玩吧接口
                                    {
                                        resultTx = TxBuyItem(resp.Data);
                                        resp.Code = IsSuccess(resultTx);
                                    }

                                }
                            }
                            else
                                resp.Code = (int) MessageCode.BuyPointFail;
                        }
                        else
                            resp.Code = (int) MessageCode.BuyPointFail;
                    }
                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("下单", ex);
                    resp.Code = (int) MessageCode.BuyPointFail;
                }
            }
            OutputHelper.Output(resp);
        }

        private void TestLogin()
        {
            string openid = "h5_qqwbandB04DBBF689C75926C62D687B27C61F98";
            string state = "state";
            string serverId = "1003";
            string nowTimestamp = "1467100321175";
            string pf = "h5_qqwb";
            string sessionId = "92eec136-f542-48b4-b595409c21fae660";
            string sign = "7ad41716601ec0f245c68c6e8f300235";
            string jsNeed = "1";
            string nickName = "shuffle";
            string qqOpenid = "B04DBBF689C75926C62D687B27C61F98";
            string qqOpenkey = "BD558FD86F4CC65FC47DCC3A39AE3FC9";
            string qqPf = "wanba_ts";
            string platform = "and";
            string share = "share";
            string shareType = "shareType";

            SystemlogMgr.Info("ceshi", "ceshi:" + Request.Url.AbsoluteUri);
            var result = LoginCheck(openid, state, serverId, nowTimestamp, pf, sessionId, sign, jsNeed, nickName);
            if (result != "0")
                LoginResponse(result, pf, false);
            else
            {
                if (serverId == "default")
                {
                    HttpContext.Current.Response.Redirect(
                        string.Format(
                            "Index.aspx?pf={0}&ck=default&openid={1}&state={2}&nowTimestamp={3}&sessionId={4}&sign={5}&jsNeed={6}&nickName={7}&qqopenid={8}&qqopenkey={9}&qqpf={10}&platform={11}&share={12}&shareType={13}",
                            pf, openid, state, nowTimestamp, sessionId, sign, jsNeed, nickName, qqOpenid, qqOpenkey,
                            qqPf, platform, share, shareType));
                    HttpContext.Current.Response.End();
                }
                else
                {
                    //用  |   连接qqOpenid, qqOpenkey, qqPf, platform,nickName5个参数放在公共参数Common里
                    var strCommon = qqOpenid + "|" + qqOpenkey + "|" + qqPf + "|" + platform;
                    var str = UAErrorCode.ErrTxException;
                    var r = SetVipInfo(openid, qqOpenid, qqOpenkey, qqPf, platform);
                    if (false)
                        LoginResponse(str, pf, true);
                    str = StartGame(openid, state, serverId, nowTimestamp, pf, sessionId, sign, jsNeed, nickName,
                        strCommon);
                    
                }

            }
        }

        private string LoginCheck(string openid, string state, string serverId, string nowTimestamp, string pf,
            string sessionId, string sign, string jsNeed, string nickName)
        {
            if (string.IsNullOrEmpty(openid)
                || string.IsNullOrEmpty(state)
                || string.IsNullOrEmpty(serverId)
                || string.IsNullOrEmpty(nowTimestamp)
                || string.IsNullOrEmpty(pf)
                || string.IsNullOrEmpty(sessionId)
                || string.IsNullOrEmpty(sign))
            {
                UAHelper.WriteLog("ret", "参数为空");
                return UAErrorCode.ErrDataOP;
            }

            if (nowTimestamp.IndexOf(' ') >= 0)
            {
                var times = nowTimestamp.Split(' ');
                if (times.Length > 0)
                {
                    nowTimestamp = times[0];
                }
            }
            var channelAliasEntity = UAFactory.Instance.GetPlatform("" + A8csdkEnum.txh5_a8); //枚举参数修改腾讯key

            //md5(openid+state+serverId+nowTimestamp+pf+sessionId+nickName+md5Key）.tolowcase()
            string cryptKey = channelAliasEntity.LoginKey;
            string signParam = "";
            string signserverId = "default";
            //if (string.IsNullOrEmpty(nickName))
            signParam =
                CryptHelper.GetMD5(openid + state + signserverId + nowTimestamp + pf + sessionId + cryptKey)
                    .ToLower();
            //else
            //    signParam =
            //        CryptHelper.GetMD5(openid + state + signserverId + nowTimestamp + pf + sessionId + nickName +
            //                           cryptKey).ToLower();

            return "0";
        }

        private void LoginResponse(string str, string pf, bool isRediect)
        {
            switch (str)
            {
                case UAErrorCode.ErrDataOP:
                    UAHelper.WriteLog("ret", "参数不正确");
                    Response.Write("{\"ret\":\"fail\",\"msg\":\"参数不正确\"}");
                    break;
                case UAErrorCode.ErrOther:
                    Response.Write("{\"ret\":\"fail\",\"msg\":\"其他\"}");
                    break;
                case UAErrorCode.ErrTimeout:
                    Response.Write("{\"ret\":\"fail\",\"msg\":\"链接超时\"}");
                    break;
                case UAErrorCode.ErrPlatform:
                    Response.Write("{\"ret\":\"fail\",\"msg\":\"pf错误}");
                    break;
                case UAErrorCode.ErrCheckSign:
                    Response.Write("{\"ret\":\"fail\",\"msg\":\"sign错误\"}");
                    break;
                case UAErrorCode.ErrNoUser:
                    Response.Write("{\"ret\":\"fail\",\"msg\":\"读取错误\"}");
                    break;
                case UAErrorCode.ErrException:
                    Response.Write("{\"ret\":\"fail\",\"msg\":\"throw\"}");
                    break;
                case UAErrorCode.ErrOK:
                    Response.Write("{\"ret\":\"success\",\"msg\":\"ok\"}");
                    break;
            }
            if (isRediect)
                HttpContext.Current.Response.Redirect("Index.aspx?pf=" + pf + "&ck=" +
                                                      Response.Cookies[FormsAuthentication.FormsCookieName].Value);
            Response.End();
        }

        private bool SetVipInfo(string openid, string qqOpenid, string qqOpenkey, string qqPf, string platform)
        {
            var url = AppsettingCache.Instance.GetAppSetting(EnumAppsetting.TxWb_ChargeUrl);//txh5_a8查询达人接口 
            var str = "";
            str = "qqopenid=" + qqOpenid + "qqopenkey=" + qqOpenkey + "qqpf=" + qqPf + "platform=" + platform;
            var result = UAHelper.HttpPost(url, str);
            //{'ret':'success', 'code':'0', 'message':'', 'data': {"is_vip":"1", "vip_level": "8", "score": "1000", "expiredtime": "1407312182"}}
            if (!string.IsNullOrEmpty(result))
            {
               
                JavaScriptSerializer serializer = new JavaScriptSerializer();
             
                  
                    var json = serializer.Deserialize<TxVipJsonResult2>(result);
               
              
                if (json != null)
                {
                    try
                    {

                    
                    var ret = json.ret;
                    if (string.IsNullOrEmpty(ret) || ret == "fail")
                    {
                        return false;
                    }
                    else if (ret == "success")
                    {
                        bool flag = json.is_vip == "1";
                        int vipLevel = Convert.ToInt32(json.vip_level);
                        var str1 = json.score + "|" + json.expiredtime;
                        return TxYellowvipMgr.Add(openid, flag, false, false, vipLevel, str1, null,
                            "" + ShareUtil.ZoneId);
                      
                    }
                    }
                    catch (Exception ex)
                    {

                        return true;
                    }
                }
            }
            return false;
        }

        private string StartGame(string openid, string state, string serverId, string nowTimestamp, string pf,
            string sessionId, string sign, string jsNeed, string nickName, string strCommon = "")
        {
            try
            {
                /* download-wyfth5.3333.cn/wyft1758/index.html?openid=pf+gid&state=xxx&serverId=8001&nowTimestamp=46546461321&pf=1758&sessionId&sign=xxx&jsNeed=0&nickName=xxx*/

                if (WebServerHandler.SetStartGameEntity(ShareUtil.PlatformCode, ShareUtil.PlatformZoneName, openid,
                    state,
                    serverId, pf, sessionId, jsNeed, nickName, strCommon)) ;
                {
                    return UAErrorCode.ErrOther;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("txa8 Charge", ex);
                Response.Write("{\"ret\":\"fail\",\"msg\":\"系统异常" + ex + "\"}");
                return UAErrorCode.ErrException;
            }
            return UAErrorCode.ErrOK;
        }

    }

    public class TxVipJsonResult2
    {
        public string ret { get; set; }
        public string code { get; set; }
        public string meg { get; set; }
        public string data { get; set; }

        public string is_vip { get; set; }
        public string vip_level { get; set; }
        public string score { get; set; }
        public string expiredtime { get; set; }

    }
    public class TxVipJsonResult3
    {
        public string ret { get; set; }
        public string code { get; set; }
        public string msg { get; set; }
        public Data data { get;set ;}
        public class Data
        {

            public string is_vip { get; set; }
            public string vip_level { get; set; }
            public string score { get; set; }
            public string expiredtime { get; set; }
        }
    }
}
 