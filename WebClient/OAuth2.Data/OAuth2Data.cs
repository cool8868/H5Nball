using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Collections.Specialized;
using Games.NBall.WebClient.Data;

namespace Games.NBall.WebClient.OAuth2.Data
{
    #region AuthCode
    public class Req_OAuth2AuthCode : BaseWebArgs
    {
        #region Names
        /// <summary>
        /// 必选，申请应用时分配的AppKey。
        /// </summary>
        public const string COLAppKey = "client_id";
        /// <summary>
        /// 必选，授权回调地址，站外应用需与设置的回调地址一致，站内应用需填写canvas page的地址。
        /// </summary>
        public const string COLRedirectUri = "redirect_uri";
        /// <summary>
        /// 申请scope权限所需参数，可一次申请多个scope权限，用逗号分隔。
        /// </summary>
        public const string COLScope = "scope";
        /// <summary>
        /// 用于保持请求和回调的状态，在回调时，会在Query Parameter中回传该参数。开发者可以用这个参数验证请求有效性，也可以记录用户请求授权页前的位置。这个参数可用于防止跨站请求伪造（CSRF）攻击
        /// </summary>
        public const string COLState = "state";
        #endregion

        #region Values
        public string AppKey
        {
            get { return GetValue(COLAppKey); }
            set { SetValue(COLAppKey, value); }
        }
        public string RedirectUri
        {
            get { return GetValue(COLRedirectUri); }
            set { SetValue(COLRedirectUri, value); }
        }
        public string Scope
        {
            get { return GetValue(COLScope); }
            set { SetValue(COLScope, value); }
        }
        public string State
        {
            get { return GetValue(COLState); }
            set { SetValue(COLState, value); }
        }
        #endregion

        #region Validate
        public override bool ValidateValue()
        {
            return !string.IsNullOrEmpty(this.AppKey)
                && !string.IsNullOrEmpty(this.RedirectUri);
        }
        #endregion
    }

    [Serializable]
    [DataContract]
    public class Resp_OAuth2AuthCode
    {
        /// <summary>
        /// 用于调用access_token，接口获取授权后的access token。
        /// </summary>
        [DataMember(Name = "code")]
        public string AuthCode { get; set; }
        /// <summary>
        /// 如果传递参数，会回传该参数。
        /// </summary>
        [DataMember(Name = "state")]
        public string State { get; set; }
    }
    #endregion

    #region AccessToken
    public class Req_OAuth2AccessToken : BaseWebArgs
    {
        #region Names
        /// <summary>
        /// 必选，申请应用时分配的AppKey。
        /// </summary>
        public const string COLAppKey = "client_id";
        /// <summary>
        /// 必选,申请应用时分配的AppSecret。
        /// </summary>
        public const string COLAppSecret = "client_secret";
        /// <summary>
        /// 必选，请求的类型，填写authorization_code。
        /// </summary>
        public const string COLGrantType = "grant_type";
        /// <summary>
        /// grant_type为authorization_code时必选，调用authorize获得的code值。
        /// </summary>
        public const string COLAuthCode = "code";
        /// <summary>
        /// grant_type为authorization_code时必选，回调地址，需需与注册应用里的回调地址一致。
        /// </summary>
        public const string COLRedirectUri = "redirect_uri";
        #endregion

        #region Values
        public string AppKey 
        {
            get { return GetValue(COLAppKey); }
            set { SetValue(COLAppKey, value); }
        }
        public string AppSecret
        {
            get { return GetValue(COLAppSecret); }
            set { SetValue(COLAppSecret, value); }
        }
        public string GrantType
        {
            get { return GetValue(COLGrantType); }
            set { SetValue(COLGrantType, value); }
        }
        public string AuthCode
        {
            get { return GetValue(COLAuthCode); }
            set { SetValue(COLAuthCode, value); }
        }
        public string RedirectUri
        {
            get { return GetValue(COLRedirectUri); }
            set { SetValue(COLRedirectUri, value); }
        }
        #endregion

        #region Validate
        public override bool ValidateValue()
        {
            return !string.IsNullOrEmpty(this.AppKey)
                && !string.IsNullOrEmpty(this.AppSecret)
                && !string.IsNullOrEmpty(this.GrantType)
                && !string.IsNullOrEmpty(this.AuthCode)
                && !string.IsNullOrEmpty(this.RedirectUri);
        }
        #endregion
    }

    [Serializable]
    [DataContract]
    public class Resp_OAuth2AccessToken
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }

        [DataMember(Name = "uid")]
        public string UserID { get; set; }

        [DataMember(Name = "expires_in")]
        public int ExpiresIn { get; set; }
    }
    #endregion
}
