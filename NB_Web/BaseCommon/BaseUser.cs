/********************************************************************************
 * 文件名：BaseUser.cs 
 * 创建人：SH\wangweiqiang 
 * 创建时间：2010-2-5 14:51:14 
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：2010-2-5 14:51:14 
 * 功能说明：
 *  用户基类
 * 历史修改记录：
 <author>  <time>   <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using Games.NBall.Entity.Share;
using Newtonsoft.Json;

namespace Games.NBall.NB_Web.BaseCommon
{
    public class BaseUser
    {
        private UserAccountEntity _account = null;
        public virtual UserAccountEntity UserAccount
        {
            get
            {
                if(_account==null)
                {
                    _account = UserAccountMgr.GetCurrentUserAccount(CK);
                }
                return _account;
            }
        }

        public virtual bool IsLogin
        {
            get
            {
                if (this.UserAccount != null && !string.IsNullOrEmpty(UserAccount.Account) && UserAccount.ManagerId!=Guid.Empty)
                {
                    return true;
                }
                return false;
            }
        }

        public string CK
        {
            get
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Request["ck"]))
                {
                    return HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request["ck"]);
                }

                return "";
            }
        }
    }
}
