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
    public class UA_wbTx : UAAdapter
    {
        private readonly string platFormKey = "h5_wb";

        /// <summary>
        /// 充值（1）
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
            var channelAliasEntity = UAFactory.Instance.GetPlatform(platFormKey); //枚举参数修改腾讯key
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

       
        public override void doLogin()
        {
            string openid = GetParam("openid");
            string openkey = GetParam("openkey");
            string pf = GetParam("pf");
            string pfkey = GetParam("pfkey");
            var serverId = GetParam("serverid");
            var platform = GetParam("platform");
            if (string.IsNullOrEmpty(pf))
                pf = "wanba_ts";
            openid = UAHelper.StrToUtf8(openid);
            if (string.IsNullOrEmpty(openid)
               || string.IsNullOrEmpty(openkey)
               //|| string.IsNullOrEmpty(platform)
               //|| string.IsNullOrEmpty(pfkey)
                )
            {
                Response.Write("{\"ret\":\"fail\",\"msg\":\"参数不正确\"}");
                Response.End();
            }
            if (string.IsNullOrEmpty(serverId))
            {
                HttpContext.Current.Response.Redirect(
                       string.Format(
                           "Index.aspx?pf={0}&ck=default&openid={1}&openkey={2}&pfkey={3}&serverId={4}&platform={5}", pf, openid, openkey, pfkey, serverId, platform));
                HttpContext.Current.Response.End();
            }

            try
            {
                //登录
                var result = Login(openid);
                LoginResponse(result, pf, true);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("txwb StartGame", ex);
                Response.Write("{\"ret\":\"fail\",\"msg\":\"系统异常" + ex + "\"}");
                Response.End();
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
            string openkey = GetParam("openkey");
            string pf = GetParam("pf");
            string pfkey = GetParam("pfkey");
            var serverId = GetParam("serverid");

            var platform = GetParam("platform");

            if (string.IsNullOrEmpty(pf))
                pf = "wanba_ts";

            openid = UAHelper.StrToUtf8(openid);
            if (string.IsNullOrEmpty(openid)
                || string.IsNullOrEmpty(openkey)
                || string.IsNullOrEmpty(serverId)
                || string.IsNullOrEmpty(pf)
                || string.IsNullOrEmpty(platform))
            {
                return (int) MessageCode.LoginError;
            }
            if (string.IsNullOrEmpty(serverId))
            {
                HttpContext.Current.Response.Redirect(
                    string.Format(
                        "Index.aspx?pf={0}&ck=default&openid={1}&openkey={2}&pfkey={3}&serverId={4}&platform={5}", pf, openid,
                        openkey, pfkey, serverId, platform));
                HttpContext.Current.Response.End();
            }
            return (int) MessageCode.Success;
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
    
    
    


