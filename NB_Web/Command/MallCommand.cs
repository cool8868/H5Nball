using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Share;
using Games.NBall.NB_Web.BaseCommon;
using Games.NBall.NB_Web.Helper;
using Games.NBall.ServiceContract.Client;
using Games.NBall.UAFacade;
using Games.NBall.UAFacade.UABll;
using Games.NBall.WebClient.Util;
using Games.NBall.WebClient.Weibo;
using Games.NBall.WebServerFacade;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NS_OpenApiV3;
using NS_SnsNetWork;

namespace Games.NBall.NB_Web.Command
{
    public class MallCommand : BaseCommand
    {
        public void Dispatch(string action)
        {
            //校验，如有接口不需校验，需加在下面
            if (false)
            {
                if (ValidatorAccountOnly() == false)
                    return;
            }
            else
            {
                if (Validator() == false)
                    return;
            }
            switch (action)
            {
                case "bufflist":
                    BuffList();
                    return;
                case "checkextraitem":
                    CheckExtraItem();
                    break;
                case "buyextraitem":
                    BuyExtraItem();
                    break;
                case "getshowlist":
                    GetShowList();
                    break;
                case "buyitem":
                    BuyItem();
                    break;
                case "buypointtx":
                    BuyPointTx();
                    break;
                case "buyparatx":
                    BuyPointParaTx();
                    break;
                case "buypoint":
                    BuyPoint();
                    break;
                case "ui":
                    var itemId = GetParamGuid("i");
                    var usedCount = GetParamInt("u");
                    if (usedCount <= 0)
                        usedCount = 1;

                    var ui = reader.UseItem(UserAccount.ManagerId, itemId, usedCount);
                    OutputHelper.Output(ui);
                    break;
                case "gbp":
                    var response = reader.GetBuyPointInfo(UserAccount.ManagerId);
                    OutputHelper.Output(response);
                    break;
                default:
                    OutputHelper.OutputBadRequest();
                    break;
            }
        }

        private readonly MallClient reader = new MallClient();
        void BuffList()
        {
            var response = reader.GetManagerBuffs(UserAccount.ManagerId);
            OutputHelper.Output(response);
        }

        void CheckExtraItem()
        {
            int extraType = GetParamInt("extratype");
            var response = reader.CheckExtraItem(UserAccount.ManagerId, extraType);
            OutputHelper.Output(response);
        }

        void BuyExtraItem()
        {
            int extraType = GetParamInt("extratype");
            var response = reader.BuyExtraItem(UserAccount.ManagerId, extraType);
            OutputHelper.Output(response);
        }

        /// <summary>
        /// 获取显示列表
        /// </summary>
        /// <returns></returns>
        void GetShowList()
        {
            var response = reader.GetShowList(UserAccount.ManagerId);
            OutputHelper.Output(response);
        }

        void BuyItem()
        {
            int mallCode = GetParamInt("mallCode");
            int count = GetParamInt("count");
            var response = reader.BuyItem(UserAccount.ManagerId, mallCode, count);
            OutputHelper.Output(response);
        }

