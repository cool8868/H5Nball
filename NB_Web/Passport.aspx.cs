using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity.Share;
using Games.NBall.NB_Web.BaseCommon;
using Games.NBall.ServiceContract.Client;

namespace Games.NBall.NB_Web
{
    public partial class Passport : System.Web.UI.Page
    {
        private string userName;
        private int area;

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// [wangweiqiang]     2010-2-8 11:24     Created
        /// </history>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        /// <summary>
        /// Gets or sets the area.
        /// </summary>
        /// <value>The area.</value>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// [wangweiqiang]     2010-2-8 11:24     Created
        /// </history>
        public int Area
        {
            get { return area; }
            set { area = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var host = Request.Url.Host;
            LogHelper.Insert(host, LogType.Info);
            lblMessage.Text = host;
            if (!IsPostBack)
            {
                try
                {
                    var list = UAFacade.UAFactory.Instance.GetAllPlatform();
                    ddlPlatform.DataSource = list;
                    ddlPlatform.DataTextField = "PlatformCode";
                    ddlPlatform.DataValueField = "PlatformCode";
                    ddlPlatform.DataBind();

                    ddlPlatform.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }
            if (Request["account"] != null)
            {
                int code = -1;
                string message = "";
                bool isValid = Check(Request["account"]);
                if (isValid)
                {
                    if (doLogin())
                    {
                        code = 0;
                        message = "success";
                    }
                    else
                    {
                        code = -3;
                        message = lblMessage.Text;
                    }
                }
                else
                {
                    code = -2;
                    message = lblMessage.Text;
                }
                Response.Write("{\"Code\":" + code + ",\"Message\":\"" + message + "\"}");
                Response.End();
            }
        }

        private bool Check()
        {
            string username = txtName.Text.Trim();
            return Check(username);
        }

        /// <summary>
        /// Checks the specified properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// [wangweiqiang]     2010-2-8 16:15     Created
        /// </history>
        private bool Check(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                ResponseMessage("用户名不能为空");
                return false;
            }
            //去掉空格和控制字符，不然MemcachedClient会抛异常
            if (username.Contains(" ") || username.Contains("\n") || username.Contains("\r") || username.Contains("\t") || username.Contains("\f") || username.Contains("\v"))
            {
                ResponseMessage("用户名包含无效字符，请重新输入");
                return false;
            }
            if (username.Length > 50)
            {
                ResponseMessage("用户名过长");
                return false;
            }

            this.UserName = username;
            return true;
        }

        protected void BtnLoginClick(object sender, EventArgs e)
        {
            bool isValid = Check();
            if (isValid)
            {
                if (doLogin())
                {
                    Response.Redirect("Index.aspx?ck=" + Response.Cookies[FormsAuthentication.FormsCookieName].Value);
                    Response.End();
                }
            }
        }

        protected void BtnTestClick(object sender, EventArgs e)
        {
            bool isValid = Check();
            if (isValid)
            {
                if (doLogin())
                {
                    var url = System.Configuration.ConfigurationManager.AppSettings["TestUrl"];
                    Response.Redirect(url);
                    Response.End();
                }
            }
        }
        private bool doLogin()
        {
            try
            {
                var platformId = ddlPlatform.SelectedValue;
                var user = NbUserMgr.GetByAccount(UserName, Request.UserHostAddress, DateTime.Today.AddDays(-1),
                    DateTime.Today, 0);
                var sessionId = ShareUtil.GenerateComb().ToString();
                if (user != null)
                {
                    UserAccountEntity userAccountEntity = new UserAccountEntity(user.Account, Guid.Empty, "", 1,
                        platformId, sessionId);
                    userAccountEntity.ExtraData = "d|pengyou|f";
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.Account,
                        DateTime.Now,
                        DateTime.Now.AddDays(10),
                        false, userAccountEntity.ToString(),
                        FormsAuthentication
                            .FormsCookiePath);
                    var cookie = FormsAuthentication.Encrypt(ticket);
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, cookie));
                   // OnlineMgr.LoginSession(info.Data.ManagerInfo.Manager.Idx, sessionId);
                    return true;
                }
                else
                {
                    ResponseMessage("用户名不存在");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                ResponseMessage(ex.Message);
                return false;
            }

        }

        private void ResponseMessage(string message)
        {
            lblMessage.Text = message;
        }

        #region Request
        /// <summary>
        /// request 数字
        /// 如果为空则返回0
        /// </summary>
        /// <param name="parmsName"></param>
        /// <returns></returns>
        protected int RequestNum(string parmsName)
        {
            int num = 0;
            int.TryParse(Request[parmsName], out num);
            return num;
        }

        /// <summary>
        /// request GUID
        /// 如果为空则返回空
        /// </summary>
        /// <param name="parmsName"></param>
        /// <returns></returns>
        protected Guid RequestGuid(string parmsName)
        {
            try
            {
                Guid num = new Guid(Request[parmsName]);

                return num;
            }
            catch// (Exception ex)
            {
                return Guid.Empty;
            }
        }
        #endregion
    }
}