using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.UAFacade.UABll;
using Games.NBall.WebServerFacade;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;

namespace Games.NBall.UAFacade
{
    public class UA_wbTxA8 : UAAdapter
    {
        private readonly string platFormKey = "txh5_a8";

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

                var channelAliasEntity = UAFactory.Instance.GetPlatform("" + A8csdkEnum.txh5_a8);
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

                decimal cash = ConvertHelper.ConvertToDecimal(price,0) / 100; //a8价格单位是  分

                Guid managerId = Guid.Empty;
                try
                {
                    managerId = new Guid(playerId);
                }
                catch
                {
                    return "{\"ret\":\"fail\",\"msg\":\"未找到角色\"}";
                }
                var result = WebServerHandler.BuyPointShipments("" + A8csdkEnum.txh5_a8, "" + serverId, playerId,
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
                SystemlogMgr.Error("tx Charge", ex);
                return "{\"ret\":\"fail\",\"msg\":\"系统内部错误2\"}";
            }

        }

        /// <summary>
        /// a8进入游戏（3）
        /// </summary>
        /// <returns></returns>
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
                    //登录
                    var result = Login(openid);

                    if (result != UAErrorCode.ErrOK)
                        return UAErrorCode.ErrOther;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("txa8 StartGame", ex);
                Response.Write("{\"ret\":\"fail\",\"msg\":\"系统异常" + ex + "\"}");
                return UAErrorCode.ErrException;
            }
            return UAErrorCode.ErrOK;
        }

        private string LoginCheck(string openid, string state, string serverId, string nowTimestamp, string pf,
            string sessionId, string sign, string jsNeed, string nickName, string qqOpenid, string qqOpenkey,
            string qqPf, string platform)
        {
            if (string.IsNullOrEmpty(openid)
                || string.IsNullOrEmpty(state)
                || string.IsNullOrEmpty(serverId)
                || string.IsNullOrEmpty(nowTimestamp)
                || string.IsNullOrEmpty(pf)
                || string.IsNullOrEmpty(sessionId)
                || string.IsNullOrEmpty(sign)
                || string.IsNullOrEmpty(qqOpenid)
                || string.IsNullOrEmpty(qqOpenkey)
                || string.IsNullOrEmpty(qqPf)
                || string.IsNullOrEmpty(platform))
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
            if (channelAliasEntity == null)
            {
                UAHelper.WriteLog("ret", "登录渠道无效");
                return UAErrorCode.ErrPlatform;
            }
            //md5(openid+state+serverId+nowTimestamp+pf+sessionId+nickName+md5Key）.tolowcase()
            string cryptKey = channelAliasEntity.LoginKey;
            string signParam = "";
            string signserverId = "default";

            signParam =
                CryptHelper.GetMD5(openid + state + signserverId + nowTimestamp + pf + sessionId + cryptKey).ToLower();
     
            if (sign != signParam)
            {
                UAHelper.WriteLog("ret", "fail");
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
                    var oId = UAHelper.StrToUtf8(openId);

                    //release=1   暂留,没有实际意义
                    postDataStr = "dataType=" + dataType + "&sessionId=" +
                                  sessionId1
                                  + "&gameNum=" + (int) A8csdkEnum.csdkId + "&channelAlias=" + channelAlias +
                                  "&channelId=" + entity.JsNeed + "&deviceId=" + ip + "&model=" + "&release=1" +
                                  "&channelAlias=" + channelAlias + "&subChannel=" + "&serverId=" + serverId +
                                  "&uid=" + oId + "&uname=" + "&serverId=" + serverId + "&serverName=" +
                                  serverId + "&roleId=" + openId +
                                  "&roleName=" + manager.Name + "&roleLevel=" + manager.Level + "&ext=" + ext +
                                  "&sdkVersion="; //第一个枚举参数不变     第二个枚举参数作废不更改
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
            string openId = GetParam("openId");
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

            var channelAliasEntity = UAFactory.Instance.GetPlatform("" + A8csdkEnum.txh5_a8);
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

            var entity = WebServerHandler.IsRegist("" + A8csdkEnum.txh5_a8, "" + serverNo, openId);
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
                var openId = GetParam("roleid");
                var serverid = GetParam("serverid");
                var sign = GetParam("sign");
                if (string.IsNullOrEmpty(openId) || string.IsNullOrEmpty(sign))
                {
                    UAHelper.WriteLog("ret", "参数不正确");
                    return UAErrorCode.ErrDataOP;
                }

                var channelAliasEntity = UAFactory.Instance.GetPlatform("" + A8csdkEnum.txh5_a8);
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

                var manager = WebServerHandler.IsRegist("" + A8csdkEnum.txh5_a8, "" + serverid, openId);
                    //第一个枚举枚举参数修改腾讯,第二个枚举参数作废可以不改
                var oId = UAHelper.StrToUtf8(openId);
                
                if (manager != null&&!string.IsNullOrEmpty(manager.Name))
                    Response.Write("{\"code\":0,\"data\":{\"uname\":\"" + openId + "\",\"sword\":\"" + manager.Kpi +
                                   "\",\"uid\":\"" +
                                   oId + "\",\"rolename\":\"" + manager.Name + "\"" +
                                   ",\"vip\":" + manager.VipLevel + "\"\",\"rolelevel\":\" " + manager.Level + " \"}}");
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
            string openid = GetParam("openid");
            string state = GetParam("state");
            string serverId = GetParam("serverId");
            string nowTimestamp = GetParam("nowTimestamp");
            string pf = GetParam("pf");
            string sessionId = GetParam("sessionId");
            string sign = GetParam("sign");
            string jsNeed = GetParam("jsNeed");
            string nickName = GetParam("nickName");
            string qqOpenid = GetParam("qqopenid");
            string qqOpenkey = GetParam("qqopenkey");
            string qqPf = GetParam("qqpf");
            string platform = GetParam("platform");
            string share = GetParam("share");
            string shareType = GetParam("shareType");
            var result = LoginCheck(openid, state, serverId, nowTimestamp, pf, sessionId, sign, jsNeed, nickName,
                qqOpenid, qqOpenkey, qqPf, platform);
            SystemlogMgr.Info("ceshi", "ceshi:" + Request.Url.AbsoluteUri);

            if (result != "0")
                LoginResponse(result, pf, false);
            else
            {
                if (serverId == "default")
                {
                    openid = UAHelper.StrToUtf8(openid);
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
                    SetVipInfo(openid, qqOpenid, qqOpenkey, qqPf, platform);

                    str = StartGame(openid, state, serverId, nowTimestamp, pf, sessionId, sign, jsNeed, nickName,
                        strCommon);
                    LoginResponse(str, pf, true);
                }

            }
        }

        public override void doOtherThree()
        {
            throw new NotImplementedException();
        }

        public override void doCharge()
        {
            var result = Charge();
        

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

        private string Login(string userName)
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
                SystemlogMgr.Error("TxA8 dologin", ex);
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

        private bool SetVipInfo(string openid, string qqOpenid, string qqOpenkey, string qqPf, string platform)
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
                            return false;
                        }
                        else if (ret == "success")
                        {

                            var json2 = serializer.Deserialize<InnerData>(json.data);

                            bool flag = json2.is_vip.ToLower() == "true";
                            int vipLevel =(int) ConvertHelper.ConvertToDouble(json2.vip_level);
                            var str1 = json2.score + "|" + json2.expiredtime;
                            return TxYellowvipMgr.Add(openid, flag, false, false, vipLevel, str1, null,
                                "" + ShareUtil.ZoneName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("{\"ret\":\"fail\",\"msg\":\"" + ex.Message + "}");
                }
            }

            return false;
        }

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
            string qqOpenid = GetParam("qqopenid");
            string qqOpenkey = GetParam("qqopenkey");
            string qqPf = GetParam("qqpf");
            string platform = GetParam("platform");
            string share = GetParam("share");
            string shareType = GetParam("shareType");


            var result = LoginCheckNew(openid, state, serverId, nowTimestamp, pf, sessionId, sign, qqOpenid, qqOpenkey,
                qqPf, platform);
            if (result != MessageCode.Success)
                return (int) result;
            if (serverId == "default")
            {
                openid = UAHelper.StrToUtf8(openid);
                HttpContext.Current.Response.Redirect(
                    string.Format(
                        "Index.aspx?pf={0}&ck=default&openid={1}&state={2}&nowTimestamp={3}&sessionId={4}&sign={5}&jsNeed={6}&nickName={7}&qqopenid={8}&qqopenkey={9}&qqpf={10}&platform={11}&share={12}&shareType={13}",
                        pf, openid, state, nowTimestamp, sessionId, sign, jsNeed, nickName, qqOpenid, qqOpenkey,
                        qqPf, platform, share, shareType));
                HttpContext.Current.Response.End();
            }
            else
            {
                var strCommon = qqOpenid + "|" + qqOpenkey + "|" + qqPf + "|" + platform;
                SetVipInfo(openid, qqOpenid, qqOpenkey, qqPf, platform);

                var str = StartGameNew(openid, state, serverId, pf, sessionId, jsNeed, nickName,
                    strCommon);
                try
                {
                   

                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("txLogin",ex.Message);
                   
                }
                return (int) str;
            }
            return (int) MessageCode.Success;
        }

        private void UserActionNew(string dataType, string sessionId, string channelAlias, string openId, string jsNeed, string serverId,string state)
        {
            //"{dataType,sessionId,gameNum,channelAlias,channelId,deviceId,model,release,uid,uname,serverId,serverName,roleId,roleName,roleLevel,ext,sdkVersion}";
            NbManagerEntity manager = null;
            var name = "";
            var level = 1;
            try
            {
                var list = NbManagerMgr.GetByAccount(openId,ShareUtil.ZoneName);
                if (list != null && list.Count > 0)
                {
                    manager = list[0]; 
                    name = manager.Name;
                    level = manager.Level;
                }
              
            }
            catch (Exception ex)
            {
                
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
                var oId = UAHelper.StrToUtf8(openId);
               
                   
                    //release=1   暂留,没有实际意义
                    postDataStr = "dataType=" + dataType + "&sessionId=" +
                                  sessionId
                                  + "&gameNum=" + (int)A8csdkEnum.csdkId + "&channelAlias=" + channelAlias +
                                  "&channelId=" + jsNeed + "&deviceId=" + ip + "&model=" + "&release=1" +
                                  "&channelAlias=" + channelAlias + "&subChannel=" + "&serverId=" + serverId +
                                  "&uid=" + oId + "&uname=" + "&serverId=" + serverId + "&serverName=" +
                                  serverId + "&roleId=" + openId +
                                  "&roleName=" + name + "&roleLevel=" + level + "&ext=" + state +
                                  "&sdkVersion="; //第一个枚举参数不变     第二个枚举参数作废不更改
                
            }
            var url = UAFactory.Instance.GetRedirectURL(ShareUtil.PlatformCode, "useraction");
            var retString = UAHelper.HttpPost(url, postDataStr);
        }

        private MessageCode LoginCheckNew(string openid, string state, string serverId, string nowTimestamp, string pf,
            string sessionId, string sign, string qqOpenid, string qqOpenkey, string qqPf, string platform)
        {
            if (string.IsNullOrEmpty(openid)
                || string.IsNullOrEmpty(state)
                || string.IsNullOrEmpty(serverId)
                || string.IsNullOrEmpty(nowTimestamp)
                || string.IsNullOrEmpty(pf)
                || string.IsNullOrEmpty(sessionId)
                || string.IsNullOrEmpty(sign)
                || string.IsNullOrEmpty(qqOpenid)
                || string.IsNullOrEmpty(qqOpenkey)
                || string.IsNullOrEmpty(qqPf)
                || string.IsNullOrEmpty(platform))
            {
                return MessageCode.LoginError;
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
            if (channelAliasEntity == null)
            {
                return MessageCode.LoginError;
            }
            //md5(openid+state+serverId+nowTimestamp+pf+sessionId+nickName+md5Key）.tolowcase()
            string cryptKey = channelAliasEntity.LoginKey;
            string signParam = "";
            string signserverId = "default";

            signParam =
                CryptHelper.GetMD5(openid + state + signserverId + nowTimestamp + pf + sessionId + cryptKey).ToLower();

            if (sign != signParam)
            {
                return MessageCode.LoginError;
            }
            return MessageCode.Success;
        }

        private MessageCode StartGameNew(string openid, string state, string serverId, string pf, string sessionId, string jsNeed, string nickName, string strCommon = "")
        {
            try
            {
                if (WebServerHandler.SetStartGameEntity(ShareUtil.PlatformCode, serverId, openid, state,
                    serverId, pf, sessionId, jsNeed, nickName, strCommon)) ;
                {
                    ////登录
                    //var result = Login(openid);

                    //if (result != UAErrorCode.ErrOK)
                    //    return MessageCode.LoginError;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("txa8 StartGameNew", ex);
                return MessageCode.LoginError;
            }
            return MessageCode.Success;
        }


        public override void doOtherOne()
        {
            throw new NotImplementedException();
        }

        public override void doOtherTwe()
        {
            throw new NotImplementedException();
        }
    }
}
    public class TxJsonResult
    {
        public string ret { get; set; }
    }
     public class TxVipJsonResult
    {
        public string ret { get; set; }
        public string code { get; set; }
        public string msg { get; set; }
        public string  data { get; set; }
        
    }
     public class InnerData
     {
         public string is_vip { get; set; }
         public string vip_level { get; set; }
         public string score { get; set; }
         public string expiredtime { get; set; }
     }
    
    


