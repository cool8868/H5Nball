using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Activity;
using Games.NBall.Entity.Enums;
using Games.NBall.UAFacade;

namespace Games.NBall.UAFacade
{
    public class UA_gov : UAAdapter
    {
        private readonly string platFormKey = "Gov";
        protected override string ColUid
        {
            get { return "username"; }
        }
        #region Login
        public override void doLogin()
        {
            string returnCode = Login();
            string platform = GetParam("platform");
            UAHelper.WriteLog("Login", returnCode);
            if (returnCode == UAErrorCode.ErrOK)
            {
                HttpContext.Current.Response.Redirect("Index.aspx");
                HttpContext.Current.Response.End();
            }
            else
            {
                ShowError(platform,returnCode);
            }
        }

        private string Login()
        {
            try
            {
                string platform = GetParam("platform");
                string uid = GetParam("username");
                string sign = GetParam("sign");
                string time = GetParam("time");

                if (string.IsNullOrEmpty(uid)
                    || string.IsNullOrEmpty(platform)
                    || string.IsNullOrEmpty(sign)
                    || string.IsNullOrEmpty(time))
                {
                    return UAErrorCode.ErrPara;
                }

                DateTime sourceTime;
                if (!DateTime.TryParseExact(time, "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out sourceTime))
                {
                    //记录详细的错误日志
                    return UAErrorCode.ErrPara;
                }
                DateTime nowTime = DateTime.Now;
                //检查时间是否过期
                if (sourceTime.AddSeconds(UAFactory.Instance.Timeout) < nowTime ||
                    sourceTime.AddSeconds(-UAFactory.Instance.Timeout) > nowTime)
                {
                    //记录详细的错误日志
                    return UAErrorCode.ErrTimeout;
                }
                
                var platformEntity = UAFactory.Instance.GetPlatform(platform);
                if (platformEntity == null)
                {
                    return UAErrorCode.ErrPlatform;
                }

                string cryptKey = platformEntity.LoginKey;
                string signParam ="platform=" + platform + "&username=" + uid +  "&time=" + time + "&key=" + cryptKey;
                string calcSignature = CryptHelper.GetSHA1(signParam);
                if (calcSignature != sign)
                {
                    return UAErrorCode.ErrCheckSign;
                }
                this.SetPlatSession();
                string returnCode = UAHelper.SaveLogindata(uid);
                return returnCode;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Gov dologin", ex);
                return UAErrorCode.ErrOther;
            }
        }
        #endregion

        #region encapsulation
        private void ShowError(string platform,string returnCode)
        {
            var page = UAFactory.Instance.GetRedirectURL(platform, "1");
            if(string.IsNullOrEmpty(page))
            {
                page=UAFactory.Instance.PlatformUrl;
            }
            
            HttpContext.Current.Response.Write("<script>alert(\"请重新登陆。\");" + UAFactory.Instance.JumpScript + "=\"" + page + "?code=" + returnCode + "\";</script>");
            HttpContext.Current.Response.End();
        }
        #endregion

        #region Charge
        public override void doCharge()
        {
            UAHelper.WriteChargeLog("Charge Start", "");
            string returnCode = Charge();
            UAHelper.WriteChargeLog("Charge", returnCode);
            UAHelper.WriteError(returnCode);
        }

        string Charge()
        {
            try
            {
                //uid=50469470&order_amount=10&order_id=test1056&server_id=S1&agentid=??&sign=7439fb6fa5f82104c107648e9ac77d76
                string platform = GetParam("platform");
                string uid = GetParam("username");
                string sign = GetParam("sign");
                string time = GetParam("time");
                int money = GetParamInt("money");
                string orderid = GetParam("orderid");
                if (string.IsNullOrEmpty(uid)
                    || string.IsNullOrEmpty(platform)
                    || string.IsNullOrEmpty(sign)
                    || string.IsNullOrEmpty(time)
                    ||money<=0)
                {
                    return UAErrorCode.ErrPara;
                }
                DateTime sourceTime;
                if (!DateTime.TryParseExact(time, "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out sourceTime))
                {
                    //记录详细的错误日志
                    return UAErrorCode.ErrPara;
                }
                DateTime nowTime = DateTime.Now;
                //检查时间是否过期
                if (sourceTime.AddSeconds(UAFactory.Instance.Timeout) < nowTime ||
                    sourceTime.AddSeconds(-UAFactory.Instance.Timeout) > nowTime)
                {
                    //记录详细的错误日志
                    return UAErrorCode.ErrTimeout;
                }

                var platformEntity = UAFactory.Instance.GetPlatform(platform);
                if (platformEntity == null)
                {
                    return UAErrorCode.ErrPlatform;
                }

                string cryptKey = platformEntity.ChargeKey;
                string signParam = "platform=" + platform + "&username=" + uid + "&money=" + money + "&orderid=" +
                                   orderid + "&time=" + time + "&key=" + cryptKey;
                string cryptParam = CryptHelper.GetSHA1(signParam);
                if (sign != cryptParam)
                {
                    return UAErrorCode.ErrCheckSign;
                }
                int point = money * platformEntity.ExchangeRate;
                int cash = money * platformEntity.CashRate;
                return UAHelper.SaveChargeData(platform, UAFactory.Instance.ZoneName, uid, cash, point, orderid);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Gov Charge", ex);
                return UAErrorCode.ErrOther;
            }
            
        }
        #endregion

        public override void doRedirect(string userName, string redirectType)
        {
            //UAHelper.doRedirect(platFormKey, redirectType);
        }

        public override void doOtherOne()
        {
            throw new NotImplementedException();
        }

        public override void doLogout()
        {
            return;
        }

        public override void doCheckActive()
        {
            //uid=28318249&server_id=S1&agentid=??
            var returnCode = CheckActive();
            UAHelper.WriteLog("CheckActive", returnCode);
            if (returnCode == UAErrorCode.ErrOK)
            {
                returnCode = "1";
            }
            else
            {
                returnCode = "0";
            }
            //已经激活过该分区返回1,否则返回0
            UAHelper.WriteError(returnCode);
        }

        private string CheckActive()
        {
            try
            {
                string uid = HttpContext.Current.Request["username"];
                string platform = HttpContext.Current.Request["platform"];

                if (string.IsNullOrEmpty(uid)
                    || string.IsNullOrEmpty(platform))
                {
                    return UAErrorCode.ErrPara;
                }

                var platformEntity = UAFactory.Instance.GetPlatform(platform);
                if (platformEntity == null)
                {
                    return UAErrorCode.ErrPlatform;
                }
                return UAHelper.CheckActive(UAFactory.Instance.ZoneName, uid);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Gov CheckActive", ex);
                return UAErrorCode.ErrOther;
            }
        }

        #region CheckAward
        public void doCheckAward()
        {
            UAHelper.WriteLog("CheckAward", "start");
            var ret = CheckAward();
            UAHelper.WriteLog("CheckAward", "end. ret:" + ret);
            UAHelper.WriteError(ret);
        }

        string CheckAward()
        {
            try
            {

                var openid = GetParam("openid");
                var appid = GetParam("appid");
                
                var pf = GetParam("pf");
                var ts = GetParam("ts");
                var version = GetParam("version");
                var discountid = GetParam("discountid");
                var token = GetParam("token");
                var payitem = GetParam("payitem");
                var billno = GetParam("billno");
                var zoneid = GetParam("zoneid");
                var providetype = GetParam("providetype");

                var sig = GetParam("sig");
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("openid", openid);
                param.Add("appid", appid);
                param.Add("ts", ts);
                param.Add("payitem", payitem);
                param.Add("discountid", discountid);
                param.Add("token", token);
                param.Add("billno", billno);
                param.Add("version", version);
                param.Add("zoneid", zoneid);
                param.Add("providetype", providetype);

                //购买礼包
                var responseCode = ActivityCore.Instance.TxTaskStep(openid, 100, 1, "award", billno,"");
                if (responseCode.Code == 0)
                {
                    return UAErrorCode.ErrOK;
                }
                else
                {
                    UAHelper.WriteError("error");
                    return UAErrorCode.ErrOther;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return UAErrorCode.ErrOther;
            }
        }
        #endregion

        public override void doPowerValue()
        {
            throw new NotImplementedException();
        }

        public override int doLoginNew()
        {
            throw new NotImplementedException();
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
}
