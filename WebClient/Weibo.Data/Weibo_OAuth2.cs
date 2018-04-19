using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Collections.Specialized;
using Games.NBall.WebClient.Data;
using Games.NBall.WebClient.OAuth2.Data;

namespace Games.NBall.WebClient.Weibo.Data
{
    public class Req_WeiboAuthCode : Req_OAuth2AuthCode
    {
        #region Names
        /// <summary>
        /// 授权页面的终端类型
        /// </summary>
        public const string COLDisplay = "display";
        /// <summary>
        /// 是否强制用户重新登录，true：是，false：否。
        /// </summary>
        public const string COLForcelogin = "forcelogin";
        /// <summary>
        /// 授权页语言，缺省为中文简体版，en为英文版。
        /// </summary>
        public const string COLLanguage = "language";
        #endregion

        #region Values
        public string Display
        {
            get { return GetValue(COLDisplay); }
            set { SetValue(COLDisplay, value); }
        }
        public string ForceLogin
        {
            get { return GetValue(COLForcelogin); }
            set { SetValue(COLForcelogin, value); }
        }
        public string Language
        {
            get { return GetValue(COLLanguage); }
            set { SetValue(COLLanguage, value); }
        }
        #endregion
    }

    [Serializable]
    [DataContract]
    public class Resp_WeiboAccessToken
    {
        /// <summary>
        /// 用于调用access_token，接口获取授权后的access token。
        /// </summary>
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }
        /// <summary>
        /// access_token的生命周期，单位是秒数。
        /// </summary>
        [DataMember(Name = "expires_in")]
        public string ExpiresIn { get; set; }
        /// <summary>
        /// access_token的生命周期（该参数即将废弃，开发者请使用expires_in）。
        /// </summary>
        [DataMember(Name = "remind_in")]
        public string RemindIn { get; set; }
        /// <summary>
        /// 当前授权用户的UID。
        /// </summary>
        [DataMember(Name = "uid")]
        public int Uid { get; set; }
    }
}
