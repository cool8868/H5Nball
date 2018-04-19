using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.WebClient.Weibo.Data
{
    public struct EnumOAuth2Display
    {
        /// <summary>
        /// 默认的授权页面，适用于web浏览器
        /// </summary>
        public const string Default = "default";
        /// <summary>
        /// 移动终端的授权页面，适用于支持html5的手机。注：使用此版授权页请用 https://open.weibo.cn/oauth2/authorize 授权接口
        /// </summary>
        public const string Mobile = "mobile";
        /// <summary>
        /// wap版授权页面，适用于非智能手机。
        /// </summary>
        public const string Wap = "wap";
        /// <summary>
        /// 客户端版本授权页面，适用于PC桌面应用。
        /// </summary>
        public const string Client = "client";
        /// <summary>
        /// 默认的站内应用授权页，授权后不返回access_token，只刷新站内应用父框架
        /// </summary>
        public const string Apponweibo = "apponweibo";
    }
}
