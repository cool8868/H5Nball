using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Net;
using System.Web;
using Games.NBall.WebClient.Data;
using Games.NBall.WebClient.Util;
using Games.NBall.Common;

namespace Games.NBall.WebClient
{
    public abstract class WebClientBase
    {
        #region Cache
        protected string _baseUri;
        protected IWebArgs _getArgs;
        protected IWebArgs _postArgs;
        #endregion

        #region .ctor
        protected WebClientBase(string uri, IWebArgs getArgs = null, IWebArgs postArgs = null)
        {
            this._baseUri = uri;
            this._getArgs = getArgs;
            this._postArgs = postArgs;
        }
        #endregion

        #region Data
        public IWebArgs QueryArgs
        {
            get
            {
                if (null == _getArgs)
                    _getArgs = CreateCommonArgs();
                return _getArgs;
            }
        }
        public IWebArgs FormArgs
        {
            get
            {
                if (null == _postArgs)
                    _postArgs = CreateCommonArgs();
                return _postArgs;
            }
        }
        public int Timeout
        {
            get;
            set;
        }
        #endregion

        #region Request
        public string WebGet()
        {
            return RequestCore(EnumWebRequestMethod.GET);
        }
        public string WebPost()
        {
            return RequestCore(EnumWebRequestMethod.POST);
        }
        public HttpWebRequest CreateWebGet()
        {
            return CreateRequest(EnumWebRequestMethod.GET);
        }
        public HttpWebRequest CreateWebPost()
        {
            return CreateRequest(EnumWebRequestMethod.POST);
        }
        #endregion

        #region Args
        public void AddQueryArgs(string key, string val)
        {
            this.QueryArgs.SetValue(key, val);
        }
        public void AddFormArgs(string key, string val)
        {
            this.FormArgs.SetValue(key, val);
        }
        #endregion

        #region RequestCore
        protected string RequestCore(EnumWebRequestMethod method)
        {
            int retryTimes = 2;
            string callUrl = string.Empty;
            string errorText = string.Empty;
            do
            {
                try
                {
                    var req = CreateRequest(method);
                    callUrl = req.RequestUri.PathAndQuery;
                    this.PrepareRequest(req);
                    return this.PostResponseText(WebUtil.GetResonseText(req));
                }
                catch (WebException wex)
                {
                    try
                    {
                        var errorResp = wex.Response as HttpWebResponse;
                        if (null != errorResp)
                        {
                            errorText = WebUtil.GetResonseText(errorResp);
                            LogHelper.Insert(string.Format("call:{0}..{1}", callUrl, errorText), LogType.Error);
                        }
                    }
                    catch (Exception wex2)
                    {
                        LogHelper.Insert(wex2);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Insert(ex, string.Format("call:{0}..", callUrl));
                }
            }
            while (--retryTimes > 0);
            return WebErrorCode.ErrorResponse;
        }
        protected HttpWebRequest CreateRequest(EnumWebRequestMethod method)
        {
            var req = WebUtil.CreateHttpRequest(this._baseUri, method, this.GetQueryArgs());
            if (this.Timeout > 0)
                req.Timeout = this.Timeout;
            if (method == EnumWebRequestMethod.POST)
            {
                string formStr = GetFormString();
                WebUtil.PostRequestText(req, formStr, Encoding.UTF8);
                formStr = null;
            }
            return req;
        }
        #endregion

        #region Custom
        protected virtual void PrepareQueryArgs(IWebArgs args)
        {
            this.PrepareArgs(args);
        }
        protected virtual void PrepareFormArgs(IWebArgs args)
        {
            this.PrepareArgs(args);
        }
        protected virtual void PrepareArgs(IWebArgs args)
        {
        }
        protected virtual void PrepareRequest(HttpWebRequest req)
        {
        }
        protected virtual string PostResponseText(string respText)
        {
            return respText;
        }
        #endregion

        #region Native
        protected virtual IWebArgs CreateCommonArgs()
        {
            return new CommonWebArgs();
        }
        protected string GetQueryString()
        {
            this.PrepareQueryArgs(_getArgs);
            if (null == _getArgs)
                return string.Empty;
            return WebUtil.BuildEscapeDataString(_getArgs.GetCollection());
        }
        protected NameValueCollection GetQueryArgs()
        {
            this.PrepareQueryArgs(_getArgs);
            if (null == _getArgs)
                return null;
            return _getArgs.GetCollection();
        }
        protected string GetFormString()
        {
            this.PrepareFormArgs(_postArgs);
            if (null == _postArgs)
                return string.Empty;
            return WebUtil.BuildEscapeDataString(_postArgs.GetCollection());
        }
        #endregion
    }
}
