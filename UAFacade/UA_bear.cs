using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Manager;
using Games.NBall.UAFacade.UABll;
using Games.NBall.WebServerFacade;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;

namespace Games.NBall.UAFacade
{
    public class UA_bear:UAAdapter
    {
        private readonly string platFormKey = "h5_bear";
        private readonly int gameAppId = 16;


        /// <summary>
        /// pay回调
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
                string nonce_str = GetParam("nonce_str");
                string extra = GetParam("extra");
                string isTest = GetParam("isTest");
                string appId = GetParam("appId");
                string product_id = GetParam("product_id");
                string order_id = GetParam("order_id");
                string transaction_id = GetParam("transaction_id");
                string price = GetParam("cash");
                string uid = GetParam("uid");
                string game_uid = GetParam("game_uid");
                string payResult = GetParam("result");
                //UAHelper.WriteLog("bear Charge",
                //   string.Format(
                //       @"appId={0}&cash={1}&extra={2}&game_uid={3}&isTest={4}&nonce_str={5}&order_id={6}&product_id={7}&result={8}&transaction_id={9}&uid={10}||{11}"
                //       , appId, price, extra, game_uid, isTest, nonce_str, order_id, product_id, payResult, transaction_id, uid,
                //        sign));
                if (string.IsNullOrEmpty(sign)
                    || string.IsNullOrEmpty(nonce_str)
                    || string.IsNullOrEmpty(extra)
                    || string.IsNullOrEmpty(payResult)
                    || string.IsNullOrEmpty(game_uid)
                    || string.IsNullOrEmpty(transaction_id)
                    || string.IsNullOrEmpty(price)
                    || string.IsNullOrEmpty(sign)
                    || string.IsNullOrEmpty(uid)
                    )
                {
                    return "{\"ret\":\"fail\",\"msg\":\"参数为空\"}";
                }
                if (payResult.Trim().ToLower() != "success")
                {
                    return "{\"ret\":\"fail\",\"msg\":\"没有付款成功\"}";
                }
             
               

                var channelAliasEntity = UAFactory.Instance.GetPlatform(platFormKey);
                if (channelAliasEntity == null)
                {
                    return "{\"ret\":\"fail\",\"msg\":\"channelAlias错误\"}";
                }
                string cryptKey = channelAliasEntity.ChargeKey;
               

                string signParam =
                    CryptHelper.GetMD5(string.Format(@"appId={0}&cash={1}&extra={2}&game_uid={3}&isTest={4}&nonce_str={5}&order_id={6}&product_id={7}&result={8}&transaction_id={9}&uid={10}{11}"
                    , appId, price, extra,game_uid, isTest, nonce_str,order_id, product_id, payResult, transaction_id, uid,cryptKey));
                if (sign != signParam)
                {
                    return "{\"ret\":\"fail\",\"msg\":\"sign错误\"}";
                }
                var ary = extra.Split('|');
                var server = "";
                var orderId = "";
                if (ary.Length > 1)
                {
                    orderId = ary[0];
                    server = ary[1];
                }
                decimal cash = ConvertHelper.ConvertToDecimal(price, 0) / 100; //价格单位是  分
                
                var zoneName = ShareUtil.ZoneName;
                var list = NbManagerMgr.GetByAccount(uid, zoneName);
                var manager = new NbManagerEntity();
                if (list.Count > 0)
                    manager = list[0];
                var result = WebServerHandler.BuyPointShipments(platFormKey, server,""+ manager.Idx,
                    orderId, order_id, cash, ConvertHelper.ConvertToInt(product_id));
                if (result == 0)
                    return "OK";
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
                SystemlogMgr.Error("bear Charge", ex);
                return "{\"ret\":\"fail\",\"msg\":\"系统内部错误2\"}";
            }

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
                                  + "&gameNum=" + (int)A8csdkEnum.csdkId + "&channelAlias=" + channelAlias +
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

            var channelAliasEntity = UAFactory.Instance.GetPlatform("" + A8csdkEnum.h5_a8);
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

