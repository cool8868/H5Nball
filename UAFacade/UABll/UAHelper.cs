using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Security;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Share;
using Games.NBall.ServiceContract.Client;
using Games.NBall.WebServerFacade;

namespace Games.NBall.UAFacade
{
    public class UAHelper
    {
        public static string GetZoneNameByPlatform(string platformCode, string platformZoneName)
        {
            var zone = CacheFactory.FunctionAppCache.GetZoneByPlatform(platformCode, platformZoneName);
            if (zone != null)
                return zone.ZoneName;
            else
            {
                return "";
            }
        }

        public static string CheckActive(string serverId, string account)
        {
            string returnCode = "";
            try
            {
                var returnVaule = WebServerHandler.CheckActive(serverId, account);
                if (returnVaule != 0)
                {
                    WriteLog("UAHelper CheckActive", returnVaule.ToString());
                    return UAErrorCode.ErrNoManager;
                }
                else
                {
                    return UAErrorCode.ErrOK;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("UAHelper CheckActive", ex);
                returnCode = UAErrorCode.ErrOther;
            }
            return returnCode;
        }

        public static string CheckActive(string platformCode, string platformZoneCode, string account)
        {
            string returnCode = "";
            try
            {
                var returnVaule = WebServerHandler.CheckActive(platformCode, platformZoneCode, account);
                if (returnVaule != 0)
                {
                    WriteLog("UAHelper CheckActive", returnVaule.ToString());
                    return UAErrorCode.ErrNoManager;
                }
                else
                {
                    return UAErrorCode.ErrOK;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("UAHelper CheckActive", ex);
                returnCode = UAErrorCode.ErrOther;
            }
            return returnCode;
        }

        public static string SaveChargeData(string factoryCode, string serverId, string account, int money, int point, string orderId)
        {
            //string returnCode = "";
            //try
            //{
            //    // 5200-充值成功;5201-相同订单号已存在; 5202-更新帐户表失败,-1，异常
            //    var returnVaule = WebServerHandler.ChargePlatform(factoryCode, serverId, account, EnumChargeSourceType.System, money, point, 0, orderId ,"");
            //    switch (returnVaule)
            //    {
            //        case 0:
            //            returnCode = UAErrorCode.ErrOK;
            //            break;
            //        case 5201:
            //            returnCode = UAErrorCode.ErrRepeatOrder;
            //            break;
            //        case 5202:
            //            returnCode = UAErrorCode.ErrDataOP;
            //            break;
            //        case 5210:
            //            returnCode = UAErrorCode.ErrNoManager;
            //            break;
            //        default:
            //            WriteLog("UAHelper SaveChargeData", returnVaule.ToString());
            //            returnCode = UAErrorCode.ErrOther;
            //            break;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    SystemlogMgr.Error("SaveChargeData", ex);
            //    returnCode = UAErrorCode.ErrOther;
            //}
            //return returnCode;
            return null;
        }

        public static string SaveLogindata(string account)
        {
            return SaveLogindata(account, "");
        }

        public static string SaveLogindata(string account, string platformCode, string extraData = "", string kgext ="")
        {
            string returnCode = "";
            try
            {
                ManagerClient reader = new ManagerClient();
                var user = reader.GetUserByAccount(account, GetRealIP());
               
                if (user != null)
                {
                    var sessionId = ShareUtil.GenerateComb().ToString();
                    SetFormsAuthentication(account, Guid.Empty, "", 1, sessionId);
                    //转页
                    
                    returnCode = UAErrorCode.ErrOK;
                }
                else
                {
                    
                    returnCode = UAErrorCode.ErrNoUser;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("UAHelper SaveLogindata", ex);
                returnCode = UAErrorCode.ErrOther;
            }
            return returnCode;
        }

        public static string SetFormsAuthentication(string account, Guid managerId, string playerName, int area,string sessionId, string kgext = "")
        {
            //写入登录信息
            UserAccountEntity accountEntity = new UserAccountEntity();
            accountEntity.Account = account;
            accountEntity.ManagerId = managerId;
            accountEntity.Name = playerName;
            accountEntity.Area = area;
            accountEntity.SessionId = sessionId;
            return SetFormsAuthentication(accountEntity);
        }

        /// <summary>
        /// FormsAuthentication
        /// </summary>
        /// <param name="userAccountEntity">用户信息</param>
        public static string SetFormsAuthentication(UserAccountEntity userAccountEntity)
        {
            Int32 timeout = 14400;
            string userData = userAccountEntity.ToString();
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, userAccountEntity.Account, DateTime.Now, DateTime.Now.AddMinutes(timeout), false,
                                              userData, FormsAuthentication.FormsCookiePath);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName);
            cookie.Value = FormsAuthentication.Encrypt(ticket);
            cookie.Expires = ticket.Expiration;
            HttpContext.Current.Response.Cookies.Add(cookie);

            HttpContext.Current.User =
                new System.Security.Principal.GenericPrincipal(
                    new System.Security.Principal.GenericIdentity(userAccountEntity.Account), new string[0]);
            return cookie.Value;
        }

        public static void doRedirect(string platForm, string redirectType)
        {
            string url = UAFactory.Instance.GetRedirectURL(platForm, redirectType);
            if (string.IsNullOrEmpty(url))
            {
                HttpContext.Current.Response.Write("{\"Code\":-20000,\"Message\":\"缺少跳转地址配置\"}");
                HttpContext.Current.Response.End();
            }
            else
            {
                HttpContext.Current.Response.Redirect(url);
                HttpContext.Current.Response.End();
            }
        }

        public static void ReturnError(string platForm, string errorCode)
        {
            string url = UAFactory.Instance.GetRedirectURL(platForm, "1");//官网地址
            if (string.IsNullOrEmpty(url))
            {
                url = UAFactory.Instance.ErrorPage;
            }

            HttpContext.Current.Response.Redirect(url + "?code=" + errorCode);
            HttpContext.Current.Response.End();
            return;
        }

        public static void WriteError(string errorCode)
        {
            HttpContext.Current.Response.Write(errorCode);
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 将Unix时间戳转换为DateTime类型时间
        /// </summary>
        /// <param name="d">double 型数字</param>
        /// <returns>DateTime</returns>
        public static System.DateTime ConvertDateTimeFromUnix(double d)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            System.DateTime time = startTime.AddSeconds(d);
            return time;
        }


        /// <summary>
        /// Gets the real IP.
        /// </summary>
        /// <returns></returns>
        public static string GetRealIP()
        {
            string ip = string.Empty;
            try
            {
                ip = HttpContext.Current.Request.Headers["Cdn-Src-Ip"];

                if (string.IsNullOrEmpty(ip))
                {
                    ip = HttpContext.Current.Request.Headers["X-Forwarded-For"];
                }
                if (string.IsNullOrEmpty(ip))
                {
                    ip = HttpContext.Current.Request.UserHostAddress;
                }
            }
            catch (Exception e)
            {
                SystemlogMgr.Error("GetRealIP", e);
            }
            return ip;
            ////ip = HttpContext.Current.Request.UserHostAddress;
            //HttpRequest request = HttpContext.Current.Request;
            //if (request.ServerVariables["HTTP_VIA"] != null &&
            //    request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            //{
            //    var forwarded = request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            //    var ss = forwarded.Split(',');
            //    if (ss != null && ss.Length > 0)
            //    {
            //        return ss[0].Trim();
            //    }
            //}
            //ip = request.UserHostAddress;
            return ip;
        }

        public static void WriteLog(string title, string returnCode)
        {
            if (!UAFactory.Instance.OpenUALog)
                return;
            try
            {
                SystemlogMgr.Info(title, string.Format("{2} return code:{0},Source:{1}", returnCode,
                                  HttpContext.Current.Request.Url.PathAndQuery, title));
                //LogHelper.Insert(
                //    string.Format("{2} return code:{0},Source:{1}", returnCode,
                //                  HttpContext.Current.Request.Url.PathAndQuery, title), LogType.Info);
            }
            catch(Exception e)
            { }
        }

        public static void WriteChargeLog(string title, string returnCode)
        {
            try
            {
                SystemlogMgr.Info(title, string.Format("{2} return code:{0},Source:{1}", returnCode,
                                  HttpContext.Current.Request.Url.PathAndQuery, title));
                //LogHelper.Insert(
                //    string.Format("{2} return code:{0},Source:{1}", returnCode,
                //                  HttpContext.Current.Request.Url.PathAndQuery, title), LogType.Info);
            }
            catch
            { }
        }

        /// <summary>
        /// 获取当前UserAccount信息
        /// </summary>
        /// <returns>UserAccountEntity</returns>
        public static UserAccountEntity GetCurrentUserAccount()
        {
            if (HttpContext.Current.User.Identity != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                UserAccountEntity userAccountEntity = Parse(ticket.UserData);
                return userAccountEntity;
            }
            return null;
        }

        /// <summary>
        /// 字符串信息解析为UserAccount信息
        /// </summary>
        /// <param name="data">字符串数据</param>
        /// <returns>UserAccountEntity</returns>
        public static UserAccountEntity Parse(string data)
        {
            string[] userData = data.Split("&".ToCharArray());
            if (userData.Length == 5)
            {
                UserAccountEntity userAccountEntity = new UserAccountEntity();

                userAccountEntity.Account = userData[0];
                userAccountEntity.ManagerId = new Guid(userData[1]);
                userAccountEntity.Name = userData[2];
                userAccountEntity.Area = int.Parse(userData[3]);
                userAccountEntity.PlatformCode = userData[4];
                return userAccountEntity;
            }
            else if (userData.Length == 6)
            {
                UserAccountEntity userAccountEntity = new UserAccountEntity();

                userAccountEntity.Account = userData[0];
                userAccountEntity.ManagerId = new Guid(userData[1]);
                userAccountEntity.Name = userData[2];
                userAccountEntity.Area = int.Parse(userData[3]);
                userAccountEntity.PlatformCode = userData[4];
                userAccountEntity.ExtraData = userData[5];
                return userAccountEntity;
            }
            else if (userData.Length == 4)
            {
                UserAccountEntity userAccountEntity = new UserAccountEntity();

                userAccountEntity.Account = userData[0];
                userAccountEntity.ManagerId = new Guid(userData[1]);
                userAccountEntity.Name = userData[2];
                userAccountEntity.Area = int.Parse(userData[3]);
                return userAccountEntity;
            }
            return null;
        }


        public static string HttpPost(string url, string postDataStr)
        {
            try
            {
                HttpWebRequest request =
                    (HttpWebRequest) WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
                Stream myRequestStream = request.GetRequestStream();
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream);
                myStreamWriter.Write(postDataStr);
                myStreamWriter.Close();

                HttpWebResponse response = (HttpWebResponse) request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                var result = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                return result;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("HttpPost请求", ex);
                return "";
            }
        }

        public static string HttpGet(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";
                HttpWebResponse response = (HttpWebResponse) request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                return retString;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("HttpGet请求", ex);
                return "";
            }
        }

        public static string StrToUtf8(string str)
        {
            var r ="";
            try
            {
                r = HttpUtility.UrlEncode(str);
            }
            catch (Exception)
            {

               
            }
            return r;
        }
     
    }
}