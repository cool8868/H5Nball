
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
    public class UA_qunhei : UAAdapter
    {
        private readonly string platFormKey = "h5_qunhei";

        public override void doCharge()
        {
            var result = Charge();
            Response.Write(result);

        }

        private string Charge()
        {
            try
            {
                string serverid = GetParam("serverid");
                string orderno = GetParam("orderno");
                string username = GetParam("username");
                string addgold = GetParam("addgold");
                string rmb = GetParam("rmb");
                string paytime = GetParam("paytime");
                string ext = GetParam("ext");
                string sign = GetParam("sign");

                if (string.IsNullOrEmpty(sign)
                    || string.IsNullOrEmpty(serverid)
                    || string.IsNullOrEmpty(orderno)
                    || string.IsNullOrEmpty(username)
                    || string.IsNullOrEmpty(addgold)
                    || string.IsNullOrEmpty(rmb)
                    || string.IsNullOrEmpty(paytime)
                    || string.IsNullOrEmpty(ext)
                    || string.IsNullOrEmpty(sign) )
                {
                    return "-4";
                }

                //Sign = Md5(orderno+username + serverid+addgold + rmb+paytime+ext+key).toLowerCase();
                //long time1 = ConvertHelper.ConvertToLong(paytime, 0);
                //DateTime sourceTime = ShareUtil.GetTime(time1);
                //DateTime nowTime = DateTime.Now;
                ////检查时间是否过期
                //if (sourceTime.AddSeconds(UAFactory.Instance.Timeout30min) < nowTime ||
                //    sourceTime.AddSeconds(-UAFactory.Instance.Timeout30min) > nowTime)
                //{
                //    //记录详细的错误日志
                //    return "{\"ret\":\"fail\",\"msg\":\"响应超时\"}";
                //}

                var channelAliasEntity = UAFactory.Instance.GetPlatform(platFormKey);
                if (channelAliasEntity == null)
                {
                    return "-4";
                }

                string cryptKey = channelAliasEntity.ChargeKey;
                string signParam =
                    CryptHelper.GetMD5(orderno + username + serverid + addgold + rmb + paytime + ext + cryptKey).ToLower();
                if (sign != signParam)
                {
                    return "-4";
                }

                decimal cash = ConvertHelper.ConvertToDecimal(rmb, 0);
                var exts = ext.Split('_');
                if(exts.Length<3)
                    return "-4";
                int mallCode = ConvertHelper.ConvertToInt(exts[0]);
                string sId = exts[1];
                string orderId = exts[2];
                var result = WebServerHandler.BuyPointShipments(platFormKey, sId, username,
                    orderId, orderno,
                    cash, mallCode);
                if (result == 0)
                    return "1";
                SystemlogMgr.Error("充值", "code:" + result);
                switch (result)
                {
                    case 2062:
                        return "-2";
                    case 5211:
                        return "-1";
                    case 151:
                        return "-1";
                    default:
                        return "-7";
                }

            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("a8 dologinnew", ex);
                return "-7";
            }

        }

        public override void doLogin()
        {
            
            string username = GetParamNOUrlDecode("username");
            string serverid = GetParam("serverid");
            string time = GetParam("time");
            string isadult = GetParam("isadult");//防沉迷标识(1是成年，0是未成年)
            string uimg = GetParam("uimg");//玩家头像
            string nname = GetParam("nname");//玩家昵称
            string unid = GetParam("unid");//玩家来源
            string flag = GetParam("flag");//验证签名，md5(username+serverid+isadult+time+key)

            var result = LoginCheck(username, serverid, time, isadult, flag);
            if (result != "0")
                LoginResponse(result, false);
            else
            {
                if (serverid == "1")
                {
                    HttpContext.Current.Response.Redirect(
                     string.Format(
                         "Index.aspx?pf=qunhei&ck=default&username={0}&serverid={1}&time={2}&isadult={3}&uimg={4}&nname={5}&unid={6}&flag={7}&isSelectZone=1",
                         username, serverid, time, isadult, uimg, nname, unid, flag));
                    HttpContext.Current.Response.End();
                }
                else
                {
                    //登录
                    result = Login(username);
                    LoginResponse(result, true);
                }
            }
        }

        private string Login(string userName)
        {
            try
            {
                this.SetPlatSession();
                return UAHelper.SaveLogindata(userName);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("h5_a8 dologin", ex);
                return UAErrorCode.ErrOther;
            }
        }

        private string LoginCheck(string ousername, string serverid, string time, string isadult, string flag)
        {
            if (string.IsNullOrEmpty(ousername)
                || string.IsNullOrEmpty(serverid)
                || string.IsNullOrEmpty(time)
                || string.IsNullOrEmpty(isadult)
                || string.IsNullOrEmpty(flag))
            {
                return UAErrorCode.ErrDataOP;
            }

            var channelAliasEntity = UAFactory.Instance.GetPlatform(platFormKey);
            if (channelAliasEntity == null)
            {
                return UAErrorCode.ErrPlatform;
            }

            //long time1 = ConvertHelper.ConvertToLong(time, 0);
            //DateTime sourceTime = ShareUtil.GetTime(time1);
            //DateTime nowTime = DateTime.Now;
            ////检查时间是否过期
            //if (sourceTime.AddSeconds(UAFactory.Instance.Timeout30min) < nowTime ||
            //    sourceTime.AddSeconds(-UAFactory.Instance.Timeout30min) > nowTime)
            //{
            //    //记录详细的错误日志
            //    return "{\"ret\":\"fail\",\"msg\":\"响应超时\"}";
            //}

            string cryptKey = channelAliasEntity.LoginKey;
            string signParam = "";
            string signserverId = "1";
            signParam = CryptHelper.GetMD5(ousername + signserverId + isadult + time + cryptKey).ToLower();

            if (flag != signParam)
            {
                return UAErrorCode.ErrCheckSign;
            }
            return "0";
        }

        private void LoginResponse(string str,  bool isRediect)
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
                default:
                    Response.Write(str);
                    break;
            }
            if (isRediect)
                HttpContext.Current.Response.Redirect("Index.aspx?ck=" +
                                                      Response.Cookies[FormsAuthentication.FormsCookieName].Value);
            Response.End();
        }

        /// <summary>
        /// 最新登录
        /// </summary>
        /// <returns></returns>
        public override int doLoginNew()
        {
            string username = GetParamNOUrlDecode("username");
            string serverid = GetParam("serverid");
            string time = GetParam("time");
            string isadult = GetParam("isadult"); //防沉迷标识(1是成年，0是未成年)
            string uimg = GetParam("uimg"); //玩家头像
            string nname = GetParam("nname"); //玩家昵称
            string unid = GetParam("unid"); //玩家来源
            string flag = GetParam("flag"); //验证签名，md5(username+serverid+isadult+time+key)

            var result = LoginCheck(username, serverid, time, isadult,flag);
            if (result != "0")
                return ConvertHelper.ConvertToInt(result);
            if (serverid == "default")
            {
                HttpContext.Current.Response.Redirect(
                    string.Format(
                        "Index.aspx?pf=qunhei&ck=default&username={0}&serverid={1}&time={2}&isadult={3}&uimg={4}&nname={5}&unid={6}&flag={7}&isSelectZone=1",
                        username, serverid, time, isadult, uimg, nname, unid, flag));
                HttpContext.Current.Response.End();
            }
            return (int) MessageCode.Success;
        }


        protected override string ColUid
        {
            get { return "username"; }
        }

        public override void doCheckActive()
        {

        }

        public override void doRedirect(string userName, string redirectType)
        {

        }

        public override void doLogout()
        {

        }

        public override void doPowerValue()
        {


        }

        public override void doOtherOne()
        {
            
        }
      
        public override void doOtherThree()
        {
            
        }

        public override void doOtherTwe()
        {
            
        }
    }

}
