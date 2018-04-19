using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Manager;
using Games.NBall.Entity.Share;
using Games.NBall.UAFacade.UABll;
using Games.NBall.WebClient.Util;
using Games.NBall.WebServerFacade;

namespace Games.NBall.UAFacade
{
    public class UA_A8 : UAAdapter
    {
        private readonly string platFormKey = "h5_a8";

        /// <summary>
        /// a8充值（1）
        /// </summary>
        /// <returns></returns>
        private string Charge()
        {
            try
            {

                /*${payNotifyURL}?sign=96d740ea416b8bf118ead1c49c9a1eda&goodsId=105&price=29&game
                OrderId=8a71d1a4-fc36-4487-9577-0b3a3b848b65&payResult=0&playerId=h5_a89e3df51e43f8
                4002b843ecd16fd59617&channelAlias=h5_a8&state=&serverId=default&payTime=1449475293
                422&orderId=1751615
                返回值:{"ret":"success","msg":"ok"}*/

                string sign = GetParam("sign");
                string orderId = GetParam("orderId");
                string gameOrderId = GetParam("gameOrderId");
                string price = GetParam("price");
                string channelAlias = GetParam("channelAlias");
                string playerId = GetParam("playerId");
                string serverId = GetParam("serverId");
                string goodsId = GetParam("goodsId");
                string payResult = GetParam("payResult");
                string state = GetParam("state");
                string payTime = GetParam("payTime");

                if (string.IsNullOrEmpty(sign)
                    || string.IsNullOrEmpty(orderId)
                    || string.IsNullOrEmpty(gameOrderId)
                    || string.IsNullOrEmpty(price)
                    || string.IsNullOrEmpty(channelAlias)
                    || string.IsNullOrEmpty(playerId)
                    || string.IsNullOrEmpty(serverId)
                    || string.IsNullOrEmpty(goodsId)
                    || string.IsNullOrEmpty(payResult)
                    || string.IsNullOrEmpty(sign)
                    || string.IsNullOrEmpty(payTime)
                    )
                {
                    return "{\"ret\":\"fail\",\"msg\":\"参数为空\"}";
                }
                if (int.Parse(payResult) == 0)
                {
                    return "{\"ret\":\"fail\",\"msg\":\"没有付款成功\"}";
                }
                //Sign = Md5(orderId + gameOrderId + price + channelAlias + playerId+ serverId + goodsId + payResult + state + payTime + PayKey).toLowerCase();
                long time1 = ConvertHelper.ConvertToLong(payTime, 0);
                DateTime sourceTime = ShareUtil.GetTime(time1);
                DateTime nowTime = DateTime.Now;
                //检查时间是否过期
                if (sourceTime.AddSeconds(UAFactory.Instance.Timeout30min) < nowTime ||
                    sourceTime.AddSeconds(-UAFactory.Instance.Timeout30min) > nowTime)
                {
                    //记录详细的错误日志
                    return "{\"ret\":\"fail\",\"msg\":\"响应超时\"}";
                }

                var channelAliasEntity = UAFactory.Instance.GetPlatform(platFormKey);
                if (channelAliasEntity == null)
                {
                    return "{\"ret\":\"fail\",\"msg\":\"channelAlias错误\"}";
                }

                string cryptKey = channelAliasEntity.ChargeKey;
                string signParam =
                    CryptHelper.GetMD5(orderId + gameOrderId + price + channelAlias + playerId + serverId + goodsId +
                                       payResult + state + payTime + cryptKey).ToLower();
                if (sign != signParam)
                {
                    return "{\"ret\":\"fail\",\"msg\":\"sign错误\"}";
                }

                decimal cash = ConvertHelper.ConvertToDecimal(price, 0) / 100; //a8价格单位是  分
                Guid managerId = Guid.Empty;
                try
                {
                    managerId = new Guid(playerId);
                }
                catch
                {
                    return "{\"ret\":\"fail\",\"msg\":\"未找到角色\"}";
                }
                var result = WebServerHandler.BuyPointShipments(platFormKey, "" + serverId, playerId,
                    gameOrderId, orderId,
                    cash, ConvertHelper.ConvertToInt(goodsId));
                if (result == 0)
                    return "{\"ret\":\"success\",\"msg\":\"ok\"}";
                switch (result)
                {
                    case 2062:
                        return "{\"ret\":\"fail\",\"msg\":\"未找到角色\"}";
                    case 5211:
                        return "{\"ret\":\"fail\",\"msg\":\"未找到订单\"}";
                    case 151:
                        return "{\"ret\":\"fail\",\"msg\":\"物品ID错误\"}";
                    default:
                        return "{\"ret\":\"fail\",\"msg\":\"系统内部错误\"}";
                }

            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("a8 dologinnew", ex);
                return "{\"ret\":\"fail\",\"msg\":\"系统内部错误2\"}";
            }

        }

