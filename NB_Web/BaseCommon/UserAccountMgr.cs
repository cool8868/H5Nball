/********************************************************************************
 * 文件名：UserAccountMgr.cs 
 * 创建人：SH\wangweiqiang 
 * 创建时间：2010-2-5 14:48:33
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：2010-2-5 14:48:33
 * 功能说明：
 *  用户账号管理类[用于登陆验证的用户管理类,跟Pass9整合的时候只需要更新这个类文件]
 * 历史修改记录：
 <author>  <time>   <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Security;
using System.Text;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Share;
using Games.NBall.NB_Web.Helper;

namespace Games.NBall.NB_Web.BaseCommon
{
    public class UserAccountMgr
    {
        /// <summary>
        /// 字符串信息解析为UserAccount信息
        /// </summary>
        /// <param name="data">字符串数据</param>
        /// <returns>UserAccountEntity</returns>
        public static UserAccountEntity Parse(string data)
        {
            string[] userData = data.Split("&".ToCharArray());
            if (userData.Length == 6)
            {
                UserAccountEntity userAccountEntity = new UserAccountEntity();

                userAccountEntity.Account = userData[0];
                userAccountEntity.ManagerId = new Guid(userData[1]);
                userAccountEntity.Name = userData[2];
                userAccountEntity.Area = int.Parse(userData[3]);
                userAccountEntity.PlatformCode = userData[4];
                userAccountEntity.SessionId = userData[5];
                return userAccountEntity;
            }
            else if (userData.Length == 7)
            {
                UserAccountEntity userAccountEntity = new UserAccountEntity();

                userAccountEntity.Account = userData[0];
                userAccountEntity.ManagerId = new Guid(userData[1]);
                userAccountEntity.Name = userData[2];
                userAccountEntity.Area = int.Parse(userData[3]);
                userAccountEntity.PlatformCode = userData[4];
                userAccountEntity.SessionId = userData[5];
                userAccountEntity.ExtraData = userData[6];
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
        /// <summary>
        /// 获取当前UserAccount信息
        /// </summary>
        /// <returns>UserAccountEntity</returns>
        public static UserAccountEntity GetCurrentUserAccount(string ck)
        {
            if (ck.Length > 0)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(ck);
                UserAccountEntity userAccountEntity = Parse(ticket.UserData);
                return userAccountEntity;
            }
            else
            {
                if (HttpContext.Current.User.Identity != null && HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                    UserAccountEntity userAccountEntity = Parse(ticket.UserData);
                    return userAccountEntity;
                }
            }
            return null;
        }

        
        static string GetCK(string key)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Params[key]))
            {
                return HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Params[key]);
            }
            return "";
        }
    }
}
