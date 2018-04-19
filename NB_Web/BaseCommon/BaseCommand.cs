using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Games.NBall.Entity.Enums;
using Games.NBall.NB_Web.Helper;
using Games.NBall.ServiceContract.Client;

namespace Games.NBall.NB_Web.BaseCommon
{
    public class BaseCommand : BaseUser
    {
        /// <summary>
        /// 只检查账号，不检查登录.
        /// </summary>
        /// <returns></returns>
        public bool ValidatorAccountOnly()
        {
            if (UserAccount == null || string.IsNullOrEmpty(UserAccount.Account))
            {
                OutputHelper.Output(MessageCode.LoginNoUser);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 校验，如不通过直接输出错误信息.
        /// </summary>
        public bool Validator()
        {
            var messageCode = LoginValidate();
            if (messageCode != MessageCode.Success)
            {
                OutputHelper.Output(messageCode);
                return false;
            }
            return true;
        }



        private MessageCode LoginValidate()
        {
            // var sessionId = OnlineMgr.GetSessionId4PC();
            string sessionId = "";
            BaseUser user = new BaseUser();
            if (user.UserAccount.SessionId == "")
                sessionId = OnlineMgr.GetSessionId4PC();
            else
                sessionId = user.UserAccount.SessionId;
            //踢线检查
            bool bumpFlag;
            if (OnlineMgr.CheckAndKickSession(sessionId, true, out bumpFlag))
            {
                return bumpFlag ? MessageCode.LoginOnlineBump : MessageCode.LoginOnlineKick;

            }
            //登录检查
            if (!user.IsLogin)
            {
                return MessageCode.LoginNoLogin;
            }
            else
            {
                OnlineMgr.ForceSession(user.UserAccount.ManagerId, sessionId);
            }
            return MessageCode.Success;
        }

        protected bool HasTask
        {
            get { return GetParamBool("ht"); }
        }

        protected int PageSize
        {
            get
            {
                var ps = GetParamInt("ps");
                if (ps < 1)
                    ps = 1;
                if (ps > 50)
                    ps = 50;
                return ps;
            }
        }

        protected int PageIndex
        {
            get
            {
                var pi = GetParamInt("pi");
                if (pi < 1)
                    pi = 1;
                return pi;
            }
        }

        protected int GuideTaskRecordId
        {
            get { return GetParamInt("gtri"); }
        }

        protected int RecordId
        {
            get { return GetParamInt("ri"); }
        }

        protected int LuckyCode
        {
            get { return GetParamInt("lc"); }
        }

        protected Guid LuckyId
        {
            get { return GetParamGuid("li"); }
        }

        #region HttpContext

        private HttpRequest _request;

        /// <summary>
        /// Gets the request.
        /// </summary>
        /// <value>The request.</value>
        public HttpRequest Request
        {
            get
            {
                if (_request == null)
                {
                    _request = Context.Request;
                }
                return _request;
            }
        }

        private HttpResponse _response;

        /// <summary>
        /// Gets the response.
        /// </summary>
        /// <value>The response.</value>
        public HttpResponse Response
        {
            get
            {
                if (_response == null)
                {
                    _response = Context.Response;
                }
                return _response;
            }
        }

        private HttpContext _context;

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        public HttpContext Context
        {
            get
            {
                if (_context == null)
                {
                    _context = HttpContext.Current;
                }
                return _context;
            }
        }

        private HttpApplicationState _application;

        /// <summary>
        /// Gets the application.
        /// </summary>
        /// <value>The application.</value>
        public HttpApplicationState Application
        {
            get
            {
                if (_application == null)
                {
                    _application = Context.Application;
                }
                return _application;
            }
        }

        #endregion

        #region GetParam

        /// <summary>
        /// 获取表单参数,默认返回 string.Empty.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public string GetParam(string key)
        {
            return GetParam(key, string.Empty);
        }

        /// <summary>
        /// 获取表单参数.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public string GetParam(string key, string defaultValue)
        {
            if (!string.IsNullOrEmpty(Request[key]))
            {
                return Context.Server.UrlDecode(Request[key]);
            }

            return defaultValue;
        }

        public string GetParamNOUrlDecode(string key)
        {
            if (!string.IsNullOrEmpty(Request[key]))
            {
                try
                {
                    var r = Request[key];
                    return r;
                }
                catch (Exception)
                {

                }

            }
            return "";
        }

        /// <summary>
        /// 获取表单参数并转换为int.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public int GetParamInt(string key, int defaultValue)
        {
            string param = GetParam(key, "");
            return Common.ConvertHelper.ConvertToInt(param, defaultValue);
        }

        public long GetParamLong(string key, long defaultValue = 0)
        {
            string param = GetParam(key, "");
            return Common.ConvertHelper.ConvertToLong(param, defaultValue);
        }

        /// <summary>
        /// 获取表单参数并转换为decimal.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public decimal GetParamDecimal(string key, decimal defaultValue)
        {
            string param = GetParam(key, "");
            return Common.ConvertHelper.ConvertToDecimal(param, defaultValue);
        }

        /// <summary>
        /// 获取表单参数并转换为int，默认返回0.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public int GetParamInt(string key)
        {
            return GetParamInt(key, 0);
        }

        public decimal GetParamDecimal(string key)
        {
            return GetParamDecimal(key, 0.00m);
        }

        /// <summary>
        /// 1=true；else = false.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool GetParamBool(string key)
        {
            string rValue = GetParam(key);
            if (rValue == "1" || rValue == "true" || rValue == "True" || rValue == "TRUE")
                return true;
            else
            {
                return false;
            }
        }

        public Guid GetParamGuid(string key)
        {
            Guid rValue;
            Guid.TryParse(GetParam(key, Guid.Empty.ToString()), out rValue);
            return rValue;
        }

        #endregion

        #region CheckParam

        /// <summary>
        /// 验证参数是否为空
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        protected bool CheckParam(Guid param)
        {
            if (param == Guid.Empty)
            {
                OutputHelper.OutputParameterError();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 验证参数是否为空
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        protected bool CheckParam(string param)
        {
            if (string.IsNullOrEmpty(param))
            {
                OutputHelper.OutputParameterError();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 验证参数是否为空
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        protected bool CheckParam(int param)
        {
            if (param <= 0)
            {
                OutputHelper.OutputParameterError();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 验证参数是否为空
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        protected bool CheckParam(long param)
        {
            if (param <= 0)
            {
                OutputHelper.OutputParameterError();
                return false;
            }
            return true;
        }

        protected bool CheckRecordId()
        {
            if (RecordId <= 0)
            {
                OutputHelper.OutputParameterError();
                return false;
            }
            return true;
        }

        protected bool CheckParam2(int param)
        {
            if (param < 0)
            {
                OutputHelper.OutputParameterError();
                return false;
            }
            return true;
        }

        #endregion

        /// <summary>
        /// Gets the ip.
        /// </summary>
        /// <returns></returns>
        public string GetIp()
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
            catch
            {
                
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
        }
    }
}