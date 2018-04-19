using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.ServiceContract.Client;
using Games.NBall.UAFacade.UABll;
using Games.NBall.WebServerFacade;

namespace Games.NBall.UAFacade
{
    public class UA_egret:UAAdapter
    {
        private readonly string platFormKey = "h5_egret";

        /// <summary>
        /// egret充值（1）
        /// </summary>
        /// <returns></returns>
        private string Charge()
        {
            try
            {


                string sign = GetParam("sign");
                string billingId = GetParam("orderId");
                string price = GetParam("money");
                string playerId = GetParam("id");
                string serverId = GetParam("serverId");
                string goodsId = GetParam("goodsId");
                string orderId = GetParam("ext");
                string payTime = GetParam("time");
                string goodsNumber = GetParam("goodsNumber");

              
                if (string.IsNullOrEmpty(sign)
                    || string.IsNullOrEmpty(orderId)
                    || string.IsNullOrEmpty(price)
                    || string.IsNullOrEmpty(playerId)
                    || string.IsNullOrEmpty(serverId)
                    || string.IsNullOrEmpty(goodsId)
                    || string.IsNullOrEmpty(sign)
                    || string.IsNullOrEmpty(payTime)
                    || string.IsNullOrEmpty(billingId)
                    )
                {
                    return "{\"code\":-1,\"msg\":\"参数为空\"}";
                }
                //var  strs = orderId.Split('|');
                //var zone = "";
                //if (strs.Count() >= 2)
                //    zone = strs[0];
              
                long time1 = ConvertHelper.ConvertToLong(payTime, 0);
                DateTime sourceTime = ShareUtil.GetTime(time1*1000);
                DateTime nowTime = DateTime.Now;
                //检查时间是否过期
                if (sourceTime.AddSeconds(UAFactory.Instance.Timeout30min) < nowTime ||
                    sourceTime.AddSeconds(-UAFactory.Instance.Timeout30min) > nowTime)
                {
                    //记录详细的错误日志
                    return "{\"code\":-1,\"msg\":\"响应超时\"}";
                }
                var platFormentity = UAFactory.Instance.GetPlatform(platFormKey);
                if (platFormentity == null)
                {
                    return "{\"code\":-1,\"msg\":\"platFormentity错误\"}";
                }

                string cryptKey = platFormentity.ChargeKey;
               
                string signParam =
                    CryptHelper.GetMD5(string.Format(@"ext={0}goodsId={1}goodsNumber={2}id={3}money={4}orderId={5}serverId={6}time={7}{8}",
                    orderId,goodsId,goodsNumber,playerId,price,billingId,serverId,payTime,cryptKey));
                if (sign != signParam)
                {
                    return "{\"code\":-1,\"msg\":\"sign错误\"}";
                }
                decimal cash = ConvertHelper.ConvertToDecimal(price, 0) ; //egret价格单位是  元
                //var zone = "";
                //if (serverId == "1002")
                //    zone = "bls2";
                //else
                //{
                //    zone = "bls1";
                //}
                //var list = NbManagerMgr.GetByAccount(playerId,zone);
                //if(list==null)
                //    return "{\"code\":-1,\"msg\":\"未找到角色\"}";
                //var manager = list[0];
             
                var result = 0;
                try
                {
                    result = WebServerHandler.BuyPointShipments(platFormKey, "" + serverId, "" + playerId,
                   orderId, billingId, cash, ConvertHelper.ConvertToInt(goodsId));//白鹭传account
                }
                catch (Exception)
                {
                    
                }
               
                if (result == 0)
                    return "{\"code\":0,\"msg\":\"ok\"}";
                switch (result)
                {
                    case 2062:
                        return "{\"code\":-1,\"msg\":\"未找到角色\"}";
                    case 5211:
                        return "{\"code\":-1,\"msg\":\"未找到订单\"}";
                    case 151:
                        return "{\"code\":-1,\"msg\":\"物品ID错误\"}";
                    default:
                        return "{\"code\":-1,\"msg\":\"系统内部错误\""+result+"}";
                }

            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("h5_egret Charge", ex);
                return "{\"code\":-1,\"msg\":\"系统内部错误2\"}";
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
            var channelAliasEntity = UAFactory.Instance.GetPlatform(platFormKey);
            if (channelAliasEntity == null)
            {
                UAHelper.WriteLog("ret", "登录渠道无效");
                return UAErrorCode.ErrPlatform;
            }
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
                    //release=1   暂留,没有实际意义
                    postDataStr = "dataType=" + dataType + "&sessionId=" +
                                  sessionId1
                                  + "&gameNum=" + (int)UAEnum.EgretAppId + "&channelAlias=" + channelAlias +
                                  "&channelId=" + entity.JsNeed + "&deviceId=" + ip + "&model=" + "&release=1" +
                                  "&channelAlias=" + channelAlias + "&subChannel=" + "&serverId=" + serverId +
                                  "&uid=" + openId + "&uname=" + "&serverId=" + serverId + "&serverName=" +
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

            var channelAliasEntity = UAFactory.Instance.GetPlatform(platFormKey);
            if (channelAliasEntity == null)
            {
                return UAErrorCode.ErrPlatform;
            }

            string cryptKey = channelAliasEntity.ChargeKey;
            string signParam =
                CryptHelper.GetMD5(openId + serverNo + pf + time + cryptKey).ToLower();
            if (sign != signParam)
            {
                return UAErrorCode.ErrCheckSign;
            }

            var entity = WebServerHandler.IsRegist(platFormKey, "" + serverNo, openId);
            if (entity == null)
            {
                return UAErrorCode.ErrNoUser;
            }
            Response.Write("{\"ret\":\"success\",\"msg\":\"ok\",\"roleId\":\"" + entity.Idx + "\",\"roleName\":\"" +
                           entity.Account + "\",\"roleLevel\":\"" + entity.Level + "\"" +
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

                if (manager != null)
                    Response.Write("{\"code\":0,\"data\":{\"uname\":\"" + openId + "\",\"sword\":\"" + manager.Kpi +
                                   "\",\"uid\":\"" +
                                   openId + "\",\"rolename\":\"" + manager.Name + "\"" +
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
            //string appId = GetParam("appId");
            string platInfo = GetParam("platInfo");
            //string spid = GetParam("egret.runtime.spid");
            //string channelId = GetParam("channelId");
            string isNewApi = GetParam("isNewApi");
            //string egretSdkDomain = GetParam("egretSdkDomain");
            string showLoginPanel = GetParam("showLoginPanel");
            string egretstartfrom = GetParam("egretstartfrom");

            var token = GetParam("token");
            var egretId = GetParam("egretId");
            var appId = GetParam("appId");
            var channelId = GetParam("channelId");
            string spid = GetParam("egret.runtime.spid");
            string egretRv = GetParam("egretRv");
            string egretSdkDomain = GetParam("egretSdkDomain");
            string egretServerDomain = GetParam("egretServerDomain");
            var userId = GetParam("userId");
            var userName = GetParam("userName");
            var userImg = GetParam("userImg");
            var userSex = GetParam("userSex");
            var sign = GetParam("sign");
            var egretOauthUser = GetParam("egretOauthUser");
            var egretChannelId = GetParam("egretChannelId");


            
            if (string.IsNullOrEmpty(platInfo))
            {
                platInfo = "open_90500_" + channelId;
            }

          
            try
            {
                
                var ary = appId.Split(',');
                appId = ary[0];
            }
            catch (Exception)
            {
                
            }

            {
                HttpContext.Current.Response.Redirect(
                    "Index.aspx" + Request.Url.Query + "&ck=default&pf="+platFormKey);
                HttpContext.Current.Response.End();
          
                    //HttpContext.Current.Response.Redirect(
                    // string.Format(
                    //     "Index.aspx?appId={0}&egret.runtime.spid={1}&channelId={2}&platInfo={3}&isNewApi={4}&egretSdkDomain={5}&egretServerDomain={6}&egretRv={7}&ck=default&pf={8}&token={9}&egretId={10}&userId={11}&userName={12}&userImg={13}&userSex={14}&sign={15}&egretOauthUser={16}&egretChannelId={17}&egretstartfrom={18}&showLoginPanel{19}",
                    //     appId, spid, channelId, platInfo, isNewApi, egretSdkDomain, egretServerDomain, egretRv, platFormKey, token, egretId, userId, userName, userImg, userSex, sign, egretOauthUser, egretChannelId, egretstartfrom, showLoginPanel));
                    //HttpContext.Current.Response.End();
                
          
                //serverId = "1";
                //var str = StartGame(openid, state, serverId, nowTimestamp, pf, sessionId, sign, jsNeed, nickName);
                //LoginResponse(str, pf, true);
            }
        }

        public override void doCharge()
        {
            var result = Charge();
            UAHelper.WriteLog("DoCharge", result);
            UAHelper.WriteError(result);

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
                SystemlogMgr.Error("egret dologin", ex);
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
            var openId = GetParam("openid");

           var serverId= GetParam("serverId");
             var pf= GetParam("pf");
            
            var r = StartGameNew(openId, "1", serverId, pf, "1", "", "");
            
            return (int)MessageCode.Success;
        }

        private void UserActionNew(string dataType, string sessionId, string channelAlias, string openId, string jsNeed, string serverId, string state)
        {
            //"{dataType,sessionId,gameNum,channelAlias,channelId,deviceId,model,release,uid,uname,serverId,serverName,roleId,roleName,roleLevel,ext,sdkVersion}";
            NbManagerEntity manager = null;
            var name = "";
            var level = 1;
            try
            {
                var list = NbManagerMgr.GetByAccount(openId, serverId);
                if (list != null && list.Count > 0)
                {
                    manager = list[0];
                }
                name = manager.Name;
                level = manager.Level;
            }
            catch (Exception)
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


                //release=1   暂留,没有实际意义
                postDataStr = "dataType=" + dataType + "&sessionId=" +
                              sessionId
                              + "&gameNum=" + (int)UAEnum.EgretAppId+ "&channelAlias=" + channelAlias +
                              "&channelId=" + jsNeed + "&deviceId=" + ip + "&model=" + "&release=1" +
                              "&channelAlias=" + channelAlias + "&subChannel=" + "&serverId=" + serverId +
                              "&uid=" + openId + "&uname=" + "&serverId=" + serverId + "&serverName=" +
                              serverId + "&roleId=" + openId +
                              "&roleName=" + name + "&roleLevel=" + level + "&ext=" + state +
                              "&sdkVersion="; //第一个枚举参数不变     第二个枚举参数作废不更改

            }
            var url = UAFactory.Instance.GetRedirectURL(ShareUtil.PlatformCode, "useraction");
            var retString = UAHelper.HttpPost(url, postDataStr);
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
            var channelAliasEntity = UAFactory.Instance.GetPlatform(platFormKey);
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
               if(WebServerHandler.SetStartGameEntity(ShareUtil.PlatformCode, serverId, openid, state, serverId, pf, sessionId, jsNeed, nickName) );
               return MessageCode.Exception;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("egret login", ex);
                return MessageCode.LoginError;
            }
            return MessageCode.Success;
        }

        public override void doOtherOne()
        {
            string str = OtherOne();
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

        public string OtherOne()
        {
            // {code:0, uid:xxx,uname:xxxx,roleid:xxxx,rolename:xxxx,vip:xxx,sword:xxx,rolelevel:xxxx}
            try
            {
                var openId = GetParam("uid");
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
                var manager = WebServerHandler.IsRegist(platFormKey, serverid,openId);

                if (manager != null)
                    Response.Write("{\"code\":0,\"uname\":\"" + openId + "\",\"sword\":\"" + manager.Kpi +
                                   "\",\"uid\":\"" + openId + "\",\"rolename\":\"" + manager.Name + "\"" +
                                   ",\"roleid:\"" + manager.Account + "\"" +
                                   ",\"vip\":\"" + manager.VipLevel + "\",\"rolelevel\":\" " + manager.Level + " \"}");
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

        public override void doOtherTwe()
        {
            throw new NotImplementedException();
        }

        public override void doOtherThree()
        {
            throw new NotImplementedException();
        }
    }

    public class JsonResultE
    {
        public string ret { get; set; }
        public string msg { get; set; }
        public string payURL { get; set; }
    }
}