        /// <summary>
        /// a8进入游戏（3）
        /// </summary>
        /// <returns></returns>
        private string StartGame(string openid, string state, string serverId, string nowTimestamp, string pf,
            string sessionId, string sign, string jsNeed, string nickName)
        {
            try
            {
                /* download-wyfth5.3333.cn/wyft1758/index.html?openid=pf+gid&state=xxx&serverId=8001&nowTimestamp=46546461321&pf=1758&sessionId&sign=xxx&jsNeed=0&nickName=xxx*/
                bool b = false;
                try
                {
                    b = WebServerHandler.SetStartGameEntity(ShareUtil.PlatformCode, ShareUtil.PlatformZoneName, openid,
                        state, serverId, pf, sessionId, jsNeed, nickName);
                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("A8 Login", ex);
                    b = false;
                }
                if (b) ;
                {
                    //登录
                    var result = Login(openid, platFormKey);

                    if (result != UAErrorCode.ErrOK)
                        return UAErrorCode.ErrOther;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("A8 Login", ex);
                Response.Write("{\"ret\":\"fail\",\"msg\":\"系统异常" + ex + "\"}");
                return UAErrorCode.ErrException;
            }
            return UAErrorCode.ErrOK;
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
            var channelAliasEntity = UAFactory.Instance.GetPlatform(platFormKey);
            if (channelAliasEntity == null)
            {
              
                return UAErrorCode.ErrPlatform;
            }
            string cryptKey = channelAliasEntity.LoginKey;
            string signParam = "";
            string signserverId = "default";
            signParam =
                CryptHelper.GetMD5(openid + state + signserverId + nowTimestamp + pf + sessionId + cryptKey).ToLower();


            if (sign != signParam)
            {
                return UAErrorCode.ErrCheckSign;
            }
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

        /// <summary>
        /// a8用户数据采集
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="openId"></param>
        public static void UserAction(string dataType, string openId = "", string session = "", string server = "",
            NbManagerEntity managerEntity = null)
        {
            //"{dataType,sessionId,gameNum,channelAlias,channelId,deviceId,model,release,uid,uname,serverId,serverName,roleId,roleName,roleLevel,ext,sdkVersion}";
            NbManagerEntity manager = null;
            if (managerEntity == null)
            {
                var list = NbManagerMgr.GetByAccount(openId, ShareUtil.ZoneName);
                if (list != null && list.Count > 0)
                {
                    manager = list[0];
                }
            }
            else
            {
                manager = managerEntity;

            }
            var postDataStr = "";
            if (manager == null)
            {
                postDataStr = "dataType=" + dataType;
            }
            else
            {
                var ip = "";
                try
                {
                    ip = UAHelper.GetRealIP();
                }
                catch (Exception)
                {

                }
                var ext = "";
                var channelAlias = "";
                var sessionId1 = "";
                var entity = WebServerHandler.GetStartgameEntity(openId, ShareUtil.PlatformCode,
                    ShareUtil.PlatformZoneName);
                if (entity != null)
                {
                    var serverId = entity.ServerId;
                    ext = entity.State;
                    channelAlias = entity.Pf;
                    sessionId1 = entity.SessionId;
                    //release=1   暂留,没有实际意义
                    var oId = UAHelper.StrToUtf8(openId);

                    postDataStr = "dataType=" + dataType + "&sessionId=" +
                                  sessionId1
                                  + "&gameNum=" + (int) A8csdkEnum.csdkId + "&channelAlias=" + channelAlias +
                                  "&channelId=" + entity.JsNeed + "&deviceId=" + ip + "&model=" + "&release=1" +
                                  "&channelAlias=" + channelAlias + "&subChannel=" + "&serverId=" + serverId +
                                  "&uid=" + oId + "&uname=" + "&serverId=" + serverId + "&serverName=" +
                                  serverId + "&roleId=" + openId +
                                  "&roleName=" + manager.Name + "&roleLevel=" + manager.Level + "&ext=" + ext +
                                  "&sdkVersion=";
                }
            }
            var url = UAFactory.Instance.GetRedirectURL(ShareUtil.PlatformCode, "useraction");
            var retString = UAHelper.HttpPost(url, postDataStr);
        }

        /// <summary>
        /// a8读取用户数据（7）
        /// </summary>
        /// <returns></returns>
        private String IsRegist()
        {
            string openId = GetParamNOUrlDecode("openId");
            string serverNo = GetParam("serverNo");
            string pf = GetParam("pf");
            string time = GetParam("time");
            string sign = GetParam("sign");
            if (string.IsNullOrEmpty(openId)
                || string.IsNullOrEmpty(serverNo)
                || string.IsNullOrEmpty(pf)
                || string.IsNullOrEmpty(time)
                || string.IsNullOrEmpty(sign)
                )
            {
                return UAErrorCode.ErrDataOP;
            }

            long time1 = ConvertHelper.ConvertToLong(time, 0);
            DateTime sourceTime = ShareUtil.GetTime((time1));

            DateTime nowTime = DateTime.Now;
            //检查时间是否过期
            if (sourceTime.AddSeconds(UAFactory.Instance.Timeout24Hour) < nowTime ||
                sourceTime.AddSeconds(-UAFactory.Instance.Timeout30min) > nowTime)
            {
                //记录详细的错误日志

                return UAErrorCode.ErrTimeout;
            }

            var channelAliasEntity = UAFactory.Instance.GetPlatform(platFormKey);
            if (channelAliasEntity == null)
            {
                return UAErrorCode.ErrPlatform;
            }

            string cryptKey = channelAliasEntity.LoginKey;
            string signParam =
                CryptHelper.GetMD5(openId + serverNo + pf + time + cryptKey).ToLower();
            if (sign != signParam)
            {
                return UAErrorCode.ErrCheckSign;
            }

            var entity = WebServerHandler.IsRegist("" + A8csdkEnum.h5_a8, "" + serverNo, openId);
            if (entity == null||string.IsNullOrEmpty(entity.Name))
            {
                return UAErrorCode.ErrNoUser;
            }
            var oId = UAHelper.StrToUtf8(openId);
            Response.Write("{\"ret\":\"success\",\"msg\":\"ok\",\"roleId\":\"" + entity.Idx + "\",\"roleName\":\"" +
                          oId + "\",\"roleLevel\":\"" + entity.Level + "\"" +
                           ",\"serverNo\":" + serverNo + "\"\",\"serverId\":\" " + serverNo + " \",\"serverName\":\"" +
                           serverNo + " \"}");
            return UAErrorCode.ErrOK;
        }

        /// <summary>
        /// a8查询战斗力接口9
        /// </summary>
        private string PowerValue()
        {
            try
            {
                var openId = GetParamNOUrlDecode("roleid");
                var serverid = GetParam("serverid");
                var sign = GetParam("sign");
                if (string.IsNullOrEmpty(openId) || string.IsNullOrEmpty(sign))
                {
                    UAHelper.WriteLog("ret", "参数不正确");
                    return UAErrorCode.ErrDataOP;
                }

                var channelAliasEntity = UAFactory.Instance.GetPlatform(platFormKey);
                if (channelAliasEntity == null)
                {
                    return UAErrorCode.ErrNoUser;
                }
                string cryptKey = channelAliasEntity.LoginKey;
                var signParam = CryptHelper.GetMD5(openId + serverid + cryptKey)
                    .ToLower();

                if (sign != signParam)
                {
                    UAHelper.WriteLog("ret", "sign错误");
                    return UAErrorCode.ErrCheckSign;
                }

                var manager = WebServerHandler.IsRegist(platFormKey, serverid, openId);
                var oId = UAHelper.StrToUtf8(openId);

                if (manager != null&&!string.IsNullOrEmpty(manager.Name))
                    Response.Write("{\"code\":0,\"data\":{\"uname\":\"" + oId + "\",\"sword\":\"" + manager.Kpi +
                                     "\",\"uid\":\"" +
                                     oId + "\",\"rolename\":\"" + manager.Name + "\"" +
                                     ",\"vip\":\"" + manager.VipLevel + "\",\"rolelevel\":\" " + manager.Level + " \"}}");
                else
                {
                    return UAErrorCode.ErrNoManager;
                }
                return UAErrorCode.ErrOK;
            }
            catch (Exception)
            {
                return UAErrorCode.ErrException;
            }

        }



        public override void doLogin()
        {
            string openid = GetParamNOUrlDecode("openid");
            string state = GetParam("state");
            string serverId = GetParam("serverId");
            string nowTimestamp = GetParam("nowTimestamp");
            string pf = GetParam("pf");
            string sessionId = GetParam("sessionId");
            string sign = GetParam("sign");
            string jsNeed = GetParam("jsNeed");
            string nickName = GetParam("nickName");
            string isSubscribe = GetParam("isSubscribe");
            string isShare = GetParam("isShare");
            var result = LoginCheck(openid, state, serverId, nowTimestamp, pf, sessionId, sign, jsNeed, nickName);
            if (result != "0")
                LoginResponse(result, pf, false);
            else
            {
                if (serverId == "default")
                {
                    openid = UAHelper.StrToUtf8(openid);
                    HttpContext.Current.Response.Redirect(
                     string.Format(
                         "Index.aspx?pf={0}&ck=default&openid={1}&state={2}&nowTimestamp={3}&sessionId={4}&sign={5}&jsNeed={6}&nickName={7}&isSubscribe={8}&isShare={9}",
                         pf, openid, state, nowTimestamp, sessionId, sign, jsNeed, nickName, isSubscribe, isShare));
                    HttpContext.Current.Response.End();
                }
                else
                {
                    var str = StartGame(openid, state, serverId, nowTimestamp, pf, sessionId, sign, jsNeed, nickName);
                    LoginResponse(str, pf, true);
                }
                //serverId = "1";
                //var str = StartGame(openid, state, serverId, nowTimestamp, pf, sessionId, sign, jsNeed, nickName);
                //LoginResponse(str, pf, true);
            }
        }

        public override void doCharge()
        {
            var result = Charge();
          Response.Write(result);

        }

        public override void doCheckActive()
        {
            var str = IsRegist();
            switch (str)
            {
                case UAErrorCode.ErrDataOP:
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
                default:
                    break;
            }
            Response.End();
        }

        public override void doRedirect(string userName, string redirectType)
        {
            throw new NotImplementedException();
        }

        public override void doLogout()
        {
            throw new NotImplementedException();
        }

        protected override string ColUid
        {
            get { return "username"; }
        }

        private string Login(string userName, string platform)
        {
            try
            {
                this.SetPlatSession();
                string returnCode = UAHelper.SaveLogindata(userName);

                try
                {
                    UserAction("entergame", userName);
                }
                catch (Exception ex)
                {

                }
                return returnCode;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("h5_a8 dologin", ex);
                return UAErrorCode.ErrOther;
            }
        }



        public override void doPowerValue()
        {
            var str = PowerValue();
            switch (str)
            {
                case UAErrorCode.ErrOK:
                    break;
                default:
                    Response.Write("{\"code\":1,\"data\":{}}");
                    Response.End();
                    break;
            }

        }

        /// <summary>
        /// 最新登录
        /// </summary>
        /// <returns></returns>
        public override int doLoginNew()
        {
            string openid = GetParam("openid");
            string state = GetParam("state");
            string serverId = GetParam("serverId");
            string nowTimestamp = GetParam("nowTimestamp");
            string pf = GetParam("pf");
            string sessionId = GetParam("sessionId");
            string sign = GetParam("sign");
            string jsNeed = GetParam("jsNeed");
            string nickName = GetParam("nickName");
            string isSubscribe = GetParam("isSubscribe");
            var result = LoginCheckNew(openid, state, serverId, nowTimestamp, pf, sessionId, sign);
            if (result != MessageCode.Success)
                return (int) result;
            if (serverId == "default")
            {
                openid = UAHelper.StrToUtf8(openid);
                HttpContext.Current.Response.Redirect(
                    string.Format(
                        "Index.aspx?pf={0}&ck=default&openid={1}&state={2}&nowTimestamp={3}&sessionId={4}&sign={5}&jsNeed={6}&nickName={7}&isSubscribe={8}",
                        pf, openid, state, nowTimestamp, sessionId, sign, jsNeed, nickName, isSubscribe));
                HttpContext.Current.Response.End();
            }
            else
            {
                var str = StartGameNew(openid, state, serverId, pf, sessionId, jsNeed, nickName);

                try
                {
                   
                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("txLogin",ex.Message);
                   
                }

                return (int)str;
            }
            return (int) MessageCode.Success;
        }

     

        private MessageCode LoginCheckNew(string openid, string state, string serverId, string nowTimestamp, string pf,
            string sessionId, string sign)
        {
            if (string.IsNullOrEmpty(openid)
                || string.IsNullOrEmpty(state)
                || string.IsNullOrEmpty(serverId)
                || string.IsNullOrEmpty(nowTimestamp)
                || string.IsNullOrEmpty(pf)
                || string.IsNullOrEmpty(sessionId)
                || string.IsNullOrEmpty(sign))
            {
                return MessageCode.LoginNoLogin;
            }

            if (nowTimestamp.IndexOf(' ') >= 0)
            {
                var times = nowTimestamp.Split(' ');
                if (times.Length > 0)
                {
                    nowTimestamp = times[0];
                }
            }
            var channelAliasEntity = UAFactory.Instance.GetPlatform("" + A8csdkEnum.h5_a8);
            if (channelAliasEntity == null)
            {
                return MessageCode.LoginNoLogin;
            }
            string cryptKey = channelAliasEntity.LoginKey;
            string signParam = "";
            string signserverId = "default";
            signParam =
                CryptHelper.GetMD5(openid + state + signserverId + nowTimestamp + pf + sessionId + cryptKey).ToLower();
            if (sign != signParam)
            {
                return MessageCode.LoginNoLogin;
            }
            return MessageCode.Success;
        }


        private MessageCode StartGameNew(string openid, string state, string serverId, string pf,
          string sessionId, string jsNeed, string nickName)
        {
            try
            {
               WebServerHandler.SetStartGameEntity(ShareUtil.PlatformCode, serverId, openid, state, serverId, pf, sessionId, jsNeed, nickName) ;
                
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("a8 StartGameNew", ex);
                return MessageCode.LoginError;
            }
            return MessageCode.Success;
        }
        
        public override void doOtherOne()
        {
            string str=OtherOne();
            switch (str)
            {
                case UAErrorCode.ErrOK:
                    break;
                default:
                    Response.Write("{\"code\":-1}");
                    Response.End();
                    break;
            }
        }
        /// <summary>
        /// 新增查询
        /// </summary>
        /// <returns></returns>
        public string OtherOne()
        {
            // {code:0, uid:xxx,uname:xxxx,roleid:xxxx,rolename:xxxx,vip:xxx,sword:xxx,rolelevel:xxxx}
            try
            {

                var openId = GetParamNOUrlDecode("uid");
                var serverid = GetParam("serverid");
                var sign = GetParam("sign");
                if (string.IsNullOrEmpty(openId) || string.IsNullOrEmpty(sign))
                {
                    UAHelper.WriteLog("ret", "参数不正确");
                    return UAErrorCode.ErrDataOP;
                }

                var channelAliasEntity = UAFactory.Instance.GetPlatform(platFormKey);
                if (channelAliasEntity == null)
                {
                    return UAErrorCode.ErrNoUser;
                }
                string cryptKey = channelAliasEntity.LoginKey;
                var signParam = CryptHelper.GetMD5(openId + serverid + cryptKey)
                    .ToLower();

                if (sign != signParam)
                {
                    UAHelper.WriteLog("ret", "sign错误");
                    return UAErrorCode.ErrCheckSign;
                }

                var manager = WebServerHandler.IsRegist(platFormKey, serverid, openId);
                var oId = UAHelper.StrToUtf8(openId);
                
                if (manager != null&&!string.IsNullOrEmpty(manager.Name))
                    Response.Write("{\"code\":0,\"uname\":\"" + openId + "\",\"sword\":\"" + manager.Kpi +
                                   "\",\"uid\":\"" + oId + "\",\"rolename\":\"" + manager.Name + "\"" +
                                   ",\"roleid\":\"" + manager.Account + "\"" +
                                   ",\"vip\":\"" + manager.VipLevel + "\",\"rolelevel\":\"" + manager.Level + "\"}");
                else
                {
                    return UAErrorCode.ErrNoManager;
                }
                return UAErrorCode.ErrOK;
            }
            catch (Exception)
            {
                return UAErrorCode.ErrException;
            }

        }
        /// <summary>
        /// a8新增渠道查询接口
        /// </summary>
        public override void doOtherTwe()
        {
            var str = IsRegistByNameList();
            
            switch (str)
            {
                case UAErrorCode.ErrDataOP:
                    Response.Write("{\"code\":\"5\",\"data\":[]}");
                    break;
                case UAErrorCode.ErrOther:
                    Response.Write("{\"code\":\"99\",\"data\":[]}");
                    break;
                case UAErrorCode.ErrTimeout:
                    Response.Write("{\"code\":\"6\",\"data\":[]}");
                    break;
                case UAErrorCode.ErrPlatform:
                    Response.Write("{\"code\":\"3\",\"data\":[]}");
                    break;
                case UAErrorCode.ErrCheckSign:
                    Response.Write("{\"code\":\"2\",\"data\":[]}");
                    break;
                case UAErrorCode.ErrNoUser:
                    Response.Write("{\"code\":\"8\",\"data\":[]}");
                    break;
                default:
                    Response.Write(str);
                    break;
            }
            Response.End();
        }
        public string IsRegistByNameList()
        {
            var data = GetParamNOUrlDecode("data");
            string serverid = GetParam("serverId");
            string time = GetParam("time");
            string sign = GetParam("sign");
            if (string.IsNullOrEmpty(data)||
                string.IsNullOrEmpty(serverid)||
                string.IsNullOrEmpty(time)||
                string.IsNullOrEmpty(sign))
            {
                return UAErrorCode.ErrDataOP;
            }
            var key = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.H5_A8DataKey);
            var appSign = CryptHelper.GetMD5(data+serverid+time+key).ToLower();
            if (sign != appSign)
                return UAErrorCode.ErrCheckSign;
            data = GetParam("data");
            //UAHelper.WriteLog("IsRegistByNameList", "|" + data + "|" + serverid + "|" + time + "|" + sign + "|");

            var managerList = WebServerHandler.IsRegistByNameList(platFormKey, serverid, data);
            if (managerList == null || managerList.Length <= 0 || string.IsNullOrEmpty(managerList[0].Name))
                return UAErrorCode.ErrNoUser;
            string str = "";
            foreach (var manager in managerList)
            {
                var rowTime = manager.RowTime;
                var registTime = ShareUtil.GetTimeTick(rowTime);
                str += "{\"name\":\"" + manager.Name + "\",\"serverId\":\"" + serverid + "\",\"gamelevel\":\"" + manager.Level +
                "\",\"registtime\":\"" + registTime + "\"},";
            }
            string r=str.Substring(0, str.Length - 1);
            return "{\"code\":\"0\",\"data\":["+r+"]}";
        }
        /// <summary>
        /// 渠道发送分享物品
        /// </summary>
        /// <returns></returns>
        public string SendItemByShare()
        {
            try
            {
                var name = GetParamNOUrlDecode("name");
                
                var serverid = GetParam("serverid");
                var time = GetParam("time");
                var type = GetParam("type");

                var sign = GetParam("sign");
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(sign) ||
                    string.IsNullOrEmpty(serverid) || 
                    string.IsNullOrEmpty(time) || string.IsNullOrEmpty(type)
                    )
                {
                  
                    return UAErrorCode.ErrDataOP;
                }

                var key = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.H5_A8DataKey);
                var appSign = CryptHelper.GetMD5(name + serverid + type + time + key).ToLower();
                if (sign != appSign)
                    return UAErrorCode.ErrCheckSign;
                name = GetParam("name");
                var t = ConvertHelper.ConvertToInt(type);
                 var code = WebServerHandler.SendItemByShare(platFormKey, serverid, name,t);
                 if (code == (int)MessageCode.NbParameterError)
                     return UAErrorCode.ErrDataOP;
                if (code == (int) MessageCode.LoginNoUser)
                    return UAErrorCode.ErrNoManager;
                if (code == (int) MessageCode.Exception)
                    return UAErrorCode.ErrException;
                if (code == (int)MessageCode.TourPassPrizeHasReceive)
                    return UAErrorCode.ErrRepeatOrder;

                return ""+code;
            }
            catch (Exception)
            {
                return UAErrorCode.ErrException;
            }


        }

        public override void doOtherThree()
        {
            var str = SendItemByShare();

            switch (str)
            {
                case UAErrorCode.ErrDataOP:
                    Response.Write("{\"code\":\"5\"}");
                    break;
                case UAErrorCode.ErrOther:
                    Response.Write("{\"code\":\"99\"}");
                    break;
                case UAErrorCode.ErrTimeout:
                    Response.Write("{\"code\":\"6\"}");
                    break;
                case UAErrorCode.ErrPlatform:
                    Response.Write("{\"code\":\"3\"}");
                    break;
                case UAErrorCode.ErrCheckSign:
                    Response.Write("{\"code\":\"2\"}");
                    break;
                case UAErrorCode.ErrNoUser:
                    Response.Write("{\"code\":\"8\"}");
                    break;
                case UAErrorCode.ErrRepeatOrder:
                    Response.Write("{\"code\":\"4\"}");
                    break;
                default:
                    Response.Write("{\"code\":\"" + str + "\"}");
                    break;
            }
            Response.End();
        }
    }

    public class JsonResult
    {
        public string ret { get; set; }
        public string msg { get; set; }
        public string payURL { get; set; }
    }
   
}