                var channelAliasEntity = UAFactory.Instance.GetPlatform("" + A8csdkEnum.h5_a8);
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

                var manager = WebServerHandler.IsRegist("" + A8csdkEnum.h5_a8, serverid, openId);

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
            string accessToken = GetParam("accessToken");

               var channelAliasEntity = UAFactory.Instance.GetPlatform(platFormKey);
                if (channelAliasEntity == null)
                {
                     return;
                }

                string userUrl = channelAliasEntity.UserActionUrl + "&accessToken=" + accessToken;
            var result =UAHelper.HttpGet(userUrl);
            //{"code":0,"res":{"openid":"fd0cbdc745910707bdb8fd038296e724","name":"\u8bf8\u845b\u6653\u535a","sex":1,"face":"","city":""},"time":1.1,"sys_time":1468577559}
            var resp = new BearId();
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                resp = serializer.Deserialize<BearId>(result);
            }
            catch (Exception e)
            {
                UAHelper.WriteLog("Dologin", "解析json失败|" + result + "|" + accessToken);
                return;
            }
           

            long sys_time = resp.Sys_time;
            string openid =resp.Res.Openid;
            string name = resp.Res.Name;
            int sex = resp.Res.Sex;
            string face = resp.Res.Face;
            string city = resp.Res.City;
          


            HttpContext.Current.Response.Redirect(
                     string.Format(
                         "Index.aspx?pf={0}&openid={1}&name={2}&sex={3}&face={4}&city={5}&ck=default&appId={6}",
                          platFormKey, openid, name, sex, face, city,gameAppId));
                    HttpContext.Current.Response.End();
                
              
                //serverId = "1";
                //var str = StartGame(openid, state, serverId, nowTimestamp, pf, sessionId, sign, jsNeed, nickName);
                //LoginResponse(str, pf, true);
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
                SystemlogMgr.Error("bear dologin", ex);
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
            string name = GetParam("name");
            string sex = GetParam("sex");
            string face = GetParam("face");
            string city = GetParam("city");
            string isSubscribe = GetParam("isSubscribe");
            string serverId = GetParam("serverId");
            string appId = GetParam("appId");

            if (serverId == "default")
            {
                HttpContext.Current.Response.Redirect(
                    string.Format(
                        "Index.aspx?appId={0}&pf={1}&openid={2}&name={3}&sex={4}&face={5}&city={6}&isSubscribe={7}&ck=default",
                        appId, platFormKey, openid, name, sex, face, city, isSubscribe));
                HttpContext.Current.Response.End();
            }
            else
            {
                var str = StartGameNew(openid, "1", serverId, platFormKey, "1", "1", name);

                try
                {

                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("bearLogin", ex.Message);

                }

                return (int)str;
            }
            return (int)MessageCode.Success;
        }



        
        private MessageCode StartGameNew(string openid, string state, string serverId, string pf,
          string sessionId, string jsNeed, string nickName)
        {
            try
            {
                WebServerHandler.SetStartGameEntity(ShareUtil.PlatformCode, serverId, openid, state, serverId, pf, sessionId, jsNeed, nickName);

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
            string str = OtherOne();
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

                //if (sign != signParam)
                //{
                //    UAHelper.WriteLog("ret", "sign错误");
                //    return UAErrorCode.ErrCheckSign;
                //}

                var manager = WebServerHandler.IsRegist(platFormKey, serverid, openId);

                if (manager != null && !string.IsNullOrEmpty(manager.Name))
                    Response.Write("{\"code\":0,\"uname\":\"" + openId + "\",\"sword\":\"" + manager.Kpi +
                                   "\",\"uid\":\"" + openId + "\",\"rolename\":\"" + manager.Name + "\"" +
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
                    Response.Write("{\"code\":5,\"data\":[]}");
                    break;
                case UAErrorCode.ErrOther:
                    Response.Write("{\"code\":99,\"data\":[]}");
                    break;
                case UAErrorCode.ErrTimeout:
                    Response.Write("{\"code\":6,\"data\":[]}");
                    break;
                case UAErrorCode.ErrPlatform:
                    Response.Write("{\"code\":3,\"data\":[]}");
                    break;
                case UAErrorCode.ErrCheckSign:
                    Response.Write("{\"code\":2,\"data\":[]}");
                    break;
                case UAErrorCode.ErrNoUser:
                    Response.Write("{\"code\":8,\"data\":[]}");
                    break;
                default:
                    Response.Write(str);
                    break;
            }
            Response.End();
        }
        public string IsRegistByNameList()
        {
            string data = GetParam("data");
            string serverid = GetParam("serverId");
            string time = GetParam("time");
            string sign = GetParam("sign");
            if (string.IsNullOrEmpty(data) ||
                string.IsNullOrEmpty(serverid) ||
                string.IsNullOrEmpty(time) ||
                string.IsNullOrEmpty(sign))
            {
                return UAErrorCode.ErrDataOP;
            }

            var key = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.H5_A8DataKey);
            var appSign = CryptHelper.GetMD5(data + serverid + time + key).ToLower();
            if (sign != appSign)
                return UAErrorCode.ErrCheckSign;
            var managerList = WebServerHandler.IsRegistByNameList(platFormKey, serverid, data);
            if (managerList == null || managerList.Length <= 0 || string.IsNullOrEmpty(managerList[0].Name))
                return UAErrorCode.ErrNoUser;
            string str = "";
            foreach (var manager in managerList)
            {
                var rowTime = manager.RowTime;
                var registTime = ShareUtil.GetTimeTick(rowTime);
                str += "{\"name\":\"" + manager.Name + "\",\"serverId\":" + serverid + ",\"gamelevel\":" + manager.Level +
                ",\"registtime\":" + registTime + "},";
            }
            string r = str.Substring(0, str.Length - 1);
            return "{\"code\":0,\"data\":[" + r + "]}";
        }
        /// <summary>
        /// 渠道发送分享物品
        /// </summary>
        /// <returns></returns>
        public string SendItemByShare()
        {
            try
            {
                var name = GetParam("name");
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

                var t = ConvertHelper.ConvertToInt(type);
                var code = WebServerHandler.SendItemByShare(platFormKey, serverid, name, t);
                if (code == (int)MessageCode.NbParameterError)
                    return UAErrorCode.ErrDataOP;
                if (code == (int)MessageCode.LoginNoUser)
                    return UAErrorCode.ErrNoManager;
                if (code == (int)MessageCode.Exception)
                    return UAErrorCode.ErrException;
                if (code == (int)MessageCode.TourPassPrizeHasReceive)
                    return UAErrorCode.ErrRepeatOrder;

                return "" + code;
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
                    Response.Write("{\"code\":5}");
                    break;
                case UAErrorCode.ErrOther:
                    Response.Write("{\"code\":99}");
                    break;
                case UAErrorCode.ErrTimeout:
                    Response.Write("{\"code\":6}");
                    break;
                case UAErrorCode.ErrPlatform:
                    Response.Write("{\"code\":3}");
                    break;
                case UAErrorCode.ErrCheckSign:
                    Response.Write("{\"code\":2}");
                    break;
                case UAErrorCode.ErrNoUser:
                    Response.Write("{\"code\":8}");
                    break;
                case UAErrorCode.ErrRepeatOrder:
                    Response.Write("{\"code\":4}");
                    break;
                default:
                    Response.Write("{\"code\":"+str+"}");
                    break;
            }
            Response.End();
        }
    }

    public class BearId
    {
        //{"code":0,"res":{"openid":"fd0cbdc745910707bdb8fd038296e724","name":"\u8bf8\u845b\u6653\u535a","sex":1,"face":"","city":""},"time":1,"sys_time":1468577559}
        public int Code { get; set; }
        public Res Res { get; set; }

        public long Sys_time { get; set; }

        public double Time { get; set; }
       
    }

    public class Res
    {
        public  string Openid { get; set; }
        public string Name { get; set; }
        public int Sex { get; set; }
        public string Face  { get; set; }
        public string City  { get; set; }
    }

}