        private void BuyPoint()
        {
            var teststr = "11111";
            int mallcode = GetParamInt("mallcode");
            var resp = reader.BuyPoint(UserAccount.ManagerId, mallcode);
            if (resp.Code == (int) MessageCode.Success&&(ShareUtil.IsH5A8||ShareUtil.IsTx))
            {
                try
                {
                    var pf = UAFactory.Instance.GetPlatform(UAFactory.Instance.FactoryName);
                    var zone = UAFactory.Instance.GetZoneByZoneName(UAFactory.Instance.ZoneName);

                    //以下a8传参
                    var itemId = "";
                    var url = UAFactory.Instance.GetRedirectURL(UAFactory.Instance.FactoryName, "charge");
                    var chargeKey = pf.ChargeKey;
                    var channelAlias = resp.Data.ChannelAlias;
                    string serverId = "";
                    string serverName = "";
                    var qqOpenid = "";
                    var qqOpenkey = "";
                    var qqPf = "";
                    var platform = "";
                    bool isvip = false;
                    if (zone != null)
                    {
                        serverId = zone.PlatformZoneName;
                        serverName = zone.Name;
                    }
                    var ext = resp.Data.Ext;
                    var openId3 = UserAccount.Account;
                    var oId = UAHelper.StrToUtf8(openId3);
                    resp.Data.AccountId = oId;
                    resp.Data.ServerId = serverId;
                    resp.Data.ServerName = serverName;
                    resp.Data.IsJump = false;
                    var score = 0;
                    var zoneidd = "";
                    var appId = AppsettingCache.Instance.GetAppSetting(EnumAppsetting.TxWb_AppId);
                    if (ShareUtil.IsTx)
                    {
                        var strCommon = resp.Data.StrCommon;
                      
                        if (strCommon != null)
                        {
                            var strlist = strCommon.Split('|');
                           
                            qqOpenid = strlist[0];
                            qqOpenkey = strlist[1];
                            qqPf = strlist[2];
                            platform = strlist[3];

                        }
                        itemId = GetTxItemId(resp.Data.ItemCode, platform,ref zoneidd);

                    }
                    else
                    {
                        itemId = "" + resp.Data.ItemCode;
                    }

                    var str = "appKey=" + chargeKey + "&gameOrderId=" + resp.Data.OrderId + "&price=" +
                              resp.Data.Cash + "&" +
                              "goodsName=" + resp.Data.ItemName + "&" +
                              "goodsId=" + itemId + "&title=" + resp.Data.ItemName + "&csdkId=" +
                              (int) A8csdkEnum.csdkId + "&" +
                              "channelAlias=" + channelAlias + "&subChannel=&serverId=" + serverId + "&" +
                              "serverName=" + serverName + "&roleId=" + UserAccount.ManagerId + "&roleName=" +
                              resp.Data.DateEntity.Name + "&" +
                              "roleLevel=" + resp.Data.DateEntity.Level + "&sessionId=" + resp.Data.SessionId +
                              "&model=&release=&deviceId=" + resp.Data.IP + "&ext=" + ext +
                              "&uid=" + oId;
                   
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
                                    if (ShareUtil.IsTx)//是否腾讯玩吧接口
                                    {
                                        resultTx = TxBuyItem(resp.Data, qqOpenid, qqOpenkey, qqPf, platform, itemId);
                                        resp.Code = IsSuccess(resultTx);
                                        SetVipInfo(UserAccount.Account, qqOpenid, qqOpenkey, qqPf, platform, ref score,ref isvip);
                                    }
                                    if (resp.Code == (int)MessageCode.NotSufficientFunds)
                                    {
                                        var cash = resp.Data.Cash;
                                        resp.Data.IsJump = true;
                                        resp.Data.Is_vip = isvip;
                                        resp.Data.OpenId = qqOpenid;
                                        resp.Data.OpenKey = qqOpenkey;
                                        resp.Data.Score = score;
                                        if (isvip)
                                        {
                                            resp.Data.DefaultScore = "" + (double) cash*0.8/10; //价格单位  由分  改成1：10的 星星(达人传8折）
                                        }
                                        else
                                        {
                                            resp.Data.DefaultScore = "" + (double)cash / 10;//价格单位  由分  改成1：10的 星星
                                        }
                                        resp.Data.AppId = appId;
                                        resp.Data.ZoneId = zoneidd;
                                        resp.Code = (int) MessageCode.Success;
                                    }
                                }
                            }
                            else
                                resp.Code = (int) MessageCode.BuyPointFail;
                        }
                        else
                            resp.Code = (int) MessageCode.BuyPointFail;
                    
                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("下单", ex);
                    resp.Code = (int)MessageCode.BuyPointFail;
                }
            }
            //小熊接口没有serverId字段
            if (ShareUtil.IsBear)
            {
                var s=resp.Data.ServerId;
                var o = resp.Data.OrderId;
                resp.Data.Ext = o + "|" + s;
            }
            if (ShareUtil.IsQunHei)
            {
                var pf = UAFactory.Instance.GetPlatform(UAFactory.Instance.FactoryName);
                resp.PR = pf.ChargeUrl;
            }
            OutputHelper.Output(resp);
        }

        private void BuyPointParaTx()
        {
            int mallcode = GetParamInt("mallcode");
            var response = reader.TxBuyPointPara(UserAccount.ManagerId, mallcode);
            OutputHelper.Output(response);
        }

        private void BuyPointTx()
        {
            int mallcode = GetParamInt("mallcode");
            var responsepara = reader.TxBuyPointPara(UserAccount.ManagerId, mallcode);
            var response = new MallTxBuyPointResultResponse();
            if (responsepara.Code != (int) MessageCode.Success)
            {
                response.Code = responsepara.Code;
                OutputHelper.Output(response);
                return;
            }
            string orderId =ShareUtil.GenerateComb()+ "";
            int cost = 1;
            var code = BuyPlayzoneItem(responsepara.Data.OpenId, responsepara.Data.Openkey, responsepara.Data.Pf,
                UAFactory.Instance.TxAppId, UAFactory.Instance.TxAppKey, responsepara.Data.ItemId,
                responsepara.Data.ZoneId, ref orderId, ref cost);
            if (code != (int)MessageCode.Success)
            {
                response.Code = code;
                OutputHelper.Output(response);
                return;
            }
            response = reader.TxBuyPointShipments(UserAccount.ManagerId, orderId, cost, responsepara.Data.MallCode);
            if (response.Code != (int)MessageCode.Success)
                response.Code = (int)MessageCode.PaySuccessAndShipmentsBeDefeated;
            OutputHelper.Output(response);
        }
        private void SetVipInfo(string openid, string qqOpenid, string qqOpenkey, string qqPf, string platform, ref int score, ref bool flag)
        {
            var url = AppsettingCache.Instance.GetAppSetting(EnumAppsetting.TxWb_FindVipUrl); //txh5_a8查询达人接口 
            var str = "";
            str = "qqopenid=" + qqOpenid + "&qqopenkey=" + qqOpenkey + "&qqpf=" + qqPf + "&platform=" + platform;
            var result = UAHelper.HttpPost(url, str);
            //{'ret':'success', 'code':'0', 'message':'', 'data': {"is_vip":"1", "vip_level": "8", "score": "1000", "expiredtime": "1407312182"}}
            if (!string.IsNullOrEmpty(result))
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                try
                {
                    var json = serializer.Deserialize<TxVipJsonResult>(result);


                    if (json != null)
                    {
                        var ret = json.ret;
                        if (string.IsNullOrEmpty(ret) || ret == "fail")
                        {
                            return ;
                        }
                        else if (ret == "success")
                        {

                            var json2 = serializer.Deserialize<InnerData>(json.data);

                            flag = json2.is_vip.ToLower() == "true";
                            int vipLevel = (int)ConvertHelper.ConvertToDouble(json2.vip_level);
                            score = ConvertHelper.ConvertToInt(json2.score);
                         
                            var str1 = json2.score + "|" + json2.expiredtime;
                            TxYellowvipMgr.Add(openid, flag, false, false, vipLevel, str1, null,
                                "" + ShareUtil.ZoneName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("{\"ret\":\"fail\",\"msg\":\"" + ex.Message + "}");
                }
            }

        }
        private string TxBuyItem(MallBuyPoint entity, string qqOpenid, string qqOpenkey, string qqPf, string platform,string itemId)
        {

            var url = AppsettingCache.Instance.GetAppSetting(EnumAppsetting.TxWb_ChargeUrl);//txh5_a8购买url
                    //用  |   连接qqOpenid, qqOpenkey, qqPf, platform,nickName5个参数放在公共参数Common里
           
            var str = "";
            double cash = (double) entity.Cash;//金额单位转为  分
            str = "qqopenid=" + qqOpenid + "&qqopenkey=" + qqOpenkey + "&qqpf=" + qqPf + "&platform=" + platform
                 + "&itemid=" + itemId + "&count=" + entity.itemCount + "&gameOrderId=" + entity.OrderId + "&price=" + cash;
           return UAHelper.HttpPost(url, str);
        }

        private string GetTxItemId(int ItemCode,string platform,ref string zoneidd)
        {
            var itemId = "";
            try
            {
                if (platform.IndexOf("and") == -1)
                {
                    itemId =""+ AppsettingCache.Instance.GetIOSItem(ItemCode);
                    zoneidd = "2";
                }
                else
                {
                    itemId =""+ AppsettingCache.Instance.GetAndItem(ItemCode);
                    zoneidd = "1";
                }
                return itemId;
            }
            catch (Exception ex)
            {


            }

            return "";
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
                        if(code=="1004")
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
            return (int) MessageCode.TxBuyPointFail;
        }
        public int BuyPlayzoneItem(string openId, string openKey, string pf, int appId, string appKey, string itemId, string zoneType, ref string orderId, ref int cost)
        {
            try
            {
                OpenApiV3 sdk = new OpenApiV3(openId, openKey, appId, pf, appKey, UAFactory.Instance.OpenUALog);
                sdk.SetUserIp(UAHelper.GetRealIP());
                RstArray result = buyPlayzoneItem(sdk,itemId,zoneType);
                if (result.Ret == 0)
                {
                    var json = JsonConvert.DeserializeObject(result.Msg) as JObject;
                    if (json == null)
                    {
                        return 1;
                    }
                    var code =  ConvertHelper.ConvertToInt(JsonUtil.GetJsonValue(json, "code")); //订单号 
                    if (code != 0)
                    {
                        switch (code)
                        {
                            case 1002: //用户没有登录态
                                return (int) MessageCode.UserNotLogin;
                                break;
                            case 1003: //账户被冻结
                                return (int) MessageCode.UserFreeze;
                                break;
                            case 1004: //账户余额不足
                                return (int) MessageCode.TxNotSufficientFunds;
                                break;
                            default:
                                return (int) MessageCode.TxBuyPointFail;
                                break;
                        }
                    }
                    var data = json["data"];
                    if (data == null)
                        return 1;
                     orderId = JsonUtil.GetJsonValue(data[0], "billno").ToLower(); //订单号
                     cost =  ConvertHelper.ConvertToInt(JsonUtil.GetJsonValue(data[0], "cost")); //消耗积分
                }

            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("腾讯充", ex);
                return -1;
            }
            return 0;
        }
        RstArray buyPlayzoneItem(OpenApiV3 sdk,string itemId,string zoneType)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            string script_name = "/v3/user/buy_playzone_item";
            param["zoneid"] = zoneType;
            param["itemid"] = itemId;
            param["count"] = "1";
            return sdk.Api(script_name, param);
        }
    }
    public class TxJsonResult2
    {
        public string ret { get; set; }
        public string msg { get; set; }
        public string code { get; set; }
    }
}