using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Web;
using Games.NBall.Common;
using Games.NBall.WebClient.Plat;
using Games.NBall.Bll.Share;

namespace Games.NBall.UAFacade
{
    public abstract class UAAdapter
    {
        public static UAAdapter GetInstance(string factory)
        {
            if (ShareUtil.IsTx)
                factory = "wbTx";
            string factoryName = "Games.NBall.UAFacade.UA_" + factory;

            UAAdapter instance = null;

            if (factoryName != "")
            {
                Assembly ass = Assembly.Load("Games.NBall.UAFacade");

                Type type = ass.GetType(factoryName);
                if (type != null)
                {
                    instance = (UAAdapter)ass.CreateInstance(factoryName);
                }
                else
                {
                    LogHelper.Insert("create instance fail,factory:"+factoryName,LogType.Error);
                }
            }

            return instance;
        }

        public abstract int doLoginNew();
        public abstract void doLogin();
        public abstract void doPowerValue();
        public abstract void doOtherThree();

        public abstract void doCharge();

        public abstract void doCheckActive();

        public abstract void doRedirect(string userName, string redirectType);
        /// <summary>
        /// 其他渠道读取用户数据使用()
        /// </summary>
        public abstract void doOtherOne();

        /// <summary>
        /// 其他渠道读取用户数据使用(2)
        /// </summary>
        public abstract void doOtherTwe();
        public abstract void doLogout();

        #region PlatSession
        public virtual IPlatSessionProvider SessionProvider
        {
            get { return PlatSessionProvider.Instance; }
        }
        protected abstract string ColUid
        {
            get;
        }
        protected virtual string[] ColArgs
        {
            get { return null; }
        }
        public IPlatSessionData GetPlatSesion(int sessionMode = -1)
        {
            return SessionProvider.LoadSession(ColUid, sessionMode, ColArgs);
        }
        public IPlatSessionData SetPlatSession(int sessionMode = -1)
        {
            return SessionProvider.LoginSession(ColUid, sessionMode, ColArgs);
        }
        #endregion


        //public abstract string GetLoginurl(string username);

        //public abstract string GetChargeurl();

        #region encapsulation
        public void ShowError(string returnCode)
        {
            var page = UAFactory.Instance.PlatformUrl;
            HttpContext.Current.Response.Write("<script>alert(\"请重新登陆。\");" + UAFactory.Instance.JumpScript + "=\"" + page + "?code=" + returnCode + "\";</script>");
            HttpContext.Current.Response.End();
        }
        #endregion

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
        /// Gets the request.
        /// </summary>
        /// <value>The request.</value>
        public HttpRequest Request
        {
            get
            {
                return  Context.Request;
            }
        }

        /// <summary>
        /// Gets the response.
        /// </summary>
        /// <value>The response.</value>
        public HttpResponse Response
        {
            get
            {
                return Context.Response;
            }
        }
        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        public HttpContext Context
        {
            get
            {
                return HttpContext.Current;
            }
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
        /// 获取表单参数.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public string GetParam(string key, string defaultValue)
        {
            if (!string.IsNullOrEmpty(Request[key]))
            {
                if (key.ToLower() == "sig")
                    return Request[key];
                return Context.Server.UrlDecode(Request[key]);
            }

            return defaultValue;
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

        /// <summary>
        /// 获取表单参数并转换为int，默认返回0.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public int GetParamInt(string key)
        {
            return GetParamInt(key, 0);
        }
    }
}